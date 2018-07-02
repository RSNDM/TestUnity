using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/Move")]
public class _MoveCharac : MonoBehaviour {
    public CharacterController controller;
    public Rigidbody rigidbody;
    public float speed = 1;
	// Use this for initialization
	void Start () {
        rigidbody = this.transform.GetComponent<Rigidbody>();
        controller = this.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("a"))
        {
            controller.SimpleMove(transform.right * -speed);
        }
        if (Input.GetKey("d"))
        {
            controller.SimpleMove(transform.right * speed);
        }
        if (Input.GetKey("w"))
        {
            controller.SimpleMove(transform.forward * speed);
        }
        if (Input.GetKey("s"))
        {
            controller.SimpleMove(transform.forward * -speed);
        }
	}
}
