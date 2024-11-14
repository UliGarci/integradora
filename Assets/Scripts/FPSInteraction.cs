using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSInteraction : MonoBehaviour
{
    public float maxDistance = 5f; // Máxima distancia para interactuar
    public Transform holdPoint; // Punto donde se sostiene el objeto
    public TMP_Text instructionText; // Texto de instrucciones
    private Rigidbody heldObjectRB; // Referencia al objeto que se sostiene
    private Inventory inventory; // Referencia al inventario del jugador
    private bool firstInteraction = false;

    private bool isThrowing = false; // Variable para evitar volver a sostener después de lanzar
    public float throwForce = 500f; // Fuerza de lanzamiento

    void Start()
    {
        inventory = GetComponent<Inventory>();
        instructionText.gameObject.SetActive(false); // Ocultar el texto al inicio
    }

    void Update()
    {
        CheckForObject();

        // Lanzar el objeto si se presiona el clic derecho
        if (Input.GetMouseButtonDown(1) && heldObjectRB != null)
        {
            ThrowObject();
            return; // Evitar cualquier acción posterior en este frame
        }

        // Usar objetos del inventario con las teclas 1-5
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                inventory.UseItem(i);
            }
        }

        // Agarrar y soltar el objeto con clic izquierdo
        if (Input.GetMouseButtonDown(0) && heldObjectRB == null && !isThrowing)
        {
            CheckForObject(); // Verifica si se puede agarrar un objeto cuando se hace clic izquierdo
        }
        if (Input.GetMouseButton(0) && heldObjectRB != null) // Mantener el objeto agarrado mientras se mantenga clic izquierdo
        {
            // Mantener el objeto en la posición del punto de agarre
            heldObjectRB.transform.position = holdPoint.position;
        }
        if (Input.GetMouseButtonUp(0) && heldObjectRB != null) // Liberar el objeto cuando se suelta clic izquierdo
        {
            ReleaseObject();
        }
    }

    void CheckForObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                if (!firstInteraction)
                {
                    instructionText.text = "Clic izquierdo para sostener, E para almacenar, clic derecho para lanzar"; // Mostrar instrucciones
                    instructionText.gameObject.SetActive(true);
                }

                // Agarrar el objeto si no se sostiene uno ya
                HoldItem(hit.collider.gameObject);
            }
        }
        else
        {
            instructionText.gameObject.SetActive(false); // Ocultar el texto si no hay objeto cerca
        }
    }

    public void HoldItem(GameObject item)
    {
        if (heldObjectRB == null) // Solo agarrar si no hay otro objeto sostenido
        {
            heldObjectRB = item.GetComponent<Rigidbody>();
            if (heldObjectRB == null)
            {
                Debug.LogWarning("El objeto no tiene un componente Rigidbody.");
                return;
            }
            heldObjectRB.useGravity = false;
            heldObjectRB.drag = 10f;
            item.transform.position = holdPoint.position;
            item.transform.rotation = holdPoint.rotation;
            item.transform.parent = holdPoint;
            firstInteraction = true;
        }
    }

    void ReleaseObject()
    {
        if (heldObjectRB != null)
        {
            heldObjectRB.useGravity = true;
            heldObjectRB.drag = 0f;
            heldObjectRB.transform.parent = null;
            heldObjectRB = null; // Liberar la referencia
        }
    }

    void ThrowObject()
    {
        if (heldObjectRB != null) // Asegurarse de que hay un objeto sostenido
        {
            ReleaseObject(); // Liberar el objeto antes de lanzar
            heldObjectRB.AddForce(Camera.main.transform.forward * throwForce); // Aplicar la fuerza de lanzamiento
            isThrowing = true; // Activar el bloqueo temporal para evitar agarrar de nuevo
            Invoke("ResetThrowing", 0.2f); // Desactivar el bloqueo después de un corto tiempo
        }
    }

    void ResetThrowing()
    {
        isThrowing = false; // Desactivar el bloqueo temporal para poder agarrar de nuevo
    }
}
