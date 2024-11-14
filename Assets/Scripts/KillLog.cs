using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLog : MonoBehaviour
{
    public GameObject spawn;       // Zona de resurgimiento del jugador
    public GameObject spawnLog;    // Zona de resurgimiento del "log"
    public GameObject log;         // El objeto "log" que queremos teletransportar

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Objeto entro a la zona de muerte: " + collision.gameObject.name);

        // Si el objeto que entra en la zona es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("El objeto es el jugador, teletransportando a spawn");

            // Deshabilitar el controlador de personaje del jugador temporalmente para evitar problemas de colisión
            CharacterController characterController = collision.gameObject.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
            }

            // Teletransportar al jugador a la zona de resurgimiento
            collision.gameObject.transform.position = spawn.transform.position;

            // Habilitar el controlador de personaje del jugador nuevamente
            if (characterController != null)
            {
                characterController.enabled = true;
            }

            // Ahora teletransportamos el "log" a la posición de spawnLog
            if (log != null && spawnLog != null)
            {
                log.transform.position = spawnLog.transform.position;
                Debug.Log("Log teletransportado a la posición de spawnLog");
            }
        }
    }
}
