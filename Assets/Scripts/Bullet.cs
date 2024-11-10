using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage = 10;

    private IDamageble _damagebleObject;

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.TryGetComponent<IDamageble>(out _damagebleObject);

        if (_damagebleObject != null)
            _damagebleObject.TakeDamage(_damage);

        Destroy(gameObject);
    }
}
