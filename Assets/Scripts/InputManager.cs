using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    public PlayerInput.OnFootActions _onFoot;

    private PlayerMotor _motor;
    private PlayerLook _look;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _onFoot = _playerInput.OnFoot;
        _motor = GetComponent<PlayerMotor>();
        _look = GetComponent<PlayerLook>();

        _onFoot.Jump.performed += ctx => _motor.Jump();
        // Crouch as hold
        _onFoot.Crouch.performed += ctx => _motor.SetCrouching(true);
        _onFoot.Crouch.canceled += ctx => _motor.SetCrouching(false);
        /*_onFoot.Sprint.performed += ctx => _motor.Sprint();*/

        _onFoot.Sprint.performed += ctx => _motor.SetSprinting(true);
        _onFoot.Sprint.canceled += ctx => _motor.SetSprinting(false);
    }

    void FixedUpdate()
    {
        _motor.ProcessMove(_onFoot.Movements.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        _look.ProcessLook(_onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _onFoot.Enable();
    }

    private void OnDisable()
    {
        _onFoot.Disable();
    }
}
