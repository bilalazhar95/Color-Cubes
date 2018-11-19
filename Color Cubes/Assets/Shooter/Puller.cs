using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puller : MonoBehaviour
{
    public Transform destination = null;

    public bool IsPulling { get { return isPulling; } set { isPulling=value; } }
    public bool HasPulledTarget { get { return hasPulledTarget; } set { hasPulledTarget = value; } }

    [SerializeField] private float pullSpeed = 5f;

    PlayerRaycaster shooterRaycaster = null;
    GameObject currentTarget = null;
    bool isPulling, hasPulledTarget = false;


	// Use this for initialization
	void Awake ()
    {
        shooterRaycaster = GetComponent<PlayerRaycaster>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        DetachAnyChildIfNotNeeded();
    }

    private void DetachAnyChildIfNotNeeded()
    {
        if (!isPulling && !hasPulledTarget && destination.childCount > 0)
        {
            destination.DetachChildren();
        }
    }

    public GameObject Pull(GameObject targetObject,float pullSpeed,ForceMode forceMode)
    {
        targetObject = shooterRaycaster.CurrentTarget;
        if (targetObject!=null)
        {
            IShootable shootable = targetObject.transform.GetComponent<IShootable>();
            if (shootable!=null)
            {
                print("pulling");
                isPulling = true;
                shootable.GetPulled(destination,pullSpeed,forceMode);
            }
        }

        return targetObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        IShootable shootable = other.transform.GetComponent<IShootable>();
        if (shootable!=null && !hasPulledTarget)
        {
            isPulling = false;
            hasPulledTarget = true;
            if (destination.childCount>0)
            {
                destination.DetachChildren();
            }
            other.transform.SetParent( destination);
            shootable.Stop();
            other.transform.position = destination.position;
           
            
        }
        
    }
}
