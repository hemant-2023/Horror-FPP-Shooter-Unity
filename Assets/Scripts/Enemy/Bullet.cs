using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Transform _hitTransform = collision.transform;

        if (_hitTransform.CompareTag("Player"))
        {
            Debug.Log("hit Player");
            _hitTransform.GetComponent<PlayerHealth>().TakeDamage(10);

            PlayerLook lookScript = _hitTransform.GetComponent<PlayerLook>();
            if (lookScript != null)
            {
                lookScript.Shake();
            }
        }

        Destroy(gameObject);
    }
}
