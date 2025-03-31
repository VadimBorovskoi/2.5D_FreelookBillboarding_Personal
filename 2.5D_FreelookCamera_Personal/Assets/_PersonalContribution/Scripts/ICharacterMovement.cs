using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacterMovement : MonoBehaviour
{
    public abstract bool IsMoving
    {
        get;
    }
    protected virtual void OnEnable()
    {
        Activate();
    }
    protected virtual void OnDisable()
    {
        Deactivate();
    }
    public abstract void Activate();
    public abstract void Deactivate();
}
