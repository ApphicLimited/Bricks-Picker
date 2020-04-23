using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float AnimationSpeed;
    public bool StartAnimation;
    public float DesiredHeight = 500;

    private RectTransform rectTransform;
    private Vector3 nextPosition;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        nextPosition += Vector3.up * DesiredHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartAnimation)
            return;

        rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, nextPosition, AnimationSpeed * Time.deltaTime);
    }
}