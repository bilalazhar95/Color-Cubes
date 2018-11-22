using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Transform laserTransform = null;
    [SerializeField] private float laserRange = 25;
    [SerializeField] private bool targetSnap = false;
    PlayerRaycaster raycaster = null;
    LineRenderer laserRenderer = null;


	// Use this for initialization
	void Awake ()
    {
        raycaster = GetComponent<PlayerRaycaster>();
        laserRenderer = laserTransform.GetComponent<LineRenderer>();
	}

    private void Start()
    {
    }
    // Update is called once per frame
    void Update ()
    {
        laserRenderer.SetPosition(0, transform.position);

        GameObject currentTarget = raycaster.CurrentTarget;

        

        if (currentTarget!=null && targetSnap)
        {
            
            Vector3 laserHit = currentTarget.transform.position;
            laserRenderer.SetPosition(1, laserHit);
        }
        else
        {
            Vector3 laserDirection = transform.right - transform.localPosition;
            laserRenderer.SetPosition(1, laserDirection * 25);
        }
       

       
	}
}
