using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneralTraps : MonoBehaviour
{
    public static float trapSpeed = 3;
    protected float timer = 0;
    protected float randomTimer = 0;
    protected abstract void TrapMovement();

    protected Vector3 startPos;
    protected Vector3 endPos;



    private void Update()
    {
        TrapMovement();
    }
}

/*
- Change all collison code to 2D
- pressure plate puzzle
- static spike trap
- squashing wall
//

    test with fukermain ctrl c & ctrl v
*/
