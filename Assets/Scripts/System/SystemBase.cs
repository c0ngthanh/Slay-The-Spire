using System;
using UnityEngine;

public abstract class SystemBase : IDisposable
{
    
    public virtual void Dispose()
    {
        throw new NotImplementedException();
    }
}
