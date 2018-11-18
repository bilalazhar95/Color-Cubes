using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour,IShootable
{

    Rigidbody rigidbody=null;
    Transform currentPuller = null;
    float currentPullSpeed = 0f;


   [SerializeField] bool isBeingPulled = false;
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

    public void Shoot(Vector3 shootDirection, ForceMode forceMode)
    {
       

    }

    public GameObject Pull(Transform puller, float pullSpeed)
    {
        return null;
    }

    public void Stop()
    {
        

    }



    private void OnCollisionEnter(Collision collision)
    { 
     
    }

   
}
