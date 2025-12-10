
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    public enum SpawnMode
    {
        Line,
        Points,
    }

    [SerializeField] SpawnMode spawnMode;

    [SerializeField] Transform spawnLineTop;
    [SerializeField] Transform spawnLineBottom;
    
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float spawnSpeed = 0.7f;
    [SerializeField] int numEnemies = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (spawnMode == SpawnMode.Line)
        {
            StartCoroutine(LineSpawning());
        } else if (spawnMode == SpawnMode.Points)
        {
            StartCoroutine(PointSpawning());
        }
    }

    IEnumerator LineSpawning()
    {
        Vector3 lineTop = spawnLineTop.position;
        Vector3 lineBottom = spawnLineBottom.position;

        for (int i = 0; i < numEnemies; i++)
        {
            float t = Random.Range(0f, 1f);
            Vector3 startPosition = Vector3.Lerp(lineTop, lineBottom, t);

            Instantiate(enemyPrefab, startPosition, Quaternion.identity);
            
            yield return new WaitForSeconds(spawnSpeed);  // Retardo de 1 segundo
        }
    }

    IEnumerator PointSpawning()
    {
        int numPoints = spawnPoints.Length;
        
        for (int i = 0; i < numEnemies; i++)
        {
            int j = Random.Range(0, numPoints);
            Vector3 startPosition = spawnPoints[j].position;

            Instantiate(enemyPrefab, startPosition, Quaternion.identity);
            
            yield return new WaitForSeconds(spawnSpeed);  // Retardo de 1 segundo
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
