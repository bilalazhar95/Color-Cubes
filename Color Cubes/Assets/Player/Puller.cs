using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puller : MonoBehaviour
{

    public bool IsPulling { get { return isPulling; }  }
    public bool HasPulledTarget { get { return hasPulledTarget; }  }
    public Transform PullZone { get { return pullZone; } }
    public float PullZoneRadius { get { return pullZoneRadius; } }

    [SerializeField] private Transform pullZone = null;
    [SerializeField] private float pullSpeed = 5f;
    [SerializeField] private float pullZoneRadius = 1f;
    

    PlayerRaycaster shooterRaycaster = null;
    GameObject currentTarget = null;
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

    public void Pull(GameObject targetObject,float pullSpeed,ForceMode forceMode)
    {
        if (paused||IsPulling||hasPulledTarget||pullZone.childCount>0)
        {
            return;
        }
        targetObject = shooterRaycaster.CurrentTarget;
        if (targetObject!=null)
        {
            IShootable shootable = targetObject.transform.GetComponent<IShootable>();
            if (shootable!=null)
            {
                isPulling = true;
                shootable.GetPulled(pullZone,pullSpeed,forceMode);
            }
        }

    }


   

    private void OnPullZoneEnter()
    {
        if (paused || pullZone.childCount>0)
        {
            Debug.Log("cant pull");
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
