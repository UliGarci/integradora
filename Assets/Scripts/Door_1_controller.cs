using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_1_controller : MonoBehaviour
{
    public Animator door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door.Play("door_open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door.Play("door_close");
        }
    }
}
