using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puller : MonoBehaviour
{
    public Transform destination = null;

    [SerializeField] private float pullSpeed = 5f;

    ShooterRaycaster shooterRaycaster = null;
    GameObject targetObject = null;
    bool isPulling, isTargetPulled = false;

	// Use this for initialization
	void Awake ()
    {
        shooterRaycaster = GetComponent<ShooterRaycaster>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //TODO Refactor pulling and shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isPulling && !isTargetPulled)
            {
                Pull();
            }
            if (isTargetPulled)
            {
                GameObject currentTarget = shooterRaycaster.CurrentTarget;
                if (currentTarget != null)
                {

                    IShootable shootable = currentTarget.transform.GetComponent<IShootable>();
                    if (shootable != null)
                    {
                        isPulling = false;
                        isTargetPulled = false;
                        print("Pushing");
                        destination.DetachChildren();
                        shootable.Shoot(transform.right,ForceMode.Impulse);
                    }
                }
            }
            
        }
	}

    public GameObject Pull()
    {
        targetObject = shooterRaycaster.CurrentTarget;
        if (targetObject!=null)
        {
            IShootable shootable = targetObject.transform.GetComponent<IShootable>();
            if (shootable!=null)
            {
                print("pulling");
                isPulling = true;
                shootable.Pull(destination,pullSpeed);
            }
        }

        return targetObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        IShootable shootable = other.transform.GetComponent<IShootable>();
        if (shootable!=null && !isTargetPulled)
        {
            isPulling = false;
            isTargetPulled = true;
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
