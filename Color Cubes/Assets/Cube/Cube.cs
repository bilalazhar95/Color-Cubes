using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour,IShootable
{
    public ForceMode forceMode = ForceMode.Force;

    Rigidbody rigidbody=null;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start ()
    {


    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void TakeShot(Vector3 direction)
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(direction,forceMode);
        
    }

    private void OnCollisionEnter(Collision collision)
    { 
     
    }
}
