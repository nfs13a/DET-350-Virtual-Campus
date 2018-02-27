using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingManager : MonoBehaviour
{
    // This is your camera rig(play area)
    public Rigidbody Body;

    public Pull left;
    public Pull right;

    public float sprintLimiter, walkLimiter;

    [HideInInspector]
    public bool  onGround, ableToJump;
    private Vector3 rightMovement, leftMovement, rightDirection, leftDirection;
    private Quaternion rightRotation, leftRotation;
    private float rightSpeed, leftSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var lDevice = SteamVR_Controller.Input((int)left.controller.index);
        var rDevice = SteamVR_Controller.Input((int)right.controller.index);

        bool isGripped = left.canGrip || right.canGrip;

        bool isJumping = left.canJump && right.canJump;

        if (isJumping  && ableToJump)
        {
            if (rDevice.GetPress(SteamVR_Controller.ButtonMask.Trigger) && lDevice.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                Body.useGravity = false;
                Body.isKinematic = true;
                Body.transform.position += (right.prevPos - right.transform.localPosition);
            }
            else if (rDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger) || lDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {

                Body.useGravity = true;
                Body.isKinematic = false;
                Body.velocity = (right.prevPos - right.transform.localPosition) / Time.deltaTime;
                ableToJump = false;
                onGround = false;
            }
        }

        if (isGripped)
        {
            if (left.canGrip && lDevice.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                Body.useGravity = false;
                Body.isKinematic = true;
                Body.transform.position += (left.prevPos - left.transform.localPosition);
            }
            else if (left.canGrip && lDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {

                Body.useGravity = true;
                Body.isKinematic = false;
                Body.velocity = (left.prevPos - left.transform.localPosition) / Time.deltaTime;
                ableToJump = false;
                onGround = false;
            }
            if (right.canGrip && rDevice.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                Body.useGravity = false;
                Body.isKinematic = true;
                Body.transform.position += (right.prevPos - right.transform.localPosition);
            }
            else if (right.canGrip && rDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {

                Body.useGravity = true;
                Body.isKinematic = false;
                Body.velocity = (right.prevPos - right.transform.localPosition) / Time.deltaTime;
                ableToJump = false;
                onGround = false;
            }

        }
        else
        {
            Body.useGravity = true;
            Body.isKinematic = false;
        }
        if (!isGripped && onGround && (lDevice.GetPress(SteamVR_Controller.ButtonMask.Grip) || rDevice.GetPress(SteamVR_Controller.ButtonMask.Grip)))
        {
            rightMovement = (right.prevPos - right.transform.localPosition) / Time.deltaTime;
            rightSpeed = rightMovement.magnitude;
            rightRotation = Quaternion.Euler(0, right.currentRos, 0);
            rightDirection = rightRotation * Vector3.forward;

            leftMovement = (left.prevPos - left.transform.localPosition) / Time.deltaTime;
            leftSpeed = leftMovement.magnitude;
            leftRotation = Quaternion.Euler(0, left.currentRos, 0);
            leftDirection = leftRotation * Vector3.forward;

            if (rightSpeed > walkLimiter)
                rightSpeed = walkLimiter;
            if (leftSpeed > walkLimiter)
                leftSpeed = walkLimiter;



            var bothDirection = rightDirection + leftDirection;
            var bothSpeed = rightSpeed + leftSpeed;
            if (bothSpeed > sprintLimiter)
                bothSpeed = sprintLimiter;

            if (rDevice.GetPress(SteamVR_Controller.ButtonMask.Grip) && !lDevice.GetPress(SteamVR_Controller.ButtonMask.Grip) && onGround)
            {
                Body.velocity = rightDirection * rightSpeed;
            }
            else if (lDevice.GetPress(SteamVR_Controller.ButtonMask.Grip) && !rDevice.GetPress(SteamVR_Controller.ButtonMask.Grip) && onGround)
            {
                Body.velocity = leftDirection * leftSpeed;
            }
            else 
            {
                Body.velocity = bothDirection * bothSpeed;
            }
        }




            left.prevPos = left.transform.localPosition;
        right.prevPos = right.transform.localPosition;
    }
    private void OnCollisionEnter(Collision collision)
    {      
            ableToJump = true;
            onGround = true;
        

    }
    private void OnCollisionStay(Collision collision)
    {

        
           // ableToJump = true;
            //onGround = true;
        
    }
    private void OnCollisionExit(Collision collision)
    {

        
            onGround = false;
        
    }
}