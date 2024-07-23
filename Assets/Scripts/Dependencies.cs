using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dependencies : MonoBehaviour
{
    private static Dependencies
        dependencies;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
