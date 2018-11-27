using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour,IMoveable,ICollectable
{ 
   
    [SerializeField] private float damage = 1f;
    [SerializeField] ZoneType compatibleZone = ZoneType.NEUTRAL;

    Rigidbody thisRigidBody = null;
    Player player = null;

   
 

    private void Awake()
    {
        thisRigidBody = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<Player>();
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

    public void Rotate(Vector3 torque,float magnitude ,ForceMode forcemode)
    {
        thisRigidBody.AddTorque(torque , forcemode);
    }




}
