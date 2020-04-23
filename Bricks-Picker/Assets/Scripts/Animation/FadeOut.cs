using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Text TextPoint;
    public float AnimationSpeed;
    public bool StartAnimation;

    private Vector4 nextColour;

    // Start is called before the first frame update
    void Start()
    {
        nextColour = new Vector4(TextPoint.color.r, TextPoint.color.g, TextPoint.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartAnimation)
            return;

        TextPoint.color = Vector4.Lerp(TextPoint.color, nextColour, AnimationSpeed * Time.deltaTime);

        if (Vector4.Distance(TextPoint.color, nextColour) < 0.01f)
            Destroy(gameObject);
    }
}