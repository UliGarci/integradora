using UnityEngine;

public class PlatformMoveYZ : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float moveDistance = 3f;

    private Vector3 startPosition;
    private bool movingPositive;

    void Start()
    {
        startPosition = transform.position;
        movingPositive = Random.value >= 0.5f;
    }

    void Update()
    {
        if (movingPositive)
        {
            transform.position += (Vector3.up + Vector3.forward) * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(startPosition, transform.position) >= moveDistance)
            {
                movingPositive = false;
            }
        }
        else
        {
            transform.position += (Vector3.down + Vector3.back) * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(startPosition, transform.position) >= moveDistance)
            {
                movingPositive = true;
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
