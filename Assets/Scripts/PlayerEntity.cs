using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    #region singleton
    public static PlayerEntity instance;
    #endregion

    #region Script Parameters

    [Header("Speed")]
    public float initMoveSpeed = 5f;
    public float moveSpeedMax = 10f;
    public float moveSpeedMin = 1f;
    public float acceleration = .5f;
    public float deceleration = 1f;
    [SerializeField] private float currentSpeed = 5f;
    public float looseSpeed = .5f;
    private bool canSpeedUp = false;
    private float[] _musicPallier;
    private bool[] _musicPallierReached;
    public int previousPallier = 0;

    [Header("inertie")]
    public bool inertieOff = false;

    public float speedPlan = 1f;
    public float inertieForce = 1;
    private Vector3 inertie = Vector3.zero;

    [Header("life")]
    public int life = 5;
    public float invicibilityFrameDuration = .1f;
    private bool isInvincible = false;
    public int takeDamage = 1;

    private Vector3 movement;

    [Header("PathFollowers")]
    public List<PathFollower> followers;

    public Camera cam;
    public Vector3 offsetRotationAlice;
    private float offsetCamToAlice;

    public float colisionRange = 5f;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        currentSpeed = initMoveSpeed;
        offsetCamToAlice = cam.transform.localPosition.z;

        previousPallier = 0;
        _musicPallier = new float[5];
        _musicPallierReached = new bool[5];
        for(int i = 0; i < _musicPallier.Length; i++)
        {
            _musicPallier[i] = (i) * (moveSpeedMax / 5);
            _musicPallierReached[i] = false;
        }

        MiniGameManager.instance.onChangeState += () =>
        {
            if(MiniGameManager.instance.state == State.NONE)
            {
                canSpeedUp = false;
            }
            else
            {
                canSpeedUp = true;
            }
        };

    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(!InterfaceManager.instance.isInPause)
            UpdateRotation();
    }

    private void FixedUpdate()
    {
        if (currentSpeed < moveSpeedMax && canSpeedUp)
        {
            currentSpeed += acceleration * Time.fixedDeltaTime;
            if(previousPallier < _musicPallier.Length)
            {
                if (!_musicPallierReached[previousPallier] && currentSpeed > _musicPallier[previousPallier])
                {
                    _musicPallierReached[previousPallier] = true;
                    previousPallier += 1;
                }
            }
        }

        if (currentSpeed > moveSpeedMin && !canSpeedUp)
        {
            currentSpeed -= deceleration * Time.fixedDeltaTime;
            if (previousPallier >= 0)
            {
                if (_musicPallierReached[previousPallier] && currentSpeed > _musicPallier[previousPallier])
                {
                    _musicPallierReached[previousPallier] = false;
                    previousPallier -= 1;
                }
            }
        }

        foreach(PathFollower follower in followers)
        {
            follower.speed = currentSpeed;
        }

        //if (movement == Vector3.zero) return;
        if (!inertieOff)
        {
            Debug.DrawRay(transform.position, inertie, Color.blue);
            inertie -= inertie.normalized * Time.deltaTime / inertieForce;
            Debug.DrawRay(transform.position, -inertie, Color.red);
            inertie += movement / (10 * Time.deltaTime);
            if (inertie.magnitude > inertie.normalized.magnitude)
            {
                inertie = inertie.normalized;
            }


            //mouvement fix
            transform.localPosition += inertie * Time.deltaTime * speedPlan;
        }
        else
        {
            transform.localPosition += movement * Time.deltaTime * speedPlan;
        }

        Vector3 tmp = transform.localPosition.normalized * colisionRange;
        if (transform.localPosition.magnitude >= (tmp.magnitude))
        {
            transform.localPosition = transform.localPosition.normalized * colisionRange;
        }


        if (MiniGameManager.instance.state != State.NONE && MiniGameManager.instance.state != State.TUTO)
        {
            if (!AudioManager.instance.CheckAudioClip(previousPallier) && AudioManager.instance.canPlayMusic)
            {
                AudioManager.instance.Play("Music", previousPallier, true);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("It hurts");

        if (collision.gameObject.tag == "Obstacle")
        {
            SpeedDown(looseSpeed);
            TakeDamage(takeDamage);

            Destroy(collision.gameObject);
            AudioManager.instance.Play("BodyImpact");
        }
        PortalPathToPath tmpPath = collision.gameObject.GetComponent<PortalPathToPath>();
        if (tmpPath != null)
        {
            Debug.Log("hit portal");
            Debug.Log(tmpPath.idNextPath);
            MasterPath.instance.SwitchMainPath(tmpPath.idNextPath);
            collision.gameObject.SetActive(false);
        }
    }

    #endregion

    #region Damage

    private void TakeDamage(int dammage)
    {
        life -= dammage;
        //Debug.LogError(life);
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

    void UpdateRotation()
    {
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(new Vector3( Screen.width-Input.mousePosition.x, Screen.height- Input.mousePosition.y, offsetCamToAlice));

        Vector3 difference = (mouseWorldPos - transform.position).normalized;

        // passage en repere d'alice
        difference = transform.parent.transform.InverseTransformDirection(difference);

        float angleX = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.localEulerAngles = new Vector3(angleX, 0, 0) + offsetRotationAlice;

        Debug.DrawRay(mouseWorldPos, Vector3.one, Color.blue);
    }

    

    private IEnumerator InvincibilityStop()
    {
        yield return new WaitForSeconds(invicibilityFrameDuration);
        isInvincible = false;
    }
}
