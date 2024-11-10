using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Character, IDamageble, IAttacker
{
    [SerializeField] private float _timeToChangeDirection = 3f;

    public event Action<Enemy> Killed;

    private float _timer = 0f;

    private float _damage = 10f;

    protected override void Start()
    {
        base.Start();

        _timer = _timeToChangeDirection;
        GetDirection();
    }

    private void Update()
    {        
        _timer += Time.deltaTime;               

        GetDirection();
        _mover.MoveToDirection(_direction);
        _rotator.GetRotation(this.transform);        
    }

    private void OnDestroy()
    {
        Killed?.Invoke(this);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        
        if (_currentHealth <= 0)
            Destroy(gameObject);
    }

    protected override void GetDirection()
    {
        if (_timer > _timeToChangeDirection)
        {
            Vector3 oldDirection = _direction;

            _direction = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2)).normalized;

            if (_direction == Vector3.zero || _direction == oldDirection)
                return;

            _timer = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player;
        collision.gameObject.TryGetComponent<Player>(out player);

        if (player != null)
            player.TakeDamage(_damage);

        if (collision.gameObject.GetComponent<Enemy>() != null)
            _timer = _timeToChangeDirection;

        if (collision.gameObject.CompareTag("Obstacle")) 
            _direction = -_direction;
    }
}
