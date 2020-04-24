using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    public BaseColour MainColour;
    public int Point;
    public Elastic Elastic;
    //public CustomJoint CustomJoint;
    public MeshRenderer MeshRenderer;
    public Rigidbody Rigidbody;

    public BaseColour CurrentColour { get; set; }

    private Material materialClone;
    //private bool keepdestroy;
    //private bool IsCollected;

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
        if (materialClone==null)
        {
            Debug.Log("NULL");
        }
        else
        {
            materialClone.color = GameManager.instance.ColourController.GetColour(colour);
        }
    }

    public void ResetColour()
    {
        CurrentColour = MainColour;
        if (materialClone == null)
        {
            Debug.Log("NULL");
        }
        else
        {
            materialClone.color = GameManager.instance.ColourController.GetColour(MainColour);
        }
    }

    public void EnableElastic(bool isEnable, Transform _transform = null)
    {
        if (isEnable)
        {
            Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            Rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
            Rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            GetComponent<BoxCollider>().enabled = true;

        }
        else
        {
            Rigidbody.constraints &= ~RigidbodyConstraints.FreezeAll;
            GetComponent<BoxCollider>().enabled = true;
        }

        if (_transform != null)
        {
            Elastic.Target = _transform;
        }

        Elastic.enabled = isEnable;
    }

    public void AddForce()
    {
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