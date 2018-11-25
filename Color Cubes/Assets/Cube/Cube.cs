using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour,IShootable,ICollectable
{
    [Range(0,1)]
    [SerializeField] private float moveSmoothing = 0.5f;
    [SerializeField] private float damage = 1f;

    Rigidbody thisRigidBody=null;
    Transform currentPuller = null;
    float currentPullSpeed = 0f;
    SphereCollider sphereCollider = null;
    Transform thisTransform = null;
    bool isBeingPulled = false;
    Player player = null;

    [SerializeField] ZoneType compatibleZone = ZoneType.NEUTRAL;
 

    private void Awake()
    {
        thisRigidBody = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        thisTransform = transform;
        player = GameObject.FindObjectOfType<Player>();
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
        sphereCollider.isTrigger = false;
        thisRigidBody.isKinematic = false;
        thisRigidBody.AddForce(shootDirection.normalized * shootSpeed,ForceMode.Impulse);

    }

    public GameObject GetPulled(Transform puller, float pullSpeed)
    {
        thisRigidBody.isKinematic = true;
        sphereCollider.isTrigger = true;
        isBeingPulled = true;
        currentPuller = puller;
        currentPullSpeed = pullSpeed;
        return this.gameObject;
    }

    public void Stop()
    {
        thisRigidBody.isKinematic = true;
        isBeingPulled = false;
        sphereCollider.isTrigger = false;
        
    }
    //TODO Camera shake settings
    public void Collect(ZoneType type)
    {
        if (compatibleZone == type)
        {
            CameraShaker.Instance.ShakeOnce(6, 4, 0.1f, 1f);
            Destroy(gameObject);
        }
        else
        {
            // TODO  show cool effects here 
            CameraShaker.Instance.ShakeOnce(2f, 1f, 0.1f, 0.5f);
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void Move(Vector3 moveDirection, float speed, ForceMode forceMode)
    {
        thisRigidBody.AddForce(moveDirection * speed,forceMode);
    }




}
