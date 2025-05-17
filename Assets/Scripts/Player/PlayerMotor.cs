using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 _playerVelocity;

    public float _speed = 5f;
    public float _gravity = 9.8f;
    public float _jumpHeight = 3f;

    private bool _isGrounded;

    // Crouch
    private bool _crouching;
    private bool _lerpCrouch;
    private float _crouchTimer;
    private float _crouchDuration = 1f;

    // Sprint
    private bool _sprinting;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        _isGrounded = _controller.isGrounded;

        // Reset vertical velocity if grounded
        if (_isGrounded && _playerVelocity.y < 0)
            _playerVelocity.y = -2f;

        // Apply gravity
        _playerVelocity.y += -_gravity * Time.deltaTime;

        // Apply vertical movement
        _controller.Move(_playerVelocity * Time.deltaTime);

        // Handle crouch transition
        if (_lerpCrouch)
        {
            _crouchTimer += Time.deltaTime;
            float p = _crouchTimer / _crouchDuration;

            if (_crouching)
                _controller.height = Mathf.Lerp(_controller.height, 1f, p);
            else
                _controller.height = Mathf.Lerp(_controller.height, 2f, p);

            if (p > 1f)
            {
                _lerpCrouch = false;
                _crouchTimer = 0f;
            }
        }
    }

    public void ProcessMove(Vector2 input)
    {
        float currentSpeed = _sprinting ? 8f : _speed;
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        _controller.Move(move * currentSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _playerVelocity.y = Mathf.Sqrt(2 * _jumpHeight * _gravity);
        }
    }

    public void SetCrouching(bool isCrouching)
    {
        if (_crouching == isCrouching) return;

        _crouching = isCrouching;
        _crouchTimer = 0f;
        _lerpCrouch = true;
    }


    public void SetSprinting(bool isSprinting)
    {
        _sprinting = isSprinting;
        _speed = _sprinting ? 8f : 5f;
    }
}
