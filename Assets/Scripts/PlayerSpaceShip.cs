 using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpaceShip : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float acceleration = 300f;

    [Header("Shooting")]
    [SerializeField] GameObject projectilePrefab;

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
    const float rawMoveThresholdForBreaking = 0.1f;
    void Update()
    {
        if (rawMove.magnitude < rawMoveThresholdForBreaking)
        {
            currentVelocity *= 0.1f * Time.deltaTime;
        }

        currentVelocity += rawMove * acceleration * Time.deltaTime;

        float linearVelocity = currentVelocity.magnitude;
        linearVelocity = Mathf.Clamp(linearVelocity, 0, maxSpeed);
        currentVelocity = currentVelocity.normalized * linearVelocity;

        transform.Translate(currentVelocity * Time.deltaTime);
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
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}
