 using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpaceShip : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float acceleration = 300f;
    [SerializeField] float rawMoveThresholdForBreaking = 0.1f;

    [Header("Shooting")]
    [SerializeField] GameObject spawnPointTop;
    [SerializeField] GameObject spawnPointCenter;
    [SerializeField] GameObject spawnPointBottom;
    [SerializeField] GameObject slowProjectilePrefab;
    [SerializeField] GameObject fastProjectilePrefab;
    [SerializeField] GameObject missileProjectilePrefab;

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
    void Update()
    {
        DoMovement();
        StayInBounds();
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
        Instantiate(slowProjectilePrefab, spawnPointCenter.transform.position, Quaternion.identity);
        Instantiate(fastProjectilePrefab, spawnPointTop.transform.position, Quaternion.identity);
        Instantiate(missileProjectilePrefab, spawnPointBottom.transform.position, Quaternion.identity);
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
