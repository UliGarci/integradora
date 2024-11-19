using UnityEngine;

public class PlatformTilt : MonoBehaviour
{
    public float tiltSpeed = 10f; // Velocidad de inclinación
    public float maxTiltAngle = 70f; // Ángulo máximo de inclinación
    private bool isTilting = false;
    private Quaternion originalRotation;
    private Quaternion targetRotation;

    void Start(){
        originalRotation = transform.rotation;
        targetRotation = originalRotation;
    }

    void Update()
    {
        if (isTilting){
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
        }else{
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * tiltSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float relativePositionX = other.transform.position.x - transform.position.x;
            float tiltAngle = -Mathf.Sign(relativePositionX) * maxTiltAngle;

            isTilting = true;
            targetRotation = Quaternion.Euler(0, 0, tiltAngle) * originalRotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTilting = false;
        }
    }
}
