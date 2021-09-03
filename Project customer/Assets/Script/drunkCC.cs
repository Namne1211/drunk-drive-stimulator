using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drunkCC : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle = 0;
    private float currentbreakForce;
    private float angle;
    private bool isBreaking;

    private System.Random rando = new System.Random();
    private bool ApplyDrunkEffect;
    private float offSteerStart;
    private bool startApply = true;
    private bool left;


    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    [SerializeField] private Transform steeringWheel;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        GetDrunkEffectStatus();
    }


    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        var fourth = true;
        //apply drunk effect to motor for 5 seconds then the next 5 seconds to steering
        if (ApplyDrunkEffect)
        {
            //calculate 1/4 chance for breaking and accelerating to work
            if (rando.Next(4) == 0)
            {
                fourth = true;
            }
            else
                fourth = false;
        }
        if (fourth == true)
        {
            //adding force to the two front wheels 
            rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
            rearRightWheelCollider.motorTorque = verticalInput * motorForce;
            //cheking if the breaking or not, if not break force equal 0
            currentbreakForce = isBreaking ? breakForce : 0f;
            ApplyBreaking();
        }
        else{
            rearLeftWheelCollider.motorTorque =  0;
            rearRightWheelCollider.motorTorque =  0;
            currentbreakForce = 0f;

        }
    }

    private void ApplyBreaking()
    {
        //break force
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        //if not applying drunk effect to breaking, add an increasing value to stearing in either left or right direction
        if (!ApplyDrunkEffect)
        {
            if (startApply)
            {
                offSteerStart = Time.time;
                left = (rando.Next(2) == 0) ? true : false;
                startApply = false;
            }
            
            if (left)
                angle -= 0.05f;
            else
                angle += 0.05f; 
        }
        else
        {
            startApply = true;
        }

        //if no steering wheel moves towards neutral position
        if ( angle < 0.03 && angle > -0.03 )
        {
            angle = 0;
        }
        else if (horizontalInput == 0 && angle > 0 )
        {
            angle -= 0.03f;
        }
        else if (horizontalInput == 0 && angle < 0)
        {
            angle += 0.03f;
        }

        //make sure drunk turning cant exceed maximum
        angle += horizontalInput/10;
        angle = Mathf.Clamp(angle, -1, 1);


        //check for input and steer the wheel with maximum angle
        currentSteerAngle = maxSteerAngle * angle;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;

        //match steering wheel model to input
          steeringWheel.eulerAngles = new Vector3(steeringWheel.eulerAngles.x, 
          steeringWheel.eulerAngles.y,  -currentSteerAngle);
        
        

    }

    private void UpdateWheels()
    {
        //update wheels visiual
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        //get position of wheels and tranform
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void GetDrunkEffectStatus()
    {
        if (Time.time % 5 == 0 && ApplyDrunkEffect == false)
        {
            ApplyDrunkEffect = true;
        }
        else if (Time.time % 5 == 0)
        {
            ApplyDrunkEffect = false;
        }
    }
}
