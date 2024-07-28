using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 
        vel = Vector3.zero;

    private static CameraController
        cameraController;

    private void Awake()
    {
        if (cameraController != null && cameraController != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            cameraController = this;
        } 
            
        DontDestroyOnLoad(cameraController);
    }

    private void Update()
    {
        if(Game._player != null)
        {
            Vector3 playerPos = new Vector3(Game._player.transform.position.x, Game._player.transform.position.y, -10f);
            this.transform.position = Vector3.SmoothDamp(this.transform.position, playerPos, ref vel, 0.1f);
        }
    }
}
