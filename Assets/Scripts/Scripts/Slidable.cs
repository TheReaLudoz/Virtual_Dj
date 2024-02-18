using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Slidable : MonoBehaviour
{
    protected Transform _originalParent;

    public Transform OriginalParent
    {
        get => _originalParent;
        protected set => _originalParent = value;
    }

    protected virtual void Start()
    {
        _originalParent = transform.parent;
    }

    public abstract void Slide(GameObject grabber);
    public abstract void Drop();
}
