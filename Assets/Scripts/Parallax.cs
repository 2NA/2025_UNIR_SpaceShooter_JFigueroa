using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float imageWidth;
    private Vector3 initalPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float remaining = speed * Time.time % imageWidth;
        
        transform.position = initalPosition + remaining * direction;
    }
}
