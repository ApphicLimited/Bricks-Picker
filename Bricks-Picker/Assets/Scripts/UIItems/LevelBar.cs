using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
    public Text TextLevel;
    public Image ImageLevelBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dff = GameManager.instance.PlayerManager.StartTransform.position.z- GameManager.instance.PlayerManager.EndTransform.position.z;
        ImageLevelBar.fillAmount = dff ;
    }
}
