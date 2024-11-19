using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float moveDistance = 3f;

    private Vector3 startPosition;
    private bool movingRight;

    void Start()
    {
        startPosition = transform.position;
        movingRight = Random.value >= 0.5f;
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            if (transform.position.x >= startPosition.x + moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            if (transform.position.x <= startPosition.x - moveDistance)
            {
                movingRight = true;
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
