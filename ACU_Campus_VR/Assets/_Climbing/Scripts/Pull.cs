using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : MonoBehaviour {


    public SteamVR_TrackedObject controller;

    [HideInInspector]
    public bool canJump;


    [HideInInspector]
    public Vector3 prevPos;
    public float currentRos;
    public Rigidbody Hand;

    [HideInInspector]
    public bool canGrip;

	// Use this for initialization
	void Start ()
    {
        canJump = true;
        prevPos = controller.transform.localPosition;
        currentRos = controller.transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        currentRos = controller.transform.localEulerAngles.y;
        // var rotCheck = Quaternion.Euler(0, currentRos, 0);
        // Debug.Log(rotCheck * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        
        canGrip = true;
        canJump = false;
        
    }

    private void OnTriggerExit(Collider other)
    {
        
        
        canGrip = false;
        canJump = true;
    }
}
