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
    Transform thisTransform = null;


   [SerializeField] bool isBeingPulled = false;
   [SerializeField] private float upwardsModifier = 0.1f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        thisTransform = transform;
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
           transform.position=Vector3.MoveTowards(transform.position,currentPuller.position,currentPullSpeed*Time.deltaTime);
            //rigidbody.AddForce(destinationDirection* Time.fixedDeltaTime*currentPullSpeed,ForceMode.Impulse);
        }
		
	}

    public void TakeShot(Vector3 shootDirection, float shootSpeed,ForceMode forceMode)
    {
        boxCollider.isTrigger = false;
        rigidbody.isKinematic = false;
        rigidbody.AddForce(shootDirection.normalized * shootSpeed,ForceMode.Impulse);

    }

    public GameObject GetPulled(Transform puller, float pullSpeed,ForceMode forceMode)
    {
        rigidbody.isKinematic = true;
        boxCollider.isTrigger = true;
        isBeingPulled = true;
        currentPuller = puller;
        currentPullSpeed = pullSpeed;
        return this.gameObject;
    }

    public void Stop()
    {
        rigidbody.isKinematic = true;
        isBeingPulled = false;
        boxCollider.isTrigger = false;
        
    }


   
}
