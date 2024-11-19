using UnityEngine;

public class PlatformMoveXZ : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float moveDistance = 3f;

    private Vector3 startPosition;
    private bool movingForward;

    void Start()
    {
        startPosition = transform.position;
        movingForward = Random.value >= 0.5f;
    }

    void Update()
    {
        if (movingForward)
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
            if (transform.position.z >= startPosition.z + moveDistance)
            {
                movingForward = false;
            }
        }
        else
        {
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
            if (transform.position.z <= startPosition.z - moveDistance)
            {
                movingForward = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.collider.GetComponent<Rigidbody>();
            if (playerRigidbody != null && playerRigidbody.isKinematic)
            {
                collision.collider.transform.SetParent(transform);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
