using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private TouchPhase touchPhase;
    private Touch currentTouch;
    private Vector2 touchPos;

    public float TouchSenseDistance;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GameState!=GameStates.GameOnGoing)
            return;

        TouchDetect();

        #region KEYBOARD DETECTION

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameManager.instance.PlayerManager.MoveToSide(TouchSides.Left);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameManager.instance.PlayerManager.MoveToSide(TouchSides.Right);
        }

        #endregion
    }

    private void TouchDetect()
    {
        switch (touchPhase)
        {
            case TouchPhase.Began:
                touchPos = currentTouch.position;
                break;
            case TouchPhase.Ended:
                if (currentTouch.position.x > touchPos.x + TouchSenseDistance)
                    GameManager.instance.PlayerManager.MoveToSide(TouchSides.Right);
                else if (currentTouch.position.x < touchPos.x + TouchSenseDistance)
                    GameManager.instance.PlayerManager.MoveToSide(TouchSides.Left);
                break;
        }
    }
}