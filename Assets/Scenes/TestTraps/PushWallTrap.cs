using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushWallTrap : GeneralTraps
{
    //Assign start / end pos first
    private void Start()
    {
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3f);
    }

    protected override void TrapMovement()
    {
        timer += Time.deltaTime;

        if (timer < 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * (trapSpeed * 2));
        }
        if (timer > 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * trapSpeed/2);
            if (timer >= 6)
            {
                timer = 0;
            }
        }
    }
}
