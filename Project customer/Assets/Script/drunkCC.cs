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
    private bool phoneUp;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float speedLimit;

    [SerializeField] private float steerResetSpeed = 0.03f;
    [SerializeField] private float drunkSteerOffSpeed = 0.05f;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    [SerializeField] private Transform steeringWheel;
    [SerializeField] private Transform dashBoard;
    [SerializeField] private Transform phone;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        GetDrunkEffectStatus();
        UpdateDashboard();
    }

    void Update()
    {
        handlePhone();
    }
    private void handlePhone()
    {
        if (Input.GetKeyUp(KeyCode.E) && phoneUp == false)
        {
            phone.transform.localPosition = Vector3.Lerp(phone.transform.localPosition, new Vector3(phone.transform.localPosition.x, 1.1f, phone.transform.localPosition.z), 1);
            phoneUp = true;
        }
        else
        if (Input.GetKeyUp(KeyCode.E) && phoneUp)
        {
            phone.transform.localPosition = Vector3.Lerp(phone.transform.localPosition, new Vector3(phone.transform.localPosition.x, 0, phone.transform.localPosition.z), 1);
            phoneUp = false;
        }
    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }
    private void UpdateDashboard()
    {
        dashBoard.eulerAngles = new Vector3(dashBoard.eulerAngles.x,
        dashBoard.eulerAngles.y, 80 - (rb.velocity.magnitude * 5));
    }
    private void HandleMotor()
    {
        var third = false;
        //apply drunk effect to motor for 5 seconds then the next 5 seconds to steering
        if (ApplyDrunkEffect)
        {
            //calculate 1/4 chance for breaking and accelerating to work
            if (rando.Next(3) == 0)
            {
                third = true;
            }

        }
        if (third == true)
        {
            if (rando.Next(2) == 1)
            {
                rearLeftWheelCollider.motorTorque = motorForce;
                rearRightWheelCollider.motorTorque = motorForce;
            }
            else
            {
                currentbreakForce = breakForce;
                ApplyBreaking();
            }

        }

        //adding force to the two front wheels 
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        //cheking if the breaking or not, if not break force equal 0
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();


    }

    private void ApplyBreaking()
    {   
        //limit speed

        if (rb.velocity.magnitude > speedLimit)
        {
            Vector3 normalisedVelocity = rb.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * 20000;

            rb.AddForce(-brakeVelocity);
        }
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
                angle -= drunkSteerOffSpeed;
            else
                angle += drunkSteerOffSpeed;
        }
        else
        {
            startApply = true;
        }

        //if no steering wheel moves towards neutral position
        if (angle < steerResetSpeed && angle > -steerResetSpeed)
        {
            angle = 0;
        }
        else if (horizontalInput == 0 && angle > 0)
        {
            angle -= steerResetSpeed;
        }
        else if (horizontalInput == 0 && angle < 0)
        {
            angle += steerResetSpeed;
        }

        //make sure drunk turning cant exceed maximum
        angle += horizontalInput / 10;
        angle = Mathf.Clamp(angle, -1, 1);


        //check for input and steer the wheel with maximum angle
        currentSteerAngle = maxSteerAngle * angle;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;

        //match steering wheel model to input
        steeringWheel.eulerAngles = new Vector3(steeringWheel.eulerAngles.x,
        steeringWheel.eulerAngles.y, -currentSteerAngle);



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
