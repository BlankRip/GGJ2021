using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap : GeneralTraps
{
    //Assign start / end pos first
    private void Start()
    {
        startPos = new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z);
        endPos = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
    }

    protected override void TrapMovement()
    {
        timer += Time.deltaTime;

        if (timer < 2)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * trapSpeed/2);
        }
        if (timer > 2)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * trapSpeed/2);
            if (timer >= 4)
            {
                timer = 0;
            }
        }
    }
}
