using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillManager : MonoBehaviour
{
    public class SkillLoadoutObject
    {
        private bool canCast;
        private Skill skill;
        private CastSkill castSkill;

        public bool _canCast
        {  get { return canCast; } set { canCast = value; } }
        public Skill _skill
        {  get { return skill; } set { skill = value; } }

        public CastSkill _castSkill
        { get { return castSkill; } set { castSkill = value; } }

        public SkillLoadoutObject(Skill skill, CastSkill castSkill)
        {
            canCast = true;
            this.skill = skill;
            this.castSkill = castSkill;
        }
    }

    private Dictionary<string, Stack<GameObject>>
        skillPools = new Dictionary<string, Stack<GameObject>>(); //Contains the pools for each skill 

    private List<SkillLoadoutObject>
        skillLoadout = new List<SkillLoadoutObject>(); //Contains the skills that the player currently has 

    private Dictionary<string, GameObject>
        skillPrefabs = new Dictionary<string, GameObject>(); //Contains the prefabs that the skills will clone from upon instantiating 

    private PlayerBehaviour
        player;

    private int
        skillSlots = 4;

    private List<float>
        skillCooldowns = new List<float>();

    private float
        timer, 
        playerRecoil = 0.5f, 
        dashSpeed = 50f, 
        dashDuration = 0.2f;

    private GameObject
        target = null;

    public delegate void CastSkill();

    public List<SkillLoadoutObject> _skillLoadout
    { get { return skillLoadout; } }

    private void Awake()
    {
        Game._skillManager = this; //Gives a reference to the static Game class 
        player = Game._player.GetComponent<PlayerBehaviour>(); //Gets a reference to the player 
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
            float recoil = Random.Range(-playerRecoil, playerRecoil); //Creates a random float based on the player's recoil variable and adds it to the target's location to create a recoil effect
            Vector2 dir = new Vector2(target.transform.position.x + recoil - transform.position.x, target.transform.position.y + recoil - transform.position.y);
            transform.up = dir;

            if (timer < 1f)
            {
                timer += Time.deltaTime * Game._player._attackSpeed;

                if (timer >= 1f)
                {
                    Debug.Log(skillLoadout[0]._skill._skillCooldown);
                    skillLoadout[0]._castSkill.Invoke();
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
            if (skillLoadout[1]._canCast)
            {
                skillLoadout[1]._castSkill.Invoke();
                StartCoroutine(CooldownTimer(skillLoadout[1]));
            }
            else
            {
                Debug.Log("Cannot cast skill");
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (skillLoadout[2]._canCast)
            {
                skillLoadout[2]._castSkill.Invoke();
                StartCoroutine(CooldownTimer(skillLoadout[1]));
            }
            else
            {
                Debug.Log("Cannot cast skill");
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (skillLoadout[3]._canCast)
            {
                skillLoadout[3]._castSkill.Invoke();
                StartCoroutine(CooldownTimer(skillLoadout[1]));
            }
            else
            {
                Debug.Log("Cannot cast skill");
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (skillLoadout[4]._canCast)
            {
                skillLoadout[4]._castSkill.Invoke();
                StartCoroutine(CooldownTimer(skillLoadout[1]));
            }
            else
            {
                Debug.Log("Cannot cast skill");
            }
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
    private SkillLoadoutObject AddSkillEffects(string skillId) //Adds behaviours to each skill based on its ID
    {
        SkillLoadoutObject result = new SkillLoadoutObject(Game._database._skillDB[skillId], null); //Adds a new object to store the skill as well as the skill's effects
        switch (skillId)
        {
            case "PROJ0001":
                result._castSkill = () =>
                {
                    AddProjectileEffects(GetObject(result._skill._skillName), result._skill as Projectile, null);
                };
                break;

            case "AREA0001":
                result._castSkill = () =>
                {
                    AddAOEEffects(GetObject(result._skill._skillName), result._skill as AOE, null);
                };
                break;
            case "Test":
                result._castSkill = () =>
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
    private int CalculateDamage(float skillMultiplier)
    {
        return (int)((Game._player._attack /* + modifiers from equipment*/) * skillMultiplier);
    }
    private IEnumerator CooldownTimer(SkillLoadoutObject obj)
    {
        obj._canCast = false;

        float timer = obj._skill._skillCooldown; 
        
        while (timer >= 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
            Debug.Log(timer);
        }
        obj._canCast = true;
    }
}
