using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    [Header("Speed")]
    public float initMoveSpeed = 5f;
    public float moveSpeedMax = 10f;
    public float moveSpeedMin = 1f;
    private float currentSpeed = 5f;

    [Header("life")]
    public int life = 5;
    public float invicibilityFrameDuration = .1f;
    private bool isInvincible = false;

    private Vector3 movement;

    private void Start()
    {
        currentSpeed = initMoveSpeed;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (movement == Vector3.zero) return;

        transform.localPosition += movement * Time.deltaTime * initMoveSpeed;
    }

    private void TakeDamage(int dammage)
    {
        life -= dammage;
        if (life<= 0)
        {
            GameManager.instance.PlayerDead();
        }
        isInvincible = true;
        StartCoroutine(InvincibilityStop());
    }

    #region speed
    public void SpeedDown(float amont)
    {
        currentSpeed -= amont;
        if (currentSpeed < moveSpeedMin)
        {
            currentSpeed = moveSpeedMin;
        }
    }

    public void SpeedUp(float amont)
    {
        currentSpeed += amont;
        if (currentSpeed > moveSpeedMax)
        {
            currentSpeed = moveSpeedMax;
        }
    }
    #endregion

    private IEnumerator InvincibilityStop()
    {
        yield return new WaitForSeconds(invicibilityFrameDuration);
        isInvincible = false;
    }
}
