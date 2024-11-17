using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThrowObject : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    bool beingCarried = false;
    private bool touched = false;

    private float holdTime = 0f;
    private float maxHoldTime = 5f;
    private float minThrowForce = 200f;
    private float maxThrowForce = 2000f;

    public Image chargeImage; // Usa una imagen en lugar de Scrollbar
    public TextMeshProUGUI instructionsText;

    private bool hasSeenInstructions = false;
    private float maxChargeWidth = 100f; // Ancho máximo de la barra de carga

    void Start()
    {
        chargeImage.gameObject.SetActive(false);
        chargeImage.rectTransform.sizeDelta = new Vector2(0f, chargeImage.rectTransform.sizeDelta.y); // Inicia en 0 de ancho
        instructionsText.gameObject.SetActive(false);
    }

    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);

        if (dist <= 2.5f && !hasSeenInstructions)
        {
            instructionsText.gameObject.SetActive(true);
        }
        else
        {
            instructionsText.gameObject.SetActive(false);
        }

        if (dist <= 2.5f && Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            beingCarried = true;
            chargeImage.gameObject.SetActive(true);

            if (!hasSeenInstructions)
            {
                hasSeenInstructions = true;
                instructionsText.gameObject.SetActive(false);
            }
        }

        if (beingCarried)
        {
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
                chargeImage.gameObject.SetActive(false);
            }

            if (Input.GetMouseButton(0))
            {
                holdTime += Time.deltaTime;
                holdTime = Mathf.Clamp(holdTime, 0f, maxHoldTime);
                
                // Calcula el ancho de la barra de carga en función de `holdTime`
                float chargeAmount = holdTime / maxHoldTime;
                chargeImage.rectTransform.sizeDelta = new Vector2(Mathf.Lerp(0, maxChargeWidth, chargeAmount), chargeImage.rectTransform.sizeDelta.y);
            }

            if (Input.GetMouseButtonUp(0))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;

                float throwMultiplier = holdTime / maxHoldTime;
                float appliedForce = Mathf.Lerp(minThrowForce, maxThrowForce, throwMultiplier);

                GetComponent<Rigidbody>().AddForce(playerCam.forward * appliedForce);

                holdTime = 0f;
                chargeImage.rectTransform.sizeDelta = new Vector2(0f, chargeImage.rectTransform.sizeDelta.y);
                chargeImage.gameObject.SetActive(false);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                holdTime = 0f;
                chargeImage.rectTransform.sizeDelta = new Vector2(0f, chargeImage.rectTransform.sizeDelta.y);
                chargeImage.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter()
    {
        if (beingCarried)
        {
            touched = true;
        }
    }
}
