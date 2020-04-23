using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChanger : MonoBehaviour
{
    public BaseColour MainColour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            GameManager.instance.PlayerManager.ChangePlayerColour(MainColour);
    }
}
