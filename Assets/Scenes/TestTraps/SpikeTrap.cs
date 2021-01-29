using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : GeneralTraps
{
    //Assign start / end pos first
    private void Start()
    {
        startPos = new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z);
        endPos = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
    }

    protected override void TrapMovement()
    {
        timer += Time.deltaTime;

        if (timer < 3)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * trapSpeed);
        }
        if (timer > 3)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * trapSpeed);
            if (timer >= 6)
            {
                timer = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision) //CHANGE TO 2D
    {
        if (collision.gameObject.tag == ("Player"))
        {
            //access comp & minus hp / apply knock back etc.
        }
    }
}
