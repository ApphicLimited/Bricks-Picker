using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StackCollector : MonoBehaviour
{
    public float CollecterMaxScale;
    public float CollecterMinScale;
    public CustomJoint CustomJoint;
    public MeshRenderer MeshRenderer;

    public bool Breakit;

    [HideInInspector]
    public List<Stack> CollectedStacks = new List<Stack>();
    private Material materialClone;

    private void Start()
    {
        SuperPowerController.OnSuperPowerActivated += OnSuperPowerActivated;
        GameManager.instance.OnGameStarted += OnGameStarted;
    }

    private void Update()
    {
        if (Breakit)
        {
            ResetJointSettings();
            Breakit = false;
        }
    }

    public void SetUpMaterial()
    {
        materialClone = new Material(GameManager.instance.PlayerManager.MaterialSource);
        MeshRenderer.material = materialClone;
    }

    public void ChangeColour(BaseColour colour)
    {
        materialClone.color = GameManager.instance.ColourController.GetColour(colour);

        foreach (var item in CollectedStacks)
            item.ChangeColour(colour);
    }

    public void ResetJointSettings()
    {
        Destroy(GameManager.instance.PlayerManager.Player.GetComponent<FixedJoint>());
        CustomJoint.DisableJoint();
        CustomJoint.BreakForce();
        CustomJoint.BreakTorque();

        Destroy(GetComponent<ConfigurableJoint>());

        foreach (var item in CollectedStacks)
        {          
            item.ResetJointSettings();
        }

        GameManager.instance.GameState = GameStates.GameFinished;

        Destroy(this);
    }

    private void UseMaxScale()
    {
        transform.localScale = new Vector3(CollecterMaxScale, transform.localScale.y, transform.localScale.z);
    }

    private void UseMinScale()
    {
        transform.localScale = new Vector3(CollecterMinScale, transform.localScale.y, transform.localScale.z);
    }

    private void DoSomething()
    {
        if (CollectedStacks.Count == 1)
        {
            CustomJoint.SetUpJoint(10, CollectedStacks[0].gameObject.GetComponent<Rigidbody>());
        }
        else
        {
            for (int i = 0; i < CollectedStacks.Count; i++)
                if (i + 1 < CollectedStacks.Count)
                    CollectedStacks[i].CustomJoint.SetUpJoint(10, CollectedStacks[i + 1].GetComponent<Rigidbody>());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Stack")
        {
            if (collision.collider.GetComponent<Stack>().CurrentColour == GameManager.instance.PlayerManager.CurrentColour)
            {
                CollectedStacks.Add(collision.collider.GetComponent<Stack>());

                if (CollectedStacks.Count == 1)
                    CollectedStacks.Last().MoveOverCollecter(new Vector3(transform.position.x, 0.5f, transform.position.z), DoSomething);
                else
                    CollectedStacks.Last().MoveOverCollecter(CollectedStacks[CollectedStacks.Count - 2].transform.position + Vector3.up * 0.2f, DoSomething);

                //int mass = 1000 - CollectedStacks.Count;
                //if (mass>10)
                //{
                //    collision.collider.GetComponent<Stack>().Rigidbody.mass = mass;
                //}

                GameManager.instance.SuperPowerController.AddPower(CollectedStacks.Last().Point);
                GameManager.instance.ScoreController.CurrentCollectedStackNumber++;
                GameManager.instance.StackManager.Stacks.Remove(collision.collider.GetComponent<Stack>());
            }
            else
            {
                GameManager.instance.SuperPowerController.SubPower(collision.collider.GetComponent<Stack>().Point);
                GameManager.instance.StackManager.Stacks.Remove(collision.collider.GetComponent<Stack>());
                Destroy(collision.collider.gameObject);
            }
        }
    }

    #region Events

    private void OnSuperPowerActivated(bool IsActivated)
    {
        if (IsActivated)
            UseMaxScale();
        else
            UseMinScale();
    }

    private void OnGameStarted()
    {
      
    }

    private void OnDestroy()
    {
        SuperPowerController.OnSuperPowerActivated -= OnSuperPowerActivated;
        GameManager.instance.OnGameStarted -= OnGameStarted;
    }

    #endregion
}