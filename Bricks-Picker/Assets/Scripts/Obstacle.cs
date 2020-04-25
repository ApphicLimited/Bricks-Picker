using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float MinDistance;
    public Move AnimationMove;

    void Update()
    {
        if (GameManager.instance.GameState!= GameStates.GameOnGoing)
            return;

        if (Vector3.Distance(transform.position,GameManager.instance.PlayerManager.Player.transform.position)< MinDistance)
        {
            AnimationMove.StartAnimation = true;
        }
    }
}