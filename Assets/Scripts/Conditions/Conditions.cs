using System;

public abstract class Conditions
{
    public abstract event Action Completed;

    public abstract void Start();
      
    public abstract void Disable();
}
