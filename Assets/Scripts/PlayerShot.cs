using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] float speed = 1.5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
