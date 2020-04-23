using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public Obstacle Obstacle;

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
