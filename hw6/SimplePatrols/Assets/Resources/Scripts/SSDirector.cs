using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object {
    private static SSDirector instance;

    public ISceneController currentSceneController { get; set; }

    public static SSDirector getInstance()
    {
        if (instance == null) instance = new SSDirector();
        return instance;
    }
}
