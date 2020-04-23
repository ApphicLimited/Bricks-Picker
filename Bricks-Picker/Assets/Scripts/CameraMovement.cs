using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Vector3 velocity = Vector3.zero;
    public Vector3 targetPosition = Vector3.zero;

    public Vector3 StarPos;
    public Vector3 InGamePos;

    private Vector3 desiredPosition;

    private void Update()
    {
        if (GameManager.instance.GameState == GameStates.GamePaused)
            targetPosition = target.TransformPoint(StarPos);
        else if (GameManager.instance.GameState == GameStates.GameOnGoing)
            targetPosition = target.TransformPoint(InGamePos);

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);

        transform.LookAt(target);
    }

    void FixedUpdate()
    {
        //Vector3 desiredPosition = target.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //transform.position = smoothedPosition;

        //transform.LookAt(target);
    }
}
