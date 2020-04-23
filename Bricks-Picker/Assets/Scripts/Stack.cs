using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    public BaseColour MainColour;
    public int Point;
    public MeshRenderer MeshRenderer;
    public CustomJoint CustomJoint;

    public BaseColour CurrentColour { get; set; }

    private Material materialClone;

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
        if (GameManager.instance.GameState != GameStates.GameOnGoing)
            return;
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


    private void ResetJointSettings()
    {

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