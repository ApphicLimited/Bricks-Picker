 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetreDetecter : MonoBehaviour
{
    public float Metre;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stack")
        {
            GameManager.instance.ScoreController.FurtherStackMetre = Metre;
            GameManager.instance.CameraMovement.lookAt(this.transform);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Stack")
        {
            GameManager.instance.ScoreController.FurtherStackMetre = Metre;
            GameManager.instance.CameraMovement.lookAt(this.transform);
            gameObject.SetActive(false);
        }
    }
}
