using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
	public Image[] slots;
	public TMP_Text itemMessage;

	private string[] items = new string[4];
	private Sprite[] itemIcons = new Sprite[4];
	private int selectedSlot = 0;

	private float messageTimer = 0f;
	private const float messageDuration = 2f;

	void Start()
	{
		UpdateHotbar();
	}

	void Update()
	{
		HandleSlotSelection();
		HandleMessageTimer();
	}

	public void AddItem(string itemName, Sprite itemIcon)
	{
		for(int i = 0; i < items.Length; i++)
		{
			if(items[i] == null)
			{
				items[i] = itemName;
				itemIcons[i] = itemIcon;
				UpdateHotbar();
				ShowMessage($"Guardado: {itemName}");
				return;
			}
		}
		ShowMessage("Inventario lleno");
	}

	private void HandleSlotSelection()
	{
		int previousSlot = selectedSlot;

		for(int i = 0; i < 4; i++)
		{
			if(Input.GetKeyDown((i + 1).ToString()))
			{
				selectedSlot = i;
				break;
			}
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");
		if(scroll > 0f)
		{
			selectedSlot = (selectedSlot - 1 + 4) % 4;
		}
		else if(scroll < 0f)
		{
			selectedSlot = (selectedSlot + 1) % 4;
		}

		if(previousSlot != selectedSlot)
		{
			UpdateHotbar();
		}
	}

	private void UpdateHotbar()
	{
		for(int i = 0; i < slots.Length; i++)
		{
			slots[i].color = (i == selectedSlot) ? Color.yellow : Color.white;
			Image icon = slots[i].transform.GetChild(0).GetComponent<Image>();
			icon.sprite = itemIcons[i];
			icon.enabled = itemIcons[i] != null;
		}
	}

	private void ShowMessage(string message)
	{
		itemMessage.text = message;
		itemMessage.gameObject.SetActive(true);
		messageTimer = messageDuration;
	}

	private void HandleMessageTimer()
	{
		if(messageTimer > 0f)
		{
			messageTimer -= Time.deltaTime;
			if(messageTimer <= 0f)
			{
				itemMessage.gameObject.SetActive(false);
			}
		}
	}
}
