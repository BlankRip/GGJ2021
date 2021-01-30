using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool pressed;
    public float pressedTimer; // def val 5
    Vector3 pressedPos;
    Vector3 mainPos;
    float timer;
    public GameObject pressurePlateChild;
    public GameObject sphereIndicator;
    Renderer sphereRenderer;
    private void Start()
    {
        pressed = false;
        pressedPos = new Vector3(transform.position.x, transform.position.y - 0.15f, transform.position.z);
        mainPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (sphereIndicator != null)
            sphereRenderer = sphereIndicator.GetComponent<Renderer>();
    }

    private void Update()
    {
        if (pressed == true)
        {
            pressurePlateChild.transform.position = Vector3.Lerp(pressurePlateChild.transform.position, pressedPos, Time.deltaTime * 2);
            timer += Time.deltaTime;
            if (timer >= pressedTimer)
            {
                pressed = false;
                timer = 0;
            }
            if(sphereRenderer != null)
                sphereRenderer.material.color = Color.green;
        }
        else
        {
            pressurePlateChild.transform.position = Vector3.Lerp(pressurePlateChild.transform.position, mainPos, Time.deltaTime * 2);
            if(sphereRenderer != null)
                sphereRenderer.material.color = Color.red;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pressed = true;
        }
    }
}
