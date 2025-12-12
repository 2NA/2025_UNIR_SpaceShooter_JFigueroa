using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] float speed = 1.5f;
    [SerializeField] Vector3 direction = Vector3.right;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.x > 2.5f || transform.position.x < -2.5f || transform.position.y < -1.5f)
        {
            Destroy(gameObject);
        }
    }
}
