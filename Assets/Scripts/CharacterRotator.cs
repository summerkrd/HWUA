using UnityEngine;

public class CharacterRotator
{
    private Rigidbody _rigidbody;

    public CharacterRotator(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void GetRotation(Transform transform)
    {        
            Vector3 horizontalVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);

        if(horizontalVelocity.magnitude > 0.1f)
            transform.rotation = Quaternion.LookRotation(horizontalVelocity);        
    }
}
