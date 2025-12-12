using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject projectilePrefab;
    Vector3 linearVelocity = Vector3.left;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnShot());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -1.3f)
        {
            flipCharacter();
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

    void flipCharacter()
    {
        Vector3 scale = transform.localScale;
        scale.x = -transform.localScale.x;
        transform.localScale = scale;
    }

    IEnumerator SpawnShot()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(projectilePrefab, spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }
}
