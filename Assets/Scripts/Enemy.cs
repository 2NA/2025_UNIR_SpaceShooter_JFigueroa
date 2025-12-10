using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Vector3 linearVelocity = Vector3.left;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -1.3f)
        {
            linearVelocity = Vector3.right;
        }
        if (transform.position.x > 2.5f)
        {
            Destroy(gameObject);
        }

        transform.Translate(linearVelocity * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerShot"))
        {
            Destroy(gameObject);
        }
    }
}
