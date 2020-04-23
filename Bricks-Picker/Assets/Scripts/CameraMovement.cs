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
    public float SmoothSpeed;

    private Transform currentTarget;
    private Vector3 desiredPosition = Vector3.zero;
    private Vector3 Velocity = Vector3.zero;
    private bool isKicking;

    private void Start()
    {
        isKicking = false;
        currentTarget = TargetPlayer;
    }

    private void Update()
    {
        AdjustCamPos();

        if (isKicking)
            if (Vector3.Distance(transform.position, desiredPosition) < 0.1f)
            {
                GameManager.instance.PlayerManager.Player.PlayKickAnim();
            }
    }

    private void AdjustCamPos()
    {
        if (GameManager.instance.GameState == GameStates.GamePaused)
        {
            if (isKicking)
                desiredPosition = currentTarget.TransformPoint(KickingPos);
            else
                desiredPosition = currentTarget.TransformPoint(StarPos);
        }
        else if (GameManager.instance.GameState == GameStates.GameOnGoing)
            desiredPosition = currentTarget.TransformPoint(InGamePos);

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref Velocity, SmoothSpeed);

        transform.LookAt(currentTarget);
    }

    public void SwapTarget(Transform nextTarget)
    {
        currentTarget = nextTarget;
    }

    public void AdjustCamToKicking()
    {
        isKicking = true;
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
