using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float _speedForce = 100f;
    [SerializeField] protected float _maxHealth;

    protected Rigidbody _rigidbody;

    protected CharacterMover _mover;
    protected CharacterRotator _rotator;

    protected float _currentHealth;

    protected void Awake()
    {
        TryGetComponent<Rigidbody>(out _rigidbody);
    }

    protected virtual void Start()
    {
        _mover = new CharacterMover(_rigidbody, _speedForce);
        _rotator = new CharacterRotator(_rigidbody);

        _currentHealth = _maxHealth;
    }
}
