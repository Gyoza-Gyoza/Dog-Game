using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DONE BY DYLAN NG SHAO WEI
public class Dependencies : MonoBehaviour
{
    private static Dependencies
        dependencies;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
