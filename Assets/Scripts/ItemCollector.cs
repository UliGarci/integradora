using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    public string itemName; // Nombre del objeto
    public Sprite itemIcon; // Ícono del objeto
    public string hintMessage = "Press 'E' to pick up"; // Mensaje tutorial
    public TextMeshProUGUI hintText; // Referencia al texto del mensaje tutorial

    private void Start()
    {
        // Ocultar el mensaje al iniciar
        if (hintText != null)
        {
            hintText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Mostrar el mensaje de interacción
            ShowHint();

            // Recoger el objeto al presionar "E"
            if (Input.GetKeyDown(KeyCode.E))
            {
                InventorySystem inventory = FindObjectOfType<InventorySystem>();
                if (inventory != null)
                {
                    inventory.AddItem(itemName, itemIcon);
                    HideHint(); // Ocultar el mensaje al recoger el objeto
                    Destroy(gameObject); // Eliminar el objeto de la escena
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Ocultar el mensaje al salir del rango del objeto
            HideHint();
        }
    }

    private void ShowHint()
    {
        if (hintText != null)
        {
            hintText.text = hintMessage;
            hintText.gameObject.SetActive(true);
        }
    }

    private void HideHint()
    {
        if (hintText != null)
        {
            hintText.gameObject.SetActive(false);
        }
    }
}
