using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void moveForward();
    void moveBackWard();
    void turn(float offsetX);
    void shoot();
    bool isGameOver();
}