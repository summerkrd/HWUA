using UnityEngine;

public class Player : Character, IDamageble
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _aimTransform;

    private const string _verticalAxis = "Vertical";
    private const string _horizontalAxis = "Horizontal";
       
    private float _xInput;
    private float _yInput;
    private Vector3 _direction;

    private Shooter _shooter;

    protected override void Start()
    {
        base.Start();
        _shooter = new Shooter(_aimTransform, _bulletPrefab);
    }

    private void Update()
    {
        GetDirection();
        _mover.MoveToDirection(_direction);
        _rotator.GetRotation(this.transform);

        if (Input.GetKeyDown(KeyCode.Space))
            _shooter.Shoot();
    }

    private void GetDirection()
    {
        _xInput = Input.GetAxisRaw(_horizontalAxis);
        _yInput = Input.GetAxisRaw(_verticalAxis);
        _direction = new Vector3(_xInput, 0, _yInput).normalized;
    }

    public void TakeDamage(float damage)
    {
        
    }
}
