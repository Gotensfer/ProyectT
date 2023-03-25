using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action<Vector2> OnMove;

    private void Update()
    {
        Vector2 directionalInput = new Vector2
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = Input.GetAxisRaw("Vertical")
        };

        if (directionalInput.sqrMagnitude > 0) OnMove(directionalInput);
    }
}