using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomJoint : MonoBehaviour
{
    public Rigidbody ConnectedBody;
    public Vector3 Anchor;
    public Vector3 Axis;
    public bool AutoConfigureConnectedAnchor;
    public Vector3 ConnectedAnchor;
    public Vector3 SecondaryAxis;
    public ConfigurableJointMotion XMotion;
    public ConfigurableJointMotion YMotion;
    public ConfigurableJointMotion ZMotion;
    public ConfigurableJointMotion AngularXMotion;
    public ConfigurableJointMotion AngularYMotion;
    public ConfigurableJointMotion AngularZMotion;
    public CustomJointDrive XDrive;
    public CustomJointDrive YDrive;
    public CustomJointDrive ZDrive;
    public CustomJointDrive AngularXDrive;
    public CustomJointDrive AngularYZDrive;
    public CustomJointDrive SlerpDrive;
    public float MassScale;
    public float ConnectedMassScale;

    private ConfigurableJoint configurableJoint;
    private Rigidbody connectedRigToJoint;

    public void SetUpJoint(int positionDamper, Rigidbody connectedRig)
    {
        if (configurableJoint!=null)
        {
            return;
        }
        gameObject.AddComponent<ConfigurableJoint>();
        configurableJoint = GetComponent<ConfigurableJoint>();

        connectedRigToJoint = connectedRig;
        configurableJoint.connectedBody = connectedRig;
        configurableJoint.autoConfigureConnectedAnchor = AutoConfigureConnectedAnchor;
        configurableJoint.axis = Axis;
        configurableJoint.xMotion = XMotion;
        configurableJoint.yMotion = YMotion;
        configurableJoint.zMotion = ZMotion;
        configurableJoint.angularXMotion = AngularXMotion;
        configurableJoint.angularYMotion = AngularYMotion;
        configurableJoint.angularZMotion = AngularZMotion;

        JointDrive jointXDrive = new JointDrive();
        jointXDrive.positionSpring = XDrive.positionSpring;
        jointXDrive.positionDamper = 0;
        jointXDrive.maximumForce = XDrive.maximumForce;
        JointDrive jointYDrive = new JointDrive();
        jointYDrive.positionSpring = YDrive.positionSpring;
        jointYDrive.positionDamper = YDrive.positionDamper;
        jointYDrive.maximumForce = YDrive.maximumForce;
        JointDrive jointZDrive = new JointDrive();
        jointZDrive.positionSpring = ZDrive.positionSpring;
        jointZDrive.positionDamper = ZDrive.positionDamper;
        jointZDrive.maximumForce = ZDrive.maximumForce;

        configurableJoint.xDrive = jointXDrive;
        configurableJoint.yDrive = jointYDrive;
        configurableJoint.zDrive = jointZDrive;

        JointDrive jointAngularXDrive = new JointDrive();
        jointAngularXDrive.positionSpring = AngularXDrive.positionSpring;
        jointAngularXDrive.positionDamper = AngularXDrive.positionDamper;
        jointAngularXDrive.maximumForce = AngularXDrive.maximumForce;
        JointDrive jointAngularYZDrive = new JointDrive();
        jointAngularYZDrive.positionSpring = AngularYZDrive.positionSpring;
        jointAngularYZDrive.positionDamper = AngularYZDrive.positionDamper;
        jointAngularYZDrive.maximumForce = AngularYZDrive.maximumForce;
        JointDrive jointSlerpDrive = new JointDrive();
        jointSlerpDrive.positionSpring = SlerpDrive.positionSpring;
        jointSlerpDrive.positionDamper = SlerpDrive.positionDamper;
        jointSlerpDrive.maximumForce = SlerpDrive.maximumForce;

        configurableJoint.angularXDrive = jointAngularXDrive;
        configurableJoint.angularYZDrive = jointAngularYZDrive;
        configurableJoint.slerpDrive = jointSlerpDrive;

        configurableJoint.massScale = MassScale;
        configurableJoint.connectedMassScale = ConnectedMassScale;
        configurableJoint.enableCollision = true;
        configurableJoint.enablePreprocessing = false;
    }

    public void EnableJoint()
    {
        configurableJoint.connectedBody = connectedRigToJoint;
    }

    public void DisableJoint()
    {
        if (configurableJoint==null)
        {
            return;
        }
        configurableJoint.connectedBody = null;
        connectedRigToJoint.WakeUp();

        configurableJoint.xMotion = ConfigurableJointMotion.Free;
        configurableJoint.yMotion = ConfigurableJointMotion.Free;
        configurableJoint.zMotion = ConfigurableJointMotion.Free;
        configurableJoint.angularXMotion = ConfigurableJointMotion.Free;
        configurableJoint.angularYMotion = ConfigurableJointMotion.Free;
        configurableJoint.angularZMotion = ConfigurableJointMotion.Free;
    }

    public void BreakForce()
    {
        if (configurableJoint == null)
        {
            return;
        }
        configurableJoint.breakForce = 1000;
    }

    public void BreakTorque()
    {
        if (configurableJoint == null)
        {
            return;
        }
        configurableJoint.breakTorque = 1000;
    }
}
[System.Serializable]
public struct CustomJointDrive
{
    public float positionSpring;
    public float positionDamper;
    public float maximumForce;
}