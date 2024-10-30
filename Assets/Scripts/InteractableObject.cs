using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour{
	void Start() {
		gameObject.tag = "Interactable";
	}
}