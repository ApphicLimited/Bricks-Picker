using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    public BaseColour MainColour;
    public int Point;
    public CustomJoint CustomJoint;
    public MeshRenderer MeshRenderer;
    public Rigidbody Rigidbody;

    public BaseColour CurrentColour { get; set; }

    private Material materialClone;
    private bool keepdestroy;

    public delegate void AfterMoved();
    public AfterMoved AfterMovedDoAction;

    private void Start()
    {
        materialClone = new Material(GameManager.instance.StackManager.MaterialSource);
        MeshRenderer.material = materialClone;
        ChangeColour(MainColour);
    }

    private void Update()
    {
        //if (GameManager.instance.GameState != GameStates.GameOnGoing)
        //    return;

        if (keepdestroy)
        {
            if (GetComponent<ConfigurableJoint>() == null)
            {
                keepdestroy = false;
            }
            Destroy(GetComponent<ConfigurableJoint>());
        }
    }

    public void ChangeColour(BaseColour colour)
    {
        CurrentColour = colour;
        materialClone.color = GameManager.instance.ColourController.GetColour(colour);
    }

    public void ResetColour()
    {
        CurrentColour = MainColour;
        materialClone.color = GameManager.instance.ColourController.GetColour(MainColour);
    }

    public void ResetJointSettings()
    {
        keepdestroy = true;

        CustomJoint.DisableJoint();
        CustomJoint.BreakForce();
        CustomJoint.BreakTorque();

        Rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
        Rigidbody.AddForce(Vector3.forward * 30, ForceMode.Impulse);
    }

    public void MoveOverCollecter(Vector3 newPos, AfterMoved action = null)
    {
        transform.position = newPos;

        AfterMovedDoAction = action;
        DoAction();
    }

    private void DoAction()
    {
        AfterMovedDoAction?.Invoke();
        AfterMovedDoAction = null;
    }
}