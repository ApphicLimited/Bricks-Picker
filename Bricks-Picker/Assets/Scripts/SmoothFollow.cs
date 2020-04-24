using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
    public CameraMovement CameraMovement;
    // The target we are following
    public Transform target;
    // The distance in the x-z plane to the target
    public float distance = 10.0f;
    // the height we want the camera to be above the target
    public float height = 5.0f;
    // How much we 
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    public float fCamShakeImpulse = 0.0f;  //Camera Shake Impulse
    // Place the script in the Camera-Control group in the component menu
    [AddComponentMenu("Camera-Control/Smooth Follow")]

    private bool isGoingForward;

    void Update()
    {
        // Early out if we don't have a target
        if (!target) return;

        if (isGoingForward)
        {
            CameraMovement.enabled = false;
            if (Mathf.Abs(transform.position.z - target.position.z) < distance)
            {
                return;
            }
            transform.position = new Vector3(transform.position.x, height, transform.position.z + 20 * Time.deltaTime);
            //transform.LookAt(target);
        }
        else
        {

            //// Calculate the current rotation angles
            //float wantedRotationAngle = target.eulerAngles.y;
            //float wantedHeight = target.position.y + height;

            //float currentRotationAngle = transform.eulerAngles.y;
            //float currentHeight = transform.position.y;

            //// Damp the rotation around the y-axis
            //currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

            //// Damp the height
            //currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            //// Convert the angle into a rotation
            //var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            //// Set the position of the camera on the x-z plane to:
            //// distance meters behind the target
            //transform.position = target.position;
            //transform.position -= currentRotation * Vector3.forward * distance;

            //// Set the height of the camera
            //transform.position = new Vector3(transform.position.x + .05f, currentHeight, transform.position.z);

            //// Always look at the target
            //transform.LookAt(target);
            ////make the camera shake if the fCamShakeImpulse is not zero
            //if (fCamShakeImpulse > 0.0f)
            //    shakeCamera();
        }
    }

    public void GoForward()
    {
        isGoingForward = true;
        height = 12;
        distance = 20;
    }

    /*
    *	FUNCTION: Set the intensity of camera vibration
    *	PARAMETER 1: Intensity value of the vibration
    */
    public void setCameraShakeImpulseValue(int iShakeValue)
    {
        if (iShakeValue == 1)
            fCamShakeImpulse = 1.0f;
        else if (iShakeValue == 2)
            fCamShakeImpulse = 2.0f;
        else if (iShakeValue == 3)
            fCamShakeImpulse = 1.3f;
        else if (iShakeValue == 4)
            fCamShakeImpulse = 1.5f;
        else if (iShakeValue == 5)
            fCamShakeImpulse = 1.3f;
    }

    /*
    *	FUNCTION: Make the camera vibrate. Used for visual effects
    */
    public void shakeCamera()
    {
        transform.position += new Vector3(0, 0, Random.Range(-fCamShakeImpulse, fCamShakeImpulse));
        fCamShakeImpulse -= Time.deltaTime * fCamShakeImpulse * 4.0f;
        if (fCamShakeImpulse < 0.01f)
            fCamShakeImpulse = 0.0f;
    }
}
