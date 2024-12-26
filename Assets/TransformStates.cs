using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformStates : MonoBehaviour
{
    public Transform OriginalParent
    {
        get;
        set;
    }

    public void Awake()
    {
        this.OriginalParent = this.transform.parent;
    }
}
