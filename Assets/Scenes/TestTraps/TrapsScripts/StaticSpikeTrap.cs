using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSpikeTrap : GeneralTraps
{
    protected override void TrapMovement()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            //access comp & minus hp / apply knock back etc.
        }
    }
}
