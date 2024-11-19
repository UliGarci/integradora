using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce = 10f; // Fuerza hacia arriba

    private void OnTriggerEnter(Collider other)
    {
        // Debug para verificar la colisión
        Debug.Log($"Trigger detectado con: {other.gameObject.name}");

        // Comprobar si el objeto colisionado tiene la etiqueta "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // Obtener el Rigidbody del objeto padre (jugador)
            Rigidbody rb = other.gameObject.GetComponentInParent<Rigidbody>();

            if (rb != null)
            {
                // Debug para confirmar que se aplica la fuerza
                Debug.Log("Se aplica fuerza hacia arriba");

                // Aplicar una fuerza hacia arriba
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("El jugador no tiene un Rigidbody.");
            }
        }
        else
        {
            Debug.Log("El objeto colisionado no tiene la etiqueta 'Player'.");
        }
    }
}
