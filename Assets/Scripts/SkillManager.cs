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
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private GameObject
        projectilePrefab, 
        aOEPrefab;

    private Vector3 //Delete after testing 
        cursorPos;

    private Stack<GameObject>
        projectilePool = new Stack<GameObject>(), 
        aOEPool = new Stack<GameObject>(), 
        buffPool = new Stack<GameObject>();

    private Dictionary<string, Stack<GameObject>>
        skillPools = new Dictionary<string, Stack<GameObject>>();

    private List<CastSkill>
        skillLoadout = new List<CastSkill>();

    //private List<Skill>
    //    skillList = new List<Skill>();

    //private Dictionary<Skill, CastSkill>
    //    skillLoadout = new Dictionary<Skill, CastSkill>();

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

    //public Dictionary<Skill, CastSkill> _skillLoadout
    //{ get {  return skillLoadout; } }

    private void Awake()
    {
        Game._skillManager = this;
        player = Game._player.GetComponent<PlayerBehaviour>();
        playerDamage = Game._chosenPlayer._attack;
        playerAttackSpeed = 10;
    }
    private void Start()
    {
        //for(int i = 0; i < skillSlots; i++)
        //{
        //    skillLoadout.Add(null, null);
        //}
        //basicSkill = InitializeSkill("SKILLPROJ0001") as Projectile;
        //AddToLoadout("SKILLAOE0001");
        InitializePrefabSkills("PROJ0001");
        InitializePrefabSkills("AREA0001");
    }
    void Update()
    {
        cursorPos = Game._cursor.transform.position; //Delete after testing 

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
                    //ShootProjectile();
                    timer = 0f;
                }
            }
        }
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
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            
        }
    }
    
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
    #region Skills
    private int CalculateDamage(float skillMultiplier)
    {
        return (int)((Game._player._attack /* + modifiers from equipment*/) * skillMultiplier);
    }
    //private void ShootProjectile()
    //{
    //    int damage = CalculateDamage(basicSkill._projectileDamage);

    //    Vector3 pos = transform.position;
    //    Quaternion rot = transform.rotation;

    //    if (projectilePool.TryPop(out GameObject result))
    //    {
    //        result.SetActive(true); 
    //        result.transform.position = pos;
    //        result.transform.rotation = rot;

    //        result.GetComponent<ProjectileBehaviour>()._damage = damage;
    //    }
    //    else
    //    {
    //        GameObject projectile = Instantiate(projectilePrefab, pos, rot);

    //        projectile.GetComponent<ProjectileBehaviour>().SetStats(
    //            basicSkill._skillName, 
    //            damage, 
    //            basicSkill._projectilePierce, 
    //            basicSkill._projectileSpeed, 
    //            basicSkill._projectileSize, 
    //            skillSprites[basicSkill._skillName]);
    //    }
    //}
    //public void DestroyProjectile(GameObject objToDestroy)
    //{
    //    objToDestroy.SetActive(false);
    //    projectilePool.Push(objToDestroy);
    //}

    //private void CastAOE(AOE aoeSkill)
    //{;
    //    int damage = CalculateDamage(aoeSkill._aOEDamage);
    //    //finish aoe
    //    Vector3 pos = cursorPos; 

    //    if (aOEPool.TryPop(out GameObject result))
    //    {
    //        result.SetActive(true);
    //        result.transform.position = pos;

    //        result.GetComponent<AOEBehaviour>()._damage = damage;
    //    }
    //    else
    //    {
    //        GameObject aoeObj = Instantiate(aOEPrefab, pos, Quaternion.identity);
    //        //aoeObj.GetComponent<AOEBehaviour>().SetStats(aoeSkill._skillName, damage, aoeSkill._aOESize, skillSprites[aoeSkill._skillName]);
    //    }
    //}
    
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
    #endregion

    #region Loading
    //This function is to initialize skills that the player has as well as preload all
    //the sprites to prevent spawning skills without sprites
    //private Skill InitializeSkill(string skillId)
    //{
    //    Skill skill = Game._database._skillDB[skillId];

    //    AssetManager.LoadSprites(skill._skillSprite, (Sprite sp) =>
    //    {
    //        skillSprites.Add(skill._skillName, sp);
    //    });

    //    return skill;
    //}
    //private void AddToLoadout(string skillId)
    //{
    //    if (skillLoadout.Count < skillSlots)
    //    {
    //        //skillLoadout.Add(InitializeSkill(skillId) as AOE, AddSkill(skillId)); 
    //    }
    //}
    
    #endregion

    private void TestSkill() //Delete after testing 
    {
        Debug.Log("Using Skill");
    }
    private void OnDrawGizmos() //Delete after testing 
    {
        if(cursorPos != null)
        {
            Gizmos.color = new Vector4(1, 0, 0, 0.5f);
            Gizmos.DrawLine(this.transform.position, cursorPos);
        }
    }
    public void InitializePrefabSkills(string skillId)
    {
        Skill skill = Game._database._skillDB[skillId]; //Gets skill information from the database

        AssetManager.LoadPrefabs(skill._skillPrefab, (GameObject go) => 
        {
            skillPrefabs.Add(skill._skillName, go); //Preloads the prefab and stores it into dictionary with its name as the key
        });

        skillLoadout.Add(AddSkillEffects(skillId)); //Adds the skill's functionalities into the loadout 

        InitializePool(skill._skillName); //Creates a pool to store the prefabs 
    }
    private CastSkill AddSkillEffects(string skillId)
    {
        Skill skillChosen = Game._database._skillDB[skillId];
        CastSkill result = null;
        switch (skillId)
        {
            case "PROJ0001":
                result = () =>
                {
                    AddProjectileEffects(GetPool(skillChosen._skillName), skillChosen as Projectile);
                };
                break;

            case "AREA0001":
                result = () =>
                {
                    AddAOEEffects(GetPool(skillChosen._skillName), skillChosen as AOE);
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
    private void InitializePool(string skillName) //Creates a pool to contain the skill prefabs if it doesn't exist
    {
        if(skillPools.ContainsKey(skillName) == false)
        {
            skillPools.Add(skillName, new Stack<GameObject>());
        }
    }
    private GameObject GetPool(string skillName)
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
    public void DestroySpell(GameObject spellToDelete)
    {
        spellToDelete.SetActive(false);
        skillPools[spellToDelete.name].Push(spellToDelete);
    }
    private void AddProjectileEffects(GameObject skillToAdd, Projectile refSkill)
    {
        skillToAdd.transform.position = transform.position;
        skillToAdd.transform.rotation = transform.rotation;
        skillToAdd.GetComponent<ProjectileBehaviour>().SetStats(CalculateDamage(refSkill._projectileDamage), refSkill._projectilePierce, refSkill._projectileSpeed, refSkill._projectileSize);
    }
    private void AddAOEEffects(GameObject skillToAdd, AOE refSkill)
    {
        skillToAdd.transform.position = GetCursorPos();
        skillToAdd.GetComponent<AOEBehaviour>().SetStats(CalculateDamage(refSkill._aOEDamage), refSkill._aOESize);
    }
    private Vector3 GetCursorPos()
    {
        return Game._cursor.transform.position;
    }
}