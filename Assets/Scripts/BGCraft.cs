using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCraft : MonoBehaviour
{
    [SerializeField] Transform moveNodes;
    [SerializeField] float moveSpeed, maxSpeed, detachRange, turnSpeed;
    Vector3 velocity;

    Vector3 targetPos, tempVec3;

    void Start()
    {
        targetPos = transform.position;
        velocity = transform.forward * maxSpeed;
    }

    void Update()
    {
        if ((transform.position - targetPos).sqrMagnitude < Mathf.Pow(detachRange, 2))
        {
            targetPos = GetNewPos();
        }
        velocity += (targetPos - transform.position).normalized * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        tempVec3 = velocity;
        transform.Translate(velocity * moveSpeed, Space.World);
        tempVec3.y = 0;
        transform.forward = Vector3.Lerp(transform.forward,tempVec3, Time.deltaTime * turnSpeed);
    }

    Vector3 GetNewPos()
    {
        return moveNodes.GetChild(Random.Range(0, moveNodes.childCount)).position;
    }

}
