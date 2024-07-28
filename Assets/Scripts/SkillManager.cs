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
        private GameObject uILoadoutBox; 

        public bool _canCast
        {  get { return canCast; } set { canCast = value; } }
        public Skill _skill
        {  get { return skill; } set { skill = value; } }

        public CastSkill _castSkill
        { get { return castSkill; } set { castSkill = value; } }

        public GameObject _uIBoxLocation
        { get { return uILoadoutBox; } set { uILoadoutBox = value; } }

        public SkillLoadoutObject(Skill skill, CastSkill castSkill, GameObject uILoadoutBox)
        {
            canCast = true;
            this.skill = skill;
            this.castSkill = castSkill;
            this.uILoadoutBox = uILoadoutBox;
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
        skillSlots = 5, 
        skillCount = 0;

    private float
        timer, 
        playerRecoil = 0.5f, 
        dashSpeed = 50f, 
        dashDuration = 0.1f;

    private GameObject
        target = null;

    [SerializeField]
    private GameObject
        cursorAim;

    [SerializeField]
    private List<GameObject>
        uILoadoutBox = new List<GameObject>();

    public float _dashSpeed
    { get { return dashSpeed; } set { dashSpeed = value; } }

    public float _dashDuration
    { get { return dashDuration; } set { dashDuration = value; } }

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
        uILoadoutBox = GameObject.FindGameObjectWithTag("Loadout").GetComponent<SkillLoadoutBoxes>().skillLoadoutBoxes;
        InitializePrefabSkills(Game._player._projectileType);
    }
    void Update()
    {
        //Aims at the cursor 
        Vector2 cursorDir = new Vector2(Game._cursor.transform.position.x - transform.position.x, Game._cursor.transform.position.y - transform.position.y);
        cursorAim.transform.up = cursorDir;

        //Handles the auto shooting of projectiles
        if (target != null)
        {
            float recoil = Random.Range(-playerRecoil, playerRecoil); //Creates a random float based on the player's recoil variable and adds it to the target's location to create a recoil effect
            Vector2 dir = new Vector2(target.transform.position.x + recoil - transform.position.x, target.transform.position.y + recoil - transform.position.y);
            transform.up = dir;

            if (timer < 1f)
            {
                timer += Time.deltaTime * Game._player._attackSpeed;

                if (timer >= 1f)
                {
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
                StartCoroutine(CooldownTimer(skillLoadout[2]));
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
                StartCoroutine(CooldownTimer(skillLoadout[3]));
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
                StartCoroutine(CooldownTimer(skillLoadout[4]));
            }
            else
            {
                Debug.Log("Cannot cast skill");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Dash(Game._cursor.transform.position));
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            InitializePrefabSkills("AREA0001");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            InitializePrefabSkills("AREA0002");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            InitializePrefabSkills("AREA0003");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            InitializePrefabSkills("AREA0004");
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

        Game._inputHandler._disableMovement = true;
        while (dashTimer < dashDuration)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, dashPos, dashSpeed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
            dashTimer += Time.deltaTime;
        }

        player._nav.SetDestination(player.transform.position);
        Game._inputHandler._disableMovement = false;
    }
    #region Loading
    public void InitializePrefabSkills(string skillId)
    {
        if(skillCount < skillSlots) //Checks if there are enough available skill slots
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
        SkillLoadoutObject result = new SkillLoadoutObject(Game._database._skillDB[skillId], null, uILoadoutBox[skillCount]); //Adds a new object to store the skill, its effects as well as the UI element that it belongs to

        if (skillId.Substring(0,4) == "PROJ")
        {
            if(skillId == "PROJ0001" || skillId == "PROJ0002" || skillId == "PROJ0003" || skillId == "PROJ0004")
            {
                result._castSkill = () =>
                {
                    AddBasicProjectileEffects(GetObject(result._skill._skillName), result._skill as Projectile, null);
                };
            }
            else
            {
                result._castSkill = () =>
                {
                    AddProjectileEffects(GetObject(result._skill._skillName), result._skill as Projectile, null);
                };
            }
        }
        else if (skillId.Substring(0,4) == "AREA")
        {
            result._castSkill = () =>
            {
                AddAOEEffects(GetObject(result._skill._skillName), result._skill as AOE, null);
            };
        }
        //switch (skillId)
        //{
        //    case "PROJ0001":
        //        result._castSkill = () =>
        //        {
        //            AddProjectileEffects(GetObject(result._skill._skillName), result._skill as Projectile, null);
        //        };
        //        break;

        //    case "PROJ0002":
        //        result._castSkill = () =>
        //        {
        //            AddProjectileEffects(GetObject(result._skill._skillName), result._skill as Projectile, null);
        //        };
        //        break;

        //    case "PROJ0003":
        //        result._castSkill = () =>
        //        {
        //            AddProjectileEffects(GetObject(result._skill._skillName), result._skill as Projectile, null);
        //        };
        //        break;

        //    case "PROJ0004":
        //        result._castSkill = () =>
        //        {
        //            AddProjectileEffects(GetObject(result._skill._skillName), result._skill as Projectile, null);
        //        };
        //        break;

        //    case "AREA0001":
        //        result._castSkill = () =>
        //        {
        //            AddAOEEffects(GetObject(result._skill._skillName), result._skill as AOE, null);
        //        };
        //        break;
        //    case "AREA0002":
        //        result._castSkill = () =>
        //        {
        //            AddAOEEffects(GetObject(result._skill._skillName), result._skill as AOE, null);
        //        };
        //        break;
        //    case "AREA0003":
        //        result._castSkill = () =>
        //        {
        //            AddAOEEffects(GetObject(result._skill._skillName), result._skill as AOE, null);
        //        };
        //        break;
        //    case "AREA0004":
        //        result._castSkill = () =>
        //        {
        //            AddAOEEffects(GetObject(result._skill._skillName), result._skill as AOE, null);
        //        };
        //        break;
        //    case "Test":
        //        result._castSkill = () =>
        //        {

        //        };
        //        break;
        //}
        uILoadoutBox[skillCount++].GetComponent<LoadoutCooldownBox>().SetImage(result._skill._skillIcon); //Loads the skill icon into the loadout

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
    //Used to initialize basic projectile skills 
    private void AddBasicProjectileEffects(GameObject skillToAdd, Projectile refSkill, UnityAction extras)
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
    //Used to initialize projectile skills 
    private void AddProjectileEffects(GameObject skillToAdd, Projectile refSkill, UnityAction extras)
    {
        //Initializes the transform of the projectile to the player's location 
        skillToAdd.transform.position = cursorAim.transform.position;
        skillToAdd.transform.rotation = cursorAim.transform.rotation;

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
        if(Game._cursor != null)
        {
            return Game._cursor.transform.position;
        }
        return Vector3.zero;
    }
    private void OnDrawGizmos() //Delete after testing 
    {
        if (GetCursorPos() != null)
        {
            Gizmos.color = new Vector4(1, 0, 0, 0.5f);
            Gizmos.DrawLine(this.transform.position, GetCursorPos());
        }
    }
    private int CalculateDamage(float skillMultiplier) // Calculate the damage dealt
    {
        return (int)((Game._player._attack /* + modifiers from equipment*/) * skillMultiplier);
    }
    private IEnumerator CooldownTimer(SkillLoadoutObject obj) //Handles the cooldown for each skill
    {
        obj._canCast = false;

        float timer = obj._skill._skillCooldown; //Gets the cooldown from the skill database

        obj._uIBoxLocation.GetComponent<LoadoutCooldownBox>().StartCooldown(obj._skill._skillCooldown); //Starts the cooldown overlay in the UI 
        while (timer >= 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        obj._canCast = true;
    }
    public void ResetSkillPools()
    {
        foreach(KeyValuePair<string, Stack<GameObject>> keyValuePair in skillPools)
        {
            keyValuePair.Value.Clear();
        }
    }
}
