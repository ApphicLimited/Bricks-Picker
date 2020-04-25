using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Tooltip("Works only for KickingPos and TargetPlayer")]
    public bool RunInEditor = false;
    [Space]
    public Transform TargetPlayer;
    public Vector3 StarPos;
    public Vector3 InGamePos;
    public Vector3 KickingPos;
    public Vector3 GoingForwardPos;
    public float SmoothSpeed;

    private Transform currentTarget;
    private Vector3 desiredPosition = Vector3.zero;
    private Vector3 desiredPosition2 = Vector3.zero;
    private Vector3 Velocity = Vector3.zero;
    private bool isGoingForward;
    private bool IsApproachedToEndPoint;

    private void Start()
    {
        isGoingForward = false;
        currentTarget = TargetPlayer;
    }

    private void Update()
    {
        if (GameManager.instance.GameState == GameStates.GameFinished)
            return;

        if (Mathf.Abs(transform.position.z - GameManager.instance.PlayerManager.EndTransform.position.z) < 30f)
            IsApproachedToEndPoint = true;

        AdjustCamPos();
    }

    private void AdjustCamPos()
    {
        if (IsApproachedToEndPoint)
            desiredPosition = currentTarget.TransformPoint(KickingPos);
        else if (GameManager.instance.GameState == GameStates.GameOnGoing)
            desiredPosition = currentTarget.TransformPoint(InGamePos);
        else
            desiredPosition = currentTarget.TransformPoint(StarPos);

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref Velocity, SmoothSpeed);

        transform.LookAt(currentTarget);
    }

    private void OnValidate()
    {
        if (!RunInEditor)
            return;

        desiredPosition = TargetPlayer.TransformPoint(KickingPos);

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref Velocity, SmoothSpeed);

        transform.LookAt(TargetPlayer);
    }
}
