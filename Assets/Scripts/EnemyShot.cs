using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [SerializeField] float speed = 1.5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);

        if (transform.position.x < -2.5f)
        {
            Destroy(gameObject);
        }
    }
}
