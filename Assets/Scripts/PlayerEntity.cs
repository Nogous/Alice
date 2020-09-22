using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector3 movement;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (movement == Vector3.zero) return;

        transform.localPosition += movement * Time.deltaTime * moveSpeed;
    }
}
