using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float AnimationSpeed;
    public bool StartAnimation;

    private RectTransform rectTransform;
    public Vector3 nextPosition;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartAnimation)
            return;

        rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, nextPosition, AnimationSpeed * Time.deltaTime);
    }
}