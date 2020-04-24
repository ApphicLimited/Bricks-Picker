using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float AnimationSpeed;
    public Vector3 nextPosition;
    [Space]
    public bool SetNativePosition;

    public bool StartAnimation { get; set; }

    private RectTransform rectTransform;
    private Transform ownTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        ownTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartAnimation)
            return;

        if (ownTransform!=null)
        {
            ownTransform.position = Vector3.Lerp(ownTransform.position, nextPosition, AnimationSpeed * Time.deltaTime);
        }

        if (rectTransform!=null)
        {
            rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, nextPosition, AnimationSpeed * Time.deltaTime);
        }
    }

    private void OnValidate()
    {
        if (SetNativePosition)
        {
            nextPosition = transform.position;
            SetNativePosition = false;
        }
    }
}