using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float baseMovementSpeed = 3f;

    float rawMovementSpeedModifier = 0f;
    public float BaseMovementSpeedModifier { get => rawMovementSpeedModifier; set {  rawMovementSpeedModifier = value; } }

    float movementSpeedMultiplier = 1f;
    public float MovementSpeedMultiplier { get => movementSpeedMultiplier; set {  movementSpeedMultiplier = value; } }

    private void Awake()
    {
        GetComponent<PlayerController>().OnMove += Move;
    }

    void Move(Vector2 movementDirection)
    {
        movementDirection.Normalize();
        Vector2 movementVector = (baseMovementSpeed + rawMovementSpeedModifier) * movementSpeedMultiplier * Time.deltaTime * movementDirection;

        transform.Translate(movementVector);
    }
}