using UnityEngine;

public class AttackState : BaseState
{
    private float _moveTimer;
    private float _losePlayerTimer;
    private float _shotTimer;

    public override void Enter()
    {
        _moveTimer = 0f;
        _losePlayerTimer = 0f;
        _shotTimer = 0f;
    }

    public override void Exit()
    {
        // Optional: cleanup or reset logic
    }

    public override void Perform()
    {
        if (_enemy.CanSeePlayer())
        {
            _losePlayerTimer = 0f;
            _moveTimer += Time.deltaTime;
            _shotTimer += Time.deltaTime;

            _enemy.transform.LookAt(_enemy.Player.transform);

            if (_shotTimer > _enemy._fireRate)
            {
                Shoot();
            }

            if (_moveTimer > Random.Range(3f, 7f))
            {
                Vector3 randomDirection = Random.insideUnitSphere * 5f;
                randomDirection.y = 0f;
                Vector3 destination = _enemy.transform.position + randomDirection;
                _enemy.Agent.SetDestination(destination);
                _moveTimer = 0f;
            }
        }
        else
        {
            _losePlayerTimer += Time.deltaTime;
            if (_losePlayerTimer > 8f)
            {
                _stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public void Shoot()
    {
        Transform gunBarrel = _enemy._gunBarel;
        if (gunBarrel == null)
        {
            Debug.LogWarning("Gun barrel not assigned.");
            return;
        }

        GameObject bulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
        if (bulletPrefab == null)
        {
            Debug.LogWarning("Bullet prefab not found in Resources/Prefabs.");
            return;
        }

        GameObject bullet = GameObject.Instantiate(bulletPrefab, gunBarrel.position, _enemy.transform.rotation);

        Vector3 shootDirection = (_enemy.Player.transform.position - gunBarrel.position).normalized;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * 40f;
        }

        if (_enemy._audioSource != null && _enemy._bulletSound != null)
        {
            _enemy._audioSource.PlayOneShot(_enemy._bulletSound);
        }

        Debug.Log("Shooting at player");
        _shotTimer = 0f;
    }
}
