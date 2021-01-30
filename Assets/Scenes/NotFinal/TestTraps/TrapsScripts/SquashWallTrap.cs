using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashWallTrap : GeneralTraps
{
    //Assign start / end pos first
    private void Start()
    {
        startPos = new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z);
        endPos = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
    }

    protected override void TrapMovement()
    {
        timer += Time.deltaTime;

        if (timer < 3)//inactive for 3 seconds
        {
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * trapSpeed);
        }
        if (timer > 3)//active for 1 seconds
        {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * trapSpeed);
            if (timer >= 4)
            {
                timer = 0;
            }
        }
    }
}
