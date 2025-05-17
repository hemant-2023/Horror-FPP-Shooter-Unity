using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera _cam;
    [SerializeField]
    private float _distnnce = 3f;
    [SerializeField]
    private LayerMask _mask;

    private InputManager _inputManager;

    private PlayerUI _playerUI;

    void Start()
    {
        _cam = GetComponent<PlayerLook>()._cam;
        _playerUI = GetComponent<PlayerUI>();
        _inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        _playerUI.UpdateText(string.Empty);

        Ray _ray = new Ray(_cam.transform.position, _cam.transform.forward);
        Debug.DrawRay(_ray.origin, _ray.direction * _distnnce, Color.red);

        RaycastHit _hitInfo;
        if (Physics.Raycast(_ray, out _hitInfo, _distnnce, _mask))
        {
            Interactable interactable = _hitInfo.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                _playerUI.UpdateText(interactable._promptMassage);
                if(_inputManager._onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
