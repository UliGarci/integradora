using StarterAssets;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce = 10f; // Fuerza hacia arriba
    private float originalJump = 0f;

    private void OnTriggerEnter(Collider other)
    {
        // Debug para verificar la colisión
        Debug.Log($"Trigger detectado con: {other.gameObject.name}");

        // Comprobar si el objeto colisionado tiene la etiqueta "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            originalJump = other.GetComponentInParent<FirstPersonController>().JumpHeight;
            Debug.Log("Entra el juegador");
            Debug.Log(other.transform.parent.name);
            other.GetComponentInParent<FirstPersonController>().JumpHeight = jumpForce;
            other.GetComponentInParent<FirstPersonController>().trampolin = true;
            other.GetComponentInParent<FirstPersonController>().JumpAndGravity();
            other.GetComponentInParent<FirstPersonController>().trampolin = false;
            other.GetComponentInParent<FirstPersonController>().JumpHeight = originalJump;

        }
        else
        {
            Debug.Log("El objeto colisionado no tiene la etiqueta 'Player'.");
        }
    }
}
