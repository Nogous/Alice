using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    #region Script Parameters

    [Header("Speed")]
    public float initMoveSpeed = 5f;
    public float moveSpeedMax = 10f;
    public float moveSpeedMin = 1f;
    public float acceleration = .5f;
    [SerializeField] private float currentSpeed = 5f;
    public float looseSpeed = .5f;

    [Header("life")]
    public int life = 5;
    public float invicibilityFrameDuration = .1f;
    private bool isInvincible = false;
    public int takeDamage = 1;

    private Vector3 movement;

    [Header("PathFollowers")]
    public List<PathFollower> followers;

    #endregion

    #region Unity Methods

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
        if (currentSpeed < moveSpeedMax)
        {
            currentSpeed += acceleration * Time.fixedDeltaTime;
        }

        foreach(PathFollower follower in followers)
        {
            follower.speed = currentSpeed;
        }

        if (movement == Vector3.zero) return;

        transform.localPosition += movement * Time.deltaTime * initMoveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("It hurts");

        if (collision.gameObject.tag == "Obstacle")
        {
            SpeedDown(looseSpeed);
            TakeDamage(takeDamage);

            Destroy(collision.gameObject);
        }
    }

    #endregion

    #region Damage

    private void TakeDamage(int dammage)
    {
        life -= dammage;
        Debug.LogError(life);
        if (life<= 0)
        {
            GameManager.instance.PlayerDead();
        }
        isInvincible = true;
        StartCoroutine(InvincibilityStop());
    }

    #endregion

    #region Speed

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
