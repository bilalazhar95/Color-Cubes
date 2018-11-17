using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour,IShootable
{

    Rigidbody rigidbody=null;
   [SerializeField] private float upwardsModifier = 0.1f;

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

    public void TakeShot(float blastForce, Vector3 blastPosition, float blastRadius, ForceMode forceMode)
    {
        rigidbody.velocity = Vector3.zero;
        
        rigidbody.AddExplosionForce(blastForce,blastPosition,blastRadius,upwardsModifier,forceMode);
        
    }

    private void OnCollisionEnter(Collision collision)
    { 
     
    }

   
}
