using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour {

    public Transform head;

    private CapsuleCollider capsuleCollider;


    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    // Use this for initialization
    void Start ()
    {
        //capsuleCollider = GetComponent<CapsuleCollider>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float distanceFromFloor = Vector3.Dot(head.localPosition, Vector3.up);
        capsuleCollider.height = Mathf.Max(capsuleCollider.radius, distanceFromFloor);
        capsuleCollider.center = head.localPosition - 0.5f * distanceFromFloor * Vector3.up;
    }
}
