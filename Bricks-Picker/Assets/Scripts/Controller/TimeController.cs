using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float SlowDownFactor;
    public float SlowDownTimeAmaount;

    // Update is called once per frame
    void Update()
    {
        Time.timeScale += (1f / SlowDownTimeAmaount) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void DoSlowMotion()
    {
        Time.timeScale = SlowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
