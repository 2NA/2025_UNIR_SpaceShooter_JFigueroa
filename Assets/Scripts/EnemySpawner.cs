
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] TextMeshProUGUI currentLevel;

    [Header("Enemy Type")]
    [SerializeField] GameObject enemyPrefab;

    [Header("Spawning")]
    [SerializeField] SpawnMode spawnMode;

    [SerializeField] Transform spawnLineTop;
    [SerializeField] Transform spawnLineBottom;
    
    [SerializeField] Transform[] spawnPoints = null;
    [SerializeField] float spawnSpeed = 0.7f;
    [SerializeField] int numEnemies = 10;

    public enum SpawnMode
    {
        Line,
        Points,
    }

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

        do
        {
            for (int i = 0; i < numEnemies * int.Parse(currentLevel.text); i++)
            {
                float t = Random.Range(0f, 1f);
                Vector3 startPosition = Vector3.Lerp(lineTop, lineBottom, t);

                Instantiate(enemyPrefab, startPosition, Quaternion.identity);
                
                yield return new WaitForSeconds(spawnSpeed);
            }
            yield return new WaitForSeconds(spawnSpeed * int.Parse(currentLevel.text));
        } while (true);
    }

    IEnumerator PointSpawning()
    {
        int numPoints = spawnPoints.Length;
        
        do
        {
            yield return new WaitForSeconds(spawnSpeed * int.Parse(currentLevel.text));
            for (int i = 0; i < numEnemies * int.Parse(currentLevel.text); i++)
            {
                yield return new WaitForSeconds(spawnSpeed);

                int j = Random.Range(0, numPoints);
                Vector3 startPosition = spawnPoints[j].position;

                Instantiate(enemyPrefab, startPosition, Quaternion.identity);
            }
        } while (true);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
