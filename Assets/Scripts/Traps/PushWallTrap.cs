using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushWallTrap : GeneralTraps
{
    public int randomNum;
    public bool randomController;

    //Assign start / end pos first
    private void Start()
    {
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3f);
        randomController = false;
    }

    protected override void TrapMovement()
    {
        timer += Time.deltaTime;
        randomTimer += Time.deltaTime;

        if (randomTimer >= 5)
        {
            randomController = false;
            randomTimer = 0;
        }

        if (randomController == false)
        {
            randomNum = Random.Range(1, 3);
            randomController = true;
        }

        if (randomNum == 1)
        {/*
            if (timer < 0.5f)
            {
                transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * (trapSpeed * 2));
            }
            if (timer > 0.5f)
            {*/
                transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * (trapSpeed / 2));
               /* if (timer >= 4)
                {
                    timer = 0;
                }
            }*/
        }
        else if (randomNum == 2)
        {
            if (timer < 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * (trapSpeed * 10));
            }
            if (timer > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * (trapSpeed * 10));
                if (timer >= 0.2f)
                {
                    timer = 0;
                }
            }
        }
    }
}
