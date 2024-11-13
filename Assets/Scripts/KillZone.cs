using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public GameObject spawn;
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Objeto entro a la zona de muerte: "+collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("El objeto es el jugador, teletransportando a spawn");
            CharacterController characterController = collision.gameObject.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
            }
            collision.gameObject.transform.position = spawn.transform.position;
            if(characterController != null)
            {
                characterController.enabled = true;
            }
            //collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}