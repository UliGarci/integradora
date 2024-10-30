using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public int maxCapacity = 5; // Máxima capacidad del inventario
    public List<GameObject> items = new List<GameObject>(); // Lista de objetos almacenados
    public List<Button> hotbarSlots; // Referencia a los botones de la hotbar

    // Añadir un objeto al inventario
    public void AddItem(GameObject item) {
        if (items.Count < maxCapacity) {
            items.Add(item);
            UpdateHotbar();
            Destroy(item); // Destruye el objeto en la escena
        } else {
            Debug.Log("Inventario lleno");
        }
    }

    // Usar un objeto del inventario por índice (1-5)
    public void UseItem(int index) {
        if (index >= 0 && index < items.Count) {
            GameObject item = Instantiate(items[index]); // Crear una instancia del objeto
            items.RemoveAt(index); // Remover del inventario
            UpdateHotbar(); // Actualizar la UI de la hotbar

            // Buscar y hacer que el jugador sostenga el objeto
            FPSInteraction player = FindObjectOfType<FPSInteraction>();
            if (player != null) {
                player.HoldItem(item);
            }
        } else {
            Debug.Log("Índice inválido o inventario vacío.");
        }
    }

    // Actualizar la hotbar visualmente
    private void UpdateHotbar() {
        for (int i = 0; i < hotbarSlots.Count; i++) {
            if (hotbarSlots[i] != null && hotbarSlots[i].GetComponentInChildren<Text>() != null) {
                if (i < items.Count) {
                    hotbarSlots[i].GetComponentInChildren<Text>().text = items[i].name;
                } else {
                    hotbarSlots[i].GetComponentInChildren<Text>().text = "";
                }
            }
        }
    }
}
