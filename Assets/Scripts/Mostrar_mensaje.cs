using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mostrar_mensaje : MonoBehaviour
{
    public string activadorname;
    public GameObject canvas;

    private void Start()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canvas != null)
            {
                canvas.SetActive(true);
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
        }
    }
}
