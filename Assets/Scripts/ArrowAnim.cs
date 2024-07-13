using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnim : MonoBehaviour
{
    public void EndAnimation()
    {
        Game._cursor.DestroyArrowIndicator(this.gameObject);
    }
}
