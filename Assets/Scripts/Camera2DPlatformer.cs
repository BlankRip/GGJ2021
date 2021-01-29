using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DPlatformer : MonoBehaviour
{
    [SerializeField] GameObject target;

    [SerializeField] Vector3 camOffSet;                         //Valuses of the postion of the camera needed to be added to the player postion on start
    [SerializeField] float smoothSpeed = 0.15f;                 //The amount of smoothening applied

    [Header("Stuff related to clamping")]
    [SerializeField] bool clampXmin;
    public float xMin;
    [SerializeField] bool clampXmax;
    public float xMax;
    [SerializeField] bool clampYmin;
    public float yMin;
    [SerializeField] bool clampYmax;
    public float yMax;

    Vector3 velocity = Vector3.zero;      

    private void Start() {
        if(target == null)
            target = GameManager.instance.playerScript.gameObject;
    }  


    // Updating the positon of camera to follow the player at all times
    void FixedUpdate() {
        // Setting the reqired spot the camera has to be
        Vector3 requiredSpot = target.transform.position + camOffSet;

        //Claming Horizontal
        if (clampXmin && clampXmax)
            requiredSpot.x = Mathf.Clamp(target.transform.position.x + camOffSet.x, xMin, xMax);
        else if (clampXmin)
            requiredSpot.x = Mathf.Clamp(target.transform.position.x + camOffSet.x, xMin, target.transform.position.x + camOffSet.x);
        else if (clampXmax)
            requiredSpot.x = Mathf.Clamp(target.transform.position.x + camOffSet.x, target.transform.position.x + camOffSet.x, xMax);

        //Clamping Vertical
        if (clampYmax && clampYmin)
            requiredSpot.y = Mathf.Clamp(target.transform.position.y + camOffSet.y, yMin, yMax);
        else if (clampYmin)
            requiredSpot.y = Mathf.Clamp(target.transform.position.y + camOffSet.y, yMin, target.transform.position.y + camOffSet.y);
        else if (clampYmax)
            requiredSpot.y = Mathf.Clamp(target.transform.position.y + camOffSet.y, target.transform.position.y + camOffSet.y, yMax);


        // Actually moving the camra to the required spot with a smoothening effect
        transform.position = Vector3.SmoothDamp(transform.position, requiredSpot, ref velocity, smoothSpeed);
    }
}
