using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    ShooterRaycaster shooterRaycaster = null;



    private void Awake()
    {
        shooterRaycaster = GetComponent<ShooterRaycaster>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

       
        
	}

    private void OnDrawGizmos()
    {
       
    }


}
