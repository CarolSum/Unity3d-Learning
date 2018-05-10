using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void MovePlayer(int dir);
    int GetScore();
    int GetCrystalNum();
    bool GetGameOver();
    void Restart();
}

public interface ISceneController
{
    void LoadResources();
}


public interface ISSActionCallback
{
    void SSActoinEvent(SSAction source, int param = 0, GameObject objParam = null);
}


public class Diretion
{
    public const int UP = 0;
    public const int DOWN = 2;
    public const int LEFT = -1;
    public const int RIGHT = 1;
}