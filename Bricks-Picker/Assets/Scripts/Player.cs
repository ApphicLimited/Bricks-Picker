using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float ForwardSpeed;
    public float Speed;
    public BaseColour BaseColour;
    public SkinnedMeshRenderer SkinnedMeshRenderer;
    public StackCollector StackCollector;
    public Animator Animator;
    public Rigidbody Rigidbody;

    public BaseColour CurrentBaseColour { get; set; }

    private Material materialClone;
    private Vector3 nextPosition;
    private bool IsArrived;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = GameManager.instance.PlayerManager.StartTransform.position;
        CurrentBaseColour = BaseColour;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GameState != GameStates.GameOnGoing)
            return;

        if (Vector3.Distance(GameManager.instance.PlayerManager.EndTransform.position, transform.position) < 3f)
        {
            if (IsArrived == false)
                ArrivedDest();
            return;
        }

        transform.Translate(Vector3.forward * Time.deltaTime * ForwardSpeed);
        nextPosition.z = transform.position.z;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.GameState != GameStates.GameOnGoing)
            return;

        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, step);
    }

    public void SetUpMaterial()
    {
        StackCollector.SetUpMaterial();

        materialClone = new Material(GameManager.instance.PlayerManager.MaterialSource);
        SkinnedMeshRenderer.material = materialClone;
        materialClone.color = GameManager.instance.ColourController.GetColour(CurrentBaseColour);
    }

    public void PlayKickAnim()
    {
        Animator.SetBool("Kicking", true);
        Animator.SetBool("Running", false);
    }

    public void MoveToSide(Vector3 position)
    {
        nextPosition = new Vector3(position.x, transform.position.y, transform.position.z);
    }

    public void ChangeColour(BaseColour colour)
    {
        CurrentBaseColour = colour;
        materialClone.color = GameManager.instance.ColourController.GetColour(colour);
        StackCollector.ChangeColour(colour);
    }

    private void ArrivedDest()
    {
        IsArrived = true;

        Destroy(GetComponent<FixedJoint>());
        StackCollector.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;
        PlayKickAnim();
        //Jump up
        Rigidbody.velocity = new Vector3(0f, 5f, -5f);
        GameManager.instance.TimeController.DoSlowMotion();
        Debug.Log("11");
        GameManager.instance.GameState = GameStates.GamePaused;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Coin")
        {
            GameManager.instance.CoinController.CollectedCoins++;
            other.GetComponent<Coin>().DisAppear();
        }
    }

    #region Animation Event

    public void AtEndOfKickingAnim()
    {
        GameManager.instance.SmothFollow.GoForward();
        StackCollector.ResetJointSettings();
        StackCollector.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;
    }

    #endregion
}