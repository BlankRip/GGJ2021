using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] float farPointDistance;
    [SerializeField] float thisLayerDistanceX, thisLayerDistanceY;
    [SerializeField] bool paralaxX = true, paralaxY;
    Transform MainCameraTransform;
    static Vector3 tempVec3;

    void Start()
    {
        MainCameraTransform = Camera.main.transform;
    }

    // parallax is calculated fom the farpoint

    void Update()
    {
            tempVec3 = transform.position;
        if(paralaxX){
            float sideToFarpointRationX = MainCameraTransform.position.x / farPointDistance;
            tempVec3.x = sideToFarpointRationX * (farPointDistance - thisLayerDistanceX);
        }
        if(paralaxY){
              float sideToFarpointRationY = MainCameraTransform.position.y / farPointDistance;
            tempVec3.y = sideToFarpointRationY * (farPointDistance - thisLayerDistanceY);
        }
        transform.position = tempVec3;
    }
}
