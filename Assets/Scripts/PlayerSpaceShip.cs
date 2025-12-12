 using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpaceShip : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] TextMeshProUGUI distanceMeter;
    [SerializeField] TextMeshProUGUI currentLevel;

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

    private void OnEnable()
    {
        move.action.Enable();
        shoot.action.Enable();

        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        shoot.action.started += OnShoot;
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
    }

    Vector2 rawMove;
    private void OnMove(InputAction.CallbackContext context)
    {
        rawMove = context.ReadValue<Vector2>();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (spawnPointTop && fastProjectilePrefab)
        {
            Instantiate(fastProjectilePrefab, spawnPointTop.transform.position, Quaternion.identity);
        }

        if (spawnPointCenter && slowProjectilePrefab)
        {
            Instantiate(slowProjectilePrefab, spawnPointCenter.transform.position, Quaternion.identity);
        }

        if (spawnPointBottom && missileProjectilePrefab)
        {
            Instantiate(missileProjectilePrefab, spawnPointBottom.transform.position, Quaternion.identity);
        }
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
}
