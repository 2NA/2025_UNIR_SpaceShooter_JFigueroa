 using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerSpaceShip : MonoBehaviour
{
    [Header("Player Setup")]
    [SerializeField] public int lives = 3;
    [SerializeField] public int powerLevel = 1;

    [Header("HUD")]
    [SerializeField] TextMeshProUGUI currentLives;
    [SerializeField] TextMeshProUGUI currentPowerLevel;
    [SerializeField] TextMeshProUGUI distanceMeter;
    [SerializeField] TextMeshProUGUI currentLevel;
    [SerializeField] TextMeshProUGUI gameOver;

    [Header("Movement")]
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float acceleration = 300f;
    [SerializeField] float rawMoveThresholdForBreaking = 0.1f;

    [Header("Shooting")]
    [SerializeField] GameObject spawnPointTop = null;
    [SerializeField] GameObject spawnPointCenter = null;
    [SerializeField] GameObject spawnPointBottom = null;
    [SerializeField] GameObject slowProjectilePrefab = null;
    [SerializeField] GameObject fastProjectilePrefab = null;
    [SerializeField] GameObject missileProjectilePrefab = null;

    [Header("Controls")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference shoot;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        move.action.Enable();
        shoot.action.Enable();

        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        shoot.action.started += OnShoot;
        shoot.action.started -= OnReset;
    }

    Vector2 currentVelocity = Vector2.zero;
    private float score = 0.0f;
    private int level = 1;
    public float speedFactor = 0.2f;
    void Update()
    {
        DoMovement();
        StayInBounds();

        speedFactor += Time.deltaTime;
        score += Time.deltaTime * speedFactor;
        distanceMeter.text = ((int)score).ToString () + " mts";
                
        level = (int)score / 250 + 1;
        currentLevel.text = level.ToString ();
    }

    private void OnDisable()
    {
        move.action.Disable();
        shoot.action.Disable();
        
        move.action.started -= OnMove;
        move.action.performed -= OnMove;
        move.action.canceled -= OnMove;

        shoot.action.started -= OnShoot;
        shoot.action.started -= OnReset;
    }

    Vector2 rawMove;
    private void OnMove(InputAction.CallbackContext context)
    {
        rawMove = context.ReadValue<Vector2>();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (powerLevel > 3)
        {
            if (spawnPointTop && fastProjectilePrefab)
            {
                Instantiate(fastProjectilePrefab, spawnPointTop.transform.position, Quaternion.identity);
            }
        }
            
        if (powerLevel > 2)
        {
            if (spawnPointCenter && fastProjectilePrefab)
            {
                Instantiate(fastProjectilePrefab, spawnPointCenter.transform.position, Quaternion.identity);
            }
        }

        if (powerLevel > 1) 
        {
            if (spawnPointBottom && missileProjectilePrefab)
            {
                Instantiate(missileProjectilePrefab, spawnPointBottom.transform.position, Quaternion.identity);
            }
        }

        if (powerLevel < 3) 
        {
            if (spawnPointBottom && missileProjectilePrefab)
            {
                Instantiate(slowProjectilePrefab, spawnPointCenter.transform.position, Quaternion.identity);
            }
        }
    }

    private void OnReset(InputAction.CallbackContext context)
    {
        OnDisable();
        SceneManager.LoadScene(0);
    }

    private void DoMovement()
    {
        if (rawMove.magnitude < rawMoveThresholdForBreaking)
        {
            currentVelocity *= 0.01f * Time.deltaTime;
        }

        currentVelocity += rawMove * acceleration * Time.deltaTime;

        float linearVelocity = currentVelocity.magnitude;
        linearVelocity = Mathf.Clamp(linearVelocity, 0, maxSpeed);
        currentVelocity = currentVelocity.normalized * linearVelocity;

        transform.Translate(currentVelocity * Time.deltaTime);
    }

    private void StayInBounds()
    {
        float xClamped = Mathf.Clamp(transform.position.x, -1.6f, 1.6f);
        float yClamped = Mathf.Clamp(transform.position.y, -0.9f, 0.9f);

        transform.position = new Vector3(xClamped, yClamped, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyShot"))
        {
            lives--;
            
            switch(lives)
            {
                case 2:
                    currentLives.text = "--";
                    break;
                case 1:
                    currentLives.text = "-";
                    break;
                case 0:
                    currentLives.text = "";
                    
                    Time.timeScale = 0;
                    
                    gameOver.gameObject.SetActive(true);
                    Destroy(gameObject);
                    
                    OnDisable();
                    shoot.action.Enable();
                    shoot.action.started += OnReset;
                    break;
                default:
                    currentLives.text = "---";
                    
                    break;
            }
        }
        else if (collision.CompareTag("PlayerPowerUp"))
        {
            Destroy(collision.gameObject);

            powerLevel++;
            
            switch(powerLevel)
            {
                case 4:
                    currentPowerLevel.text = "----";
                    break;
                case 3:
                    currentPowerLevel.text = "---";
                    break;
                case 2:
                    currentPowerLevel.text = "--";
                    break;
                default:
                    currentPowerLevel.text = "-";
                    break;
            }
        }
    }
}
