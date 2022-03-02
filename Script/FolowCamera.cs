using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowCamera : MonoBehaviour
{
    private Transform target;
    public float moveDamping = 15.0f;
    public float rotateDamping = 10.0f;
    public float distance = 5.0f;
    public float height = 4.0f;
    public float targetOffset = 2.0f;


    private void Start()
    {
        target = GameObject.Find("Man").transform;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 camPos = target.position
            - (target.forward * distance) + (target.up * height);

        transform.position = Vector3.Slerp(transform.position,
            camPos, Time.deltaTime * moveDamping);

        transform.rotation = Quaternion.Slerp(transform.rotation,
            target.rotation, Time.deltaTime * rotateDamping);

        transform.LookAt(target.position + (target.up * targetOffset));
    }

    private void OnDrawGizmos()
    {
        if (target == null)
            return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(target.position +
            (target.up * targetOffset), 0.1f);

        Gizmos.DrawLine(target.position + (target.up * targetOffset),
            transform.position);
    }
}
