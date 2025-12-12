using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    [SerializeField] float verticalPosition = 0.1f;
    Vector3 linearVelocity = Vector3.up;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.y < verticalPosition)
        {
            transform.Translate(linearVelocity * speed * Time.deltaTime);
        }
    }
}
