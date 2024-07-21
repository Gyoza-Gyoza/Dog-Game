using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private GameObject
        projectilePrefab, 
        aOEPrefab;

    private Stack<GameObject>
        projectilePool = new Stack<GameObject>(), 
        aOEPool = new Stack<GameObject>(), 
        buffPool = new Stack<GameObject>();

    private Dictionary<string, Stack<GameObject>>
        skillPools = new Dictionary<string, Stack<GameObject>>();

    private List<CastSkill>
        skillLoadout = new List<CastSkill>();

    private Projectile
        basicSkill;

    private Dictionary<string, Sprite>
        skillSprites = new Dictionary<string, Sprite>();

    private Dictionary<string, GameObject>
        skillPrefabs = new Dictionary<string, GameObject>();

    private PlayerBehaviour
        player;

    private int
        playerDamage,
        playerAttackSpeed, 
        skillSlots = 4;

    private float
        timer, 
        playerRecoil = 0.5f, 
        dashSpeed = 50f, 
        dashDuration = 0.2f;

    private GameObject
        target = null;

    public delegate void CastSkill();

    public List<CastSkill> _skillLoadout
    { get { return skillLoadout; } }

    private void Awake()
    {
        Game._skillManager = this; //Gives a reference to the static Game class 
        player = Game._player.GetComponent<PlayerBehaviour>(); //Gets a reference to the player 
        playerDamage = Game._chosenPlayer._attack; //Gets the damage of the player 
        playerAttackSpeed = 10; //Gets the attack speed of the player 
    }
    private void Start()
    {
        InitializePrefabSkills("PROJ0001");
        InitializePrefabSkills("AREA0001");
    }
    void Update()
    {
        //Handles the auto shooting of projectiles
        if(target != null)
        {
            float recoil = UnityEngine.Random.Range(-playerRecoil, playerRecoil);
            Vector2 dir = new Vector2(target.transform.position.x + recoil - transform.position.x, target.transform.position.y + recoil - transform.position.y);
            transform.up = dir;

            if (timer < 1f)
            {
                timer += Time.deltaTime * playerAttackSpeed;

                if (timer >= 1f)
                {
                    skillLoadout[0].Invoke();
                    timer = 0f;
                }
            }
        }
        GetInput();
    }
    //Gets the key input 
    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            skillLoadout[1].Invoke();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            skillLoadout[2].Invoke();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            skillLoadout[3].Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            skillLoadout[4].Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //AddToLoadout();
            StartCoroutine(Dash(Game._cursor.transform.position));
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {

        }
    }
    //Checks if there are enemies in range
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<IDamageable>() != null && target == null)
        {
            target = collision.gameObject; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamageable>() != null)
        {
            target = null;
        }
    }
    private int CalculateDamage(float skillMultiplier)
    {
        return (int)((Game._player._attack /* + modifiers from equipment*/) * skillMultiplier);
    }
    
    private IEnumerator Dash(Vector3 dashPos)
    {
        float dashTimer = 0f;

        player._disableMovement = true;
        while (dashTimer < dashDuration)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, dashPos, dashSpeed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
            dashTimer += Time.deltaTime;
        }

        player._nav.SetDestination(player.transform.position);
        player._disableMovement = false;
    }
    #region Loading
    public void InitializePrefabSkills(string skillId)
    {
        if(skillLoadout.Count < skillSlots) //Checks if there are enough available skill slots
        {
            Skill skill = Game._database._skillDB[skillId]; //Gets skill information from the database

            AssetManager.LoadPrefabs(skill._skillPrefab, (GameObject go) =>
            {
                skillPrefabs.Add(skill._skillName, go); //Preloads the prefab and stores it into dictionary with its name as the key
            });

            skillLoadout.Add(AddSkillEffects(skillId)); //Adds the skill's functionalities into the loadout 

            InitializePool(skill._skillName); //Creates a pool to store the prefabs 
        }
        else
        {
            Debug.Log("Not enough slots");
        }
    }
    private CastSkill AddSkillEffects(string skillId) //Adds behaviours to each skill based on its ID
    {
        Skill skillChosen = Game._database._skillDB[skillId]; //Stores the skill to be referenced later on 
        CastSkill result = null; //Initializes a delegate to contain the skill effects 
        switch (skillId)
        {
            case "PROJ0001":
                result = () =>
                {
                    AddProjectileEffects(GetObject(skillChosen._skillName), skillChosen as Projectile, null);
                };
                break;

            case "AREA0001":
                result = () =>
                {
                    AddAOEEffects(GetObject(skillChosen._skillName), skillChosen as AOE, null);
                };
                break;
            case "Test":
                result = () =>
                {

                };
                break;
        }

        return result;
    }
    //Creates a pool to contain the skill prefabs if it doesn't exist
    private void InitializePool(string skillName) 
    {
        if (skillPools.ContainsKey(skillName) == false)
        {
            skillPools.Add(skillName, new Stack<GameObject>());
        }
    }
    //Used to initialize projectile skills 
    private void AddProjectileEffects(GameObject skillToAdd, Projectile refSkill, UnityAction extras)
    {
        //Initializes the transform of the projectile to the player's location 
        skillToAdd.transform.position = transform.position;
        skillToAdd.transform.rotation = transform.rotation;

        //Sets the stats of the projectile as well as the added effects
        ProjectileBehaviour behaviour = skillToAdd.GetComponent<ProjectileBehaviour>();
        behaviour.SetStats(CalculateDamage(refSkill._projectileDamage), refSkill._projectilePierce, refSkill._projectileSpeed, refSkill._projectileSize);
        if (extras != null)
        {
            behaviour.SetSpellEffects(extras);
        }
    }
    //Used to initialize aoe skills 
    private void AddAOEEffects(GameObject skillToAdd, AOE refSkill, UnityAction extras)
    {
        //Initializes the location of the skill
        skillToAdd.transform.position = GetCursorPos();

        //Sets the stats of the skill as well as the added effects
        AOEBehaviour behaviour = skillToAdd.GetComponent<AOEBehaviour>();
        behaviour.GetComponent<AOEBehaviour>().SetStats(CalculateDamage(refSkill._aOEDamage), refSkill._aOESize);
        if (extras != null)
        {
            behaviour.SetSpellEffects(extras);
        }
    }
    #endregion

    private GameObject GetObject(string skillName) //Gets the object from the pool
    {
        if (skillPools[skillName].TryPop(out GameObject result))
        {
            result.SetActive(true);
            return result;
        }
        else
        {
            GameObject obj = Instantiate(skillPrefabs[skillName]);
            obj.name = skillName;
            return obj;
        }
    }
    public void DestroySpell(GameObject spellToDelete) //Function to add objects into the pool
    {
        spellToDelete.SetActive(false);
        skillPools[spellToDelete.name].Push(spellToDelete);
    }
    
    private Vector3 GetCursorPos()
    {
        return Game._cursor.transform.position;
    }
    private void OnDrawGizmos() //Delete after testing 
    {
        if (GetCursorPos() != null)
        {
            Gizmos.color = new Vector4(1, 0, 0, 0.5f);
            Gizmos.DrawLine(this.transform.position, GetCursorPos());
        }
    }
}