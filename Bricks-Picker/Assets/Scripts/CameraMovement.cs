using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Tooltip("Works only for KickingPos and TargetPlayer")]
    public bool RunInEditor = false;
    [Space]
    public Transform TargetPlayer;
    public Transform StarPos;
    public Transform InGamePos;
    public Transform KickingPos;
    public Transform GoingForwardPos;
    public float SmoothSpeed;

    private Transform currentTarget;
    private Vector3 desiredPosition = Vector3.zero;
    private Vector3 desiredPosition2 = Vector3.zero;
    private Vector3 Velocity = Vector3.zero;
    private bool isGoingForward;
    private bool IsApproachedToEndPoint;


    public SmoothFollow camerafollow;

    private void Start()
    {
        camerafollow = GetComponent<SmoothFollow>();
        isGoingForward = false;
        currentTarget = TargetPlayer;
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.z - GameManager.instance.PlayerManager.EndTransform.position.z) < 30f)
            IsApproachedToEndPoint = true;

        AdjustCamPos();
    }

    private void AdjustCamPos()
    {
        if (IsApproachedToEndPoint && camerafollow.target != KickingPos)
        {
            camerafollow.target = KickingPos;
        }
        else if (GameManager.instance.GameState == GameStates.GameOnGoing && camerafollow.target != InGamePos)
        {
            camerafollow.target = InGamePos;
        }
        else if (!IsApproachedToEndPoint && GameManager.instance.GameState != GameStates.GameOnGoing && camerafollow.target != StarPos)
        {
            camerafollow.target = StarPos;
        }

        // Smoothly move the camera towards that target position
        //transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref Velocity, SmoothSpeed);

        //transform.LookAt(currentTarget);
    }

    private void OnValidate()
    {
        if (!RunInEditor)
            return;

        camerafollow.target = KickingPos;

        // Smoothly move the camera towards that target position
        //transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref Velocity, SmoothSpeed);

        //transform.LookAt(TargetPlayer);
    }
}
