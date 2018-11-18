using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour,IShootable
{

    Rigidbody rigidbody=null;
    Transform currentPuller = null;
    float currentPullSpeed = 0f;
    BoxCollider boxCollider = null;


   [SerializeField] bool isBeingPulled = false;
   [SerializeField] private float upwardsModifier = 0.1f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Use this for initialization
    void Start ()
    {


    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isBeingPulled)
        {
            Vector3 destinationDirection = currentPuller.position - transform.position;
            rigidbody.AddForce(destinationDirection* Time.deltaTime*currentPullSpeed,ForceMode.Impulse);
        }
		
	}

    public void Shoot(Vector3 shootDirection, ForceMode forceMode)
    {
        boxCollider.isTrigger = false;
        rigidbody.isKinematic = false;
        //TODO shoot speed
        rigidbody.AddForce(shootDirection.normalized * 70f,ForceMode.Impulse);
        
        

    }

    public GameObject Pull(Transform pullDestination, float pullSpeed)
    {
        boxCollider.isTrigger = true;
        isBeingPulled = true;
        currentPuller = pullDestination;
        currentPullSpeed = pullSpeed;
        return this.gameObject;
    }

    public void Stop()
    {
        rigidbody.isKinematic = true;
        isBeingPulled = false;
        boxCollider.isTrigger = true;
        print("has stopped");
        
    }


   
}
