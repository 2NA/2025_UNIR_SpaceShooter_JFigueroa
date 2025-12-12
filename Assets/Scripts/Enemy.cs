using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentLevel;
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject spawnPointTop = null;
    [SerializeField] GameObject spawnPointCenter = null;
    [SerializeField] GameObject spawnPointBottom = null;
    [SerializeField] GameObject projectilePrefab = null;
    [SerializeField] Vector3 linearVelocity = Vector3.left;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (linearVelocity == Vector3.up)
        {
            transform.Rotate(new Vector3(0,0,-90));
            linearVelocity = Vector3.left;
        }
        StartCoroutine(SpawnShot());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -1.3f && !this.name.Contains("EnemyDragon"))
        {
            flipCharacter();
            linearVelocity = Vector3.right;
        }
        if (transform.position.x > 2.5f || transform.position.x < -2.5f || transform.position.y > 1.5)
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
        if (projectilePrefab)
        {
            for (int i = 0; i < 5; i++)
            {
                if (spawnPointCenter)
                {
                    Instantiate(projectilePrefab, spawnPointCenter.transform.position, Quaternion.identity);
                }
                if (spawnPointTop)
                {
                    Instantiate(projectilePrefab, spawnPointTop.transform.position, Quaternion.identity);
                }
                if (spawnPointBottom)
                {
                    Instantiate(projectilePrefab, spawnPointBottom.transform.position, Quaternion.identity);
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
