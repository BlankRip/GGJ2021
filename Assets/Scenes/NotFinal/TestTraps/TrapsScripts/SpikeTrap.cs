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

        if (timer < 2f) //active for 2 seconds
        {
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * trapSpeed);
        }
        if (timer > 2f) //inactive for 3 seconds
        {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * trapSpeed);
            if (timer >= 5)
            {
                timer = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            //access comp & minus hp / apply knock back etc.
        }
    }
}
