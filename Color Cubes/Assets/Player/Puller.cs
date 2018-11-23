using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puller : MonoBehaviour
{

    public bool IsPulling { get { return isPulling; }  }
    public bool HasPulledTarget { get { return hasPulledTarget; }  }
    public Transform PullZone { get { return pullZone; } }
    public float PullZoneRadius { get { return pullZoneRadius; } }
    public bool StrikerOnlyMode { get { return strikerOnly; } set { strikerOnly = value; } }

    [SerializeField] private Transform pullZone = null;
    [SerializeField] private float pullZoneRadius = 1f;
    [SerializeField] bool strikerOnly = true;


    PlayerRaycaster shooterRaycaster = null;
    bool isPulling, hasPulledTarget,paused = false;
    private float unPauseTime = 0f;
   


    // Use this for initialization
    void Awake ()
    {
        shooterRaycaster = GetComponent<PlayerRaycaster>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (paused)
        {
            UnPauseWhenTime();
        }
        DetachAnyChildIfNotNeeded();
        OnPullZoneEnter();
    }

    private void UnPauseWhenTime()
    {
        if (Time.time >= unPauseTime)
        {
            UnPause();
        }
    }

    private void DetachAnyChildIfNotNeeded()
    {
        if (!isPulling && !hasPulledTarget && pullZone.childCount > 0)
        {
            pullZone.DetachChildren();
        }
    }

    public void Pull(GameObject targetObject,float pullSpeed)
    {
        if (paused||IsPulling||hasPulledTarget||pullZone.childCount>0)
        {
            return;
        }
        targetObject = shooterRaycaster.CurrentTarget;
        if (targetObject!=null)
        {
            if (strikerOnly)
            {
                if (!targetObject.transform.CompareTag("striker"))
                {
                    print("cant pull");
                    return;
                }

                IShootable shootable = targetObject.transform.GetComponent<IShootable>();
                if (shootable != null)
                {
                    isPulling = true;
                    shootable.GetPulled(pullZone, pullSpeed);
                }

            }
            else
            {
                IShootable shootable = targetObject.transform.GetComponent<IShootable>();
                if (shootable != null)
                {
                    isPulling = true;
                    shootable.GetPulled(pullZone, pullSpeed);
                }
            }
          
        }

    }


   

    private void OnPullZoneEnter()
    {
        if (paused || pullZone.childCount>0)
        {
            return;
        }

        Collider[] allcolliders = Physics.OverlapSphere(pullZone.position, pullZoneRadius);
        foreach (Collider col in allcolliders)
        {
            IShootable shootable = col.transform.GetComponent<IShootable>();
            if (shootable != null && !hasPulledTarget)
            {
                isPulling = false;
                hasPulledTarget = true;
                if (pullZone.childCount > 0)
                {
                    pullZone.DetachChildren();
                }
                col.transform.SetParent(pullZone);
                shootable.Stop();
                col.transform.position = pullZone.position;


            }
        }
    }

    public void Pause(float pauseTime)
    {
        unPauseTime = Time.time + pauseTime;
        paused = true;
    }

    private void UnPause()
    {
        unPauseTime = 0f;
        paused = false;
    }

    public void ReleaseCurrentTarget()
    {
        isPulling = false;
        hasPulledTarget = false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pullZone.position,pullZoneRadius);
    }


}
