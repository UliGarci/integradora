using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThrowObject : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public float throwForce = 10f;
    bool hasPlayer = false;
    bool beingCarried = false;
    public int dmg;
    private bool touched = false;

    private float holdTime = 0f;
    private float maxHoldTime = 5f;
    private float minThrowForce = 200f;
    private float maxThrowForce = 2000f;

    public Image chargeBar;
    public TextMeshProUGUI instructionsText; 

    private bool hasSeenInstructions = false; 

    void Start()
    {
        chargeBar.gameObject.SetActive(false);  
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
            chargeBar.gameObject.SetActive(true);

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
                chargeBar.gameObject.SetActive(false);
            }

            if (Input.GetMouseButton(0))
            {
                holdTime += Time.deltaTime;
                holdTime = Mathf.Clamp(holdTime, 0f, maxHoldTime);
                float chargeAmount = holdTime / maxHoldTime;
                chargeBar.fillAmount = chargeAmount;
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
                chargeBar.fillAmount = 0f;
                chargeBar.gameObject.SetActive(false);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                holdTime = 0f;
                chargeBar.fillAmount = 0f;
                chargeBar.gameObject.SetActive(false);
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
