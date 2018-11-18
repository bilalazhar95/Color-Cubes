using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    ShooterRaycaster shooterRaycaster = null;

    [SerializeField] private float pullSpeed = 5f;
    [SerializeField] private ForceMode blastForceMode = ForceMode.Impulse;


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
