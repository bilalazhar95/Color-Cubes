using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    ShooterRaycaster shooterRaycaster = null;

    [SerializeField] private float blastForce=10f;
    [SerializeField] private float blastRadius = 1f;
    [SerializeField] private Vector3 explosionOffset = Vector3.zero;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IShootable shootableTarget = shooterRaycaster.CurrentShootableTarget;
            Vector3 blastPosition = shooterRaycaster.BlastPosition + explosionOffset;
            Shoot(shootableTarget,blastPosition);
            
        }
        
	}

    void Shoot(IShootable shootableTarget, Vector3 blastPosition)
    {
        print("boom");
        if (shootableTarget == null || blastPosition == Vector3.zero)
        {
            print(" cool VFX are shown");
            return;
        }
        shootableTarget.TakeShot(blastForce,blastPosition,blastRadius,blastForceMode);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        if (shooterRaycaster!=null)
        {
            Vector3 blastPosition = shooterRaycaster.BlastPosition + explosionOffset;
            Gizmos.DrawSphere(blastPosition, blastRadius);
        }
       
    }


}
