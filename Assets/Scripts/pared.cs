using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pared : MonoBehaviour
{
    public GameObject destructionEffect;

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que colisiona tiene la etiqueta "martillote"
        if (collision.gameObject.CompareTag("martillote"))
        {
            // Si hay un efecto de destrucción configurado, lo instancia
            if (destructionEffect != null)
            {
                Instantiate(destructionEffect, transform.position, Quaternion.identity);
            }

            // Destruye el objeto al que está asignado este script
            Destroy(gameObject);
        }
    }
}
