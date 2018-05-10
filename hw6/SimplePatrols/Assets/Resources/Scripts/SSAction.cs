using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSAction : ScriptableObject {

    public bool enable = true;
    public bool destory = false;
    public GameObject gameObject;
    public Transform transform;
    public ISSActionCallback callback;

    protected SSAction() { }

    public virtual void Start() { }

    public virtual void Update() { }

}
