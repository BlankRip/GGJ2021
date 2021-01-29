using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTrap : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    public float timer = 0;
    public float speedTest;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z);
        endPos = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 3)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * speedTest);
        }
        if (timer > 3)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * speedTest);
            if (timer >= 6)
            {
                timer = 0;
            }
        }
    }
}
