using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarMensajeConCollider : MonoBehaviour
{
    public string activadorName;
    public GameObject canvas;
    public Collider blockCollider; // Collider que se activará para bloquear la entrada

    private void Start()
    {
        // Desactiva el canvas inicialmente
        if (canvas != null)
        {
            canvas.SetActive(false);
        }

        // Asegúrate de que el collider esté desactivado al inicio
        if (blockCollider != null)
        {
            blockCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            if (canvas != null)
            {
                canvas.SetActive(true); // Muestra el canvas
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canvas != null)
            {
                canvas.SetActive(false);
            }

            if (blockCollider != null)
            {
                blockCollider.enabled = true;
            }
        }
    }
}
