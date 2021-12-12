using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolowScript : NetworkBehaviour
{
    private void Start()
    {
        transform.gameObject.SetActive(true);
    } /*    
    Transform _player;
    
     void Start()
    {
        if (isLocalPlayer)
        {
            _player = FindObjectOfType<Player>().transform;
        }
    }
    void Update()
    {
       transform.position = _player.position - new Vector3(0, 0, -7);
    }
   */
}
