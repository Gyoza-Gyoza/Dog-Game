using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    private void Start()
    {
        projectileSource = gameObject;
    }

    void Update()
    {
        cursorPos = Game._cursor.transform.position;

        Vector2 dir = new Vector2(cursorPos.x - transform.position.x, cursorPos.y - transform.position.y); 
        transform.up = dir;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = transform.position;
            Quaternion rot = transform.rotation;
            GameObject projectile = Instantiate(projectilePrefab, pos, rot);

            projectile.GetComponent<Projectile>()._damage = Game._player._attack;
            projectile.GetComponent<Rigidbody2D>().AddForce(transform.up * 5f, ForceMode2D.Impulse);
        }
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
