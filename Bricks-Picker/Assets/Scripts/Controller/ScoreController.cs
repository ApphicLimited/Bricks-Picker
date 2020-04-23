using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private float furtherStackMetre;
    public float FurtherStackMetre
    {
        get { return furtherStackMetre; }
        set
        {
            if (value > furtherStackMetre)
                furtherStackMetre = value;
        }
    }

    private int currentCollectedStackNumber;
    public int CurrentCollectedStackNumber
    {
        get { return currentCollectedStackNumber; }
        set {
            currentCollectedStackNumber = value;
            GameManager.instance.CongratWordsController.CongratPlayer(value);
            GameManager.instance.StackManager.TextStackCount.text = value.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.OnGameStarted += OnGameStarted;
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Events

    private void OnGameStarted()
    {

    }

    private void OnDestroy()
    {
        GameManager.instance.OnGameStarted -= OnGameStarted;
    }

    #endregion
}