using UnityEngine;

public class Shooter
{    
    private Bullet _bulletPrefab;
    private Bullet _bullet;
    private Transform _aimTransform;

    public Shooter(Transform aimTransform, Bullet bulletPrefab)
    {
        _aimTransform = aimTransform;
        _bulletPrefab = bulletPrefab;
    }

    public void Shoot()
    {
        _bullet = Object.Instantiate(_bulletPrefab, _aimTransform.position, _aimTransform.rotation);
        Rigidbody bulletRigidbody = _bullet.GetComponent<Rigidbody>();

        bulletRigidbody.AddForce(_aimTransform.forward * 0.01f, ForceMode.Impulse);
        Object.Destroy(_bullet.gameObject, 5f);
    }
}
