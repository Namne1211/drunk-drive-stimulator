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
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    private System.Random rando = new System.Random();
    private bool ApplyDrunkEffect;
    private float offSteerStart;
    private bool startApply = true;

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

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        GetDrunkEffectStatus();
        Debug.Log(rearLeftWheelCollider.motorTorque);
    }


    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        var third = true;
        //apply drunk effect to motor for 5 seconds then the next 5 seconds to steering
        if (ApplyDrunkEffect)
        {
            //calculate 1/3 chance for breaking and accelerating to work
            if (rando.Next(3) == 0)
            {
                third = true;
            }
            else
                third = false;
        }
        if (third == true)
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
        
        if (!ApplyDrunkEffect)
        {
            if (startApply)
            {
                offSteerStart = Time.time;
                startApply = false;
            }
            horizontalInput += (Time.time-offSteerStart+1)/10;
        }
        else
        {
            startApply = true;
        }

        //check for input and steer the wheel with maximum angle
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
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
