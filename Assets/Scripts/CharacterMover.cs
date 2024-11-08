using UnityEngine;

public class CharacterMover 
{         
    private Rigidbody _rigidbody;
    private Vector3 _direction;
    private float _speedForce = 100f;

    public CharacterMover(Rigidbody rigidbody, float speedForce)
    {
        _rigidbody = rigidbody;
        _speedForce = speedForce;
    }   

    public void MoveToDirection(Vector3 direction)
    {        
        _rigidbody.AddForce(direction * Time.deltaTime * _speedForce, ForceMode.Force);
    }
}
