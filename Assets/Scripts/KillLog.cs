using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLog : MonoBehaviour
{
    public GameObject spawn;       
    public GameObject spawnLog;    
    public GameObject log;         

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            CharacterController characterController = collision.gameObject.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
            }

            collision.gameObject.transform.position = spawn.transform.position;

            if (characterController != null)
            {
                characterController.enabled = true;
            }

            if (log != null && spawnLog != null)
            {
                log.transform.position = spawnLog.transform.position;
            }
        }
    }
}
