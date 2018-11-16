using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    ShooterRaycaster shooterRaycaster = null;

    [SerializeField] private float shootForce=10f;
    [SerializeField] private Vector3 explosionOffset = Vector3.zero;


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
        //TODO implement touch to shoot

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (shooterRaycaster.CurrentTarget==null)
            {
                print("no current target");
                return;
            }
            
            IShootable shootable = shooterRaycaster.CurrentTarget.GetComponent<IShootable>();
            if (shootable!=null)
            {
                Vector3 direction = shooterRaycaster.CurrentTarget.transform.position - shooterRaycaster.shootPoint.position; 
                shootable.TakeShot(direction * shootForce);
            }
            
        }
	}

  
}
