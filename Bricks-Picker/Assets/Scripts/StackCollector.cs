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

    [HideInInspector]
    public List<Stack> CollectedStacks = new List<Stack>();
    private Material materialClone;

    private void Start()
    {
        materialClone = new Material(GameManager.instance.PlayerManager.MaterialSource);
        MeshRenderer.material = materialClone;

        SuperPowerController.OnSuperPowerActivated += OnSuperPowerActivated;
        GameManager.instance.OnGameStarted += OnGameStarted;
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
        foreach (var item in CollectedStacks)
        {
            item.CustomJoint.DisableJoint();
            Destroy(item.GetComponent<ConfigurableJoint>());

            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
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
                    CollectedStacks.Last().MoveOverCollecter(transform.position + Vector3.up * 0.2f, DoSomething);
                else
                    CollectedStacks.Last().MoveOverCollecter(CollectedStacks[CollectedStacks.Count - 2].transform.position + Vector3.up * 0.2f, DoSomething);

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