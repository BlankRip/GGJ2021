using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    public GameObject[] plates;
    public bool openDoor;
    Vector3 closedDoorPos;
    Vector3 openedDoorPos;

    // Start is called before the first frame update
    void Start()
    {
        closedDoorPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        openedDoorPos = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        openDoor = true;
        for (int i = 0; i < plates.Length; i++)
        {
            openDoor &= plates[i].GetComponent<PressurePlate>().pressed == true;

            /*
            if(plates[i].GetComponent<PressurePlate>().pressed == false)
            {
                return;
            }
            */
        }

        /*if (plates[i].GetComponent<PressurePlate>().pressed == true)// && //plates[i + 1].GetComponent<PressurePlate>().pressed == true)
        {
            openDoor = true;
        }
        else
        {
            openDoor = false;
        }*/

        //openDoor = true;

        if (openDoor == true)
        {
            transform.position = Vector3.Lerp(transform.position, openedDoorPos, Time.deltaTime * 5f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, closedDoorPos, Time.deltaTime * 5f);
        }
    }
}
