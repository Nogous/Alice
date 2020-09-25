using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    [SerializeField] private float _minSpeed = 30;
    [SerializeField] private float _maxSpeed = 55;
    private float _currentSpeed = 2;

    private PlayerEntity _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = PlayerEntity.instance;
        _currentSpeed = _minSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _currentSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_currentSpeed < _maxSpeed && _player.canSpeedUp)
        {
            _currentSpeed += _player.acceleration * 2 * Time.fixedDeltaTime;
        }
        else if (_currentSpeed > _minSpeed && !_player.canSpeedUp)
        {
            _currentSpeed -= _player.deceleration * 2 * Time.fixedDeltaTime;
        }
    }
}
