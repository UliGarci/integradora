using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSInteraction : MonoBehaviour{
	public float maxDistance = 5f; // Máxima distancia para interactuar
    public Transform holdPoint; // Punto donde se sostiene el objeto
    public TMP_Text instructionText; // Texto de instrucciones
    private Rigidbody heldObjectRB; // Referencia al objeto que se sostiene
    private Inventory inventory; // Referencia al inventario del jugador

    void Start()
    {
        inventory = GetComponent<Inventory>();
        instructionText.gameObject.SetActive(false); // Ocultar el texto al inicio
    }

    void Update()
    {
        CheckForObject();

        // Soltar el objeto si se deja de presionar clic derecho
        if (Input.GetMouseButtonUp(1) && heldObjectRB != null)
        {
            ReleaseObject();
        }

        // Usar objetos del inventario con las teclas 1-5
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                inventory.UseItem(i);
            }
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
                instructionText.text = "Right-click to pick up, E to store"; // Mostrar instrucciones
                instructionText.gameObject.SetActive(true);

                // Mantener el objeto agarrado si se mantiene clic derecho
                if (Input.GetMouseButton(1))
                {
                    HoldItem(hit.collider.gameObject);
                }

                // Almacenar el objeto en el inventario con "E"
                if (Input.GetKeyDown(KeyCode.E))
                {
                    inventory.AddItem(hit.collider.gameObject);
                }
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
            heldObjectRB.useGravity = false;
            heldObjectRB.drag = 10f;
            item.transform.position = holdPoint.position;
            item.transform.rotation = holdPoint.rotation;
            item.transform.parent = holdPoint;
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
}