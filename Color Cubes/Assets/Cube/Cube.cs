using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour,IShootable,ICollectable
{
    [Range(0,1)]
    [SerializeField] private float moveSmoothing = 0.5f;

    Rigidbody thisRigidBody=null;
    Transform currentPuller = null;
    float currentPullSpeed = 0f;
    BoxCollider boxCollider = null;
    Transform thisTransform = null;
    bool isBeingPulled = false;

    [SerializeField] ZoneType compatibleZone = ZoneType.NEUTRAL;
 

    private void Awake()
    {
        thisRigidBody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        thisTransform = transform;
    }

    // Use this for initialization
    void Start ()
    {


    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (isBeingPulled)
        {
            Vector3 deltaPos = Vector3.MoveTowards(transform.position, currentPuller.position, currentPullSpeed * Time.deltaTime);
            transform.position= Vector3.Lerp(transform.position,deltaPos, currentPullSpeed * moveSmoothing );
        }
		
	}

    public void TakeShot(Vector3 shootDirection, float shootSpeed,ForceMode forceMode)
    {
        boxCollider.isTrigger = false;
        thisRigidBody.isKinematic = false;
        thisRigidBody.AddForce(shootDirection.normalized * shootSpeed,ForceMode.Impulse);

    }

    public GameObject GetPulled(Transform puller, float pullSpeed)
    {
        thisRigidBody.isKinematic = true;
        boxCollider.isTrigger = true;
        isBeingPulled = true;
        currentPuller = puller;
        currentPullSpeed = pullSpeed;
        return this.gameObject;
    }

    public void Stop()
    {
        thisRigidBody.isKinematic = true;
        isBeingPulled = false;
        boxCollider.isTrigger = false;
        
    }

    public void Collect(ZoneType type)
    {
        if (compatibleZone == type)
        {
            Destroy(gameObject);
        }
        else
        {

        }
    }

    public void Move(Vector3 moveDirection, float speed, ForceMode forceMode)
    {
        thisRigidBody.AddForce(moveDirection * speed,forceMode);
    }




}
