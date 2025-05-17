using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine _stateMachine;
    private GameObject _player;
    private NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;
    public GameObject Player => _player;

    public path _path;
    [Header("Sight Values")]
    public float _sightDistance = 20f;
    public float _fieldOfView = 85f;
    public float _eyeHeight;

    [Header("Weapon Values")]
    public AudioClip _bulletSound;
    public AudioSource _audioSource;
    public Transform _gunBarel;
    [Range(0.1f, 10f)]
    public float _fireRate;
    [SerializeField] private string _currentState;
    void Start()
    {
        _stateMachine = GetComponent<StateMachine>();
        _agent = GetComponent<NavMeshAgent>();
        _stateMachine.Initialise();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CanSeePlayer();
        _currentState = _stateMachine._activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        if (_player == null) return false;

        if (Vector3.Distance(transform.position, _player.transform.position) > _sightDistance)
            return false;

        Vector3 _targetDirection = _player.transform.position - transform.position - (Vector3.up * _eyeHeight);
        float _angleToPlayer = Vector3.Angle(_targetDirection, transform.forward);

        if (_angleToPlayer <= _fieldOfView)
        {
            Ray _ray = new Ray(transform.position + (Vector3.up * _eyeHeight), _targetDirection);
            if (Physics.Raycast(_ray, out RaycastHit _hitInfo, _sightDistance))
            {
                if (_hitInfo.transform.gameObject == _player)
                {
                    Debug.DrawRay(_ray.origin, _ray.direction * _sightDistance, Color.green);
                    return true;
                }
            }
            Debug.DrawRay(_ray.origin, _ray.direction * _sightDistance, Color.red);
        }

        return false;
    }
}
