using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField]
    private GameObject
        arrowCursor; 

    private Stack<GameObject>
        arrowPool = new Stack<GameObject>();

    private Animator
        animator;

    private Vector3
        mousePos;

    public Vector3
        _mousePos
    {
        get { return mousePos; }
    }

    private void Awake()
    {
        Game._cursor = this;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.position = mousePos;
    }
    private GameObject GetArrowIndicator()
    {
        if (arrowPool.TryPop(out GameObject result))
        {
            result.SetActive(true);
            return result;
        }
        else
        {
            return Instantiate(arrowCursor, mousePos, Quaternion.identity);
        }
    }
    public void SpawnArrowIndicator(Vector3 pos)
    {
        GameObject obj = GetArrowIndicator();
        obj.SetActive(true); 
        obj.transform.position = pos;
    }
    public void DestroyArrowIndicator(GameObject arrow)
    {
        arrow.SetActive(false);
        arrowPool.Push(arrow);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Vector4(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(this.transform.position, 1f);
    }
}
