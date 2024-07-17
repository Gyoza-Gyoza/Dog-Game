using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private GameObject
        projectilePrefab,
        projectileSource;

    private Vector3
        cursorPos;

    private Stack<GameObject>
        projectilePool = new Stack<GameObject>();

    private PlayerBehaviour
        player;

    private int
        playerDamage,
        playerAttackSpeed;

    private float
        timer, 
        playerRecoil = 1.5f;

    private void Awake()
    {
        Game._skillManager = this;
        projectileSource = gameObject;
        player = Game._player.GetComponent<PlayerBehaviour>();
        playerDamage = Game._chosenPlayer._attack;
        playerAttackSpeed = 10;
    }

    void Update()
    {
        float recoil = Random.Range(-playerRecoil, playerRecoil);
        cursorPos = new Vector3(Game._cursor.transform.position.x + recoil, Game._cursor.transform.position.y + recoil, Game._cursor.transform.position.z);

        Vector2 dir = new Vector2(cursorPos.x - transform.position.x, cursorPos.y - transform.position.y); 
        transform.up = dir;


        if(timer < 1f)
        {
            timer += Time.deltaTime * playerAttackSpeed;  

            if (timer >= 1f)
            {
                ShootProjectile(); 
                timer = 0f;
            }
        }
    }
    private void ShootProjectile()
    {
        if(projectilePool.TryPop(out GameObject result))
        {
            result.SetActive(true); 
            result.transform.position = transform.position;
            result.transform.rotation = transform.rotation; 

            result.GetComponent<Projectile>()._damage = Game._player._attack;
        }
        else
        {
            Vector3 pos = transform.position;
            Quaternion rot = transform.rotation;
            GameObject projectile = Instantiate(projectilePrefab, pos, rot);

            projectile.GetComponent<Projectile>()._damage = Game._player._attack;
        }
    }
    public void DestroyProjectile(GameObject objToDestroy)
    {
        objToDestroy.SetActive(false);
        projectilePool.Push(objToDestroy);
    }
    private void OnDrawGizmos()
    {
        if(cursorPos != null)
        {
            Gizmos.color = new Vector4(1, 0, 0, 0.5f);
            Gizmos.DrawLine(this.transform.position, cursorPos);
        }
    }
}
