using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Player))]
public class Laser : MonoBehaviour
{
    
    [SerializeField] private Transform laserTransform = null;
    [SerializeField] private float laserRange = 25;
    [SerializeField] private bool targetSnapping = false;


    LineRenderer laserRenderer = null;
    Player player = null;
    


	// Use this for initialization
	void Awake ()
    {
      
        laserRenderer = laserTransform.GetComponent<LineRenderer>();
        player = GetComponent<Player>();
	}

    private void Start()
    {
        laserRenderer.SetPosition(0, laserTransform.position);
    }
    // Update is called once per frame
    void Update ()
    {
        laserRenderer.SetPosition(0, laserTransform.position);
        Vector3 laserEndPointInWorldSpace = laserTransform.position + laserTransform.forward * laserRange;
        laserRenderer.SetPosition(1, laserEndPointInWorldSpace);
       

    }

    
}
