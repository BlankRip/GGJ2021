using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] float farPointDistance;
    [SerializeField] float thisLayerDistance;
    Transform MainCameraTransform;
    static Vector3 tempVec3;

    void Start()
    {
        MainCameraTransform = Camera.main.transform;
    }

    // parallax is calculated fom the farpoint

    void Update()
    {
        float sideToFarpointRation = MainCameraTransform.position.x / farPointDistance;
        tempVec3 = transform.position;
        tempVec3.x = sideToFarpointRation * (farPointDistance - thisLayerDistance);
        transform.position = tempVec3;
    }
}
