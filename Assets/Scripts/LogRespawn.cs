using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRespawn : MonoBehaviour
{
    public GameObject spawn;
    public GameObject spawnPlayer;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Log"))
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
        }
    }
}
