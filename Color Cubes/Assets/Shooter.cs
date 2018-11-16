using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    ShooterRaycaster shooterRaycaster = null;

    [SerializeField] private float shootForce=10f;


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
            IShootable shootable = shooterRaycaster.CurrentTarget.GetComponent<IShootable>();
            if (shootable!=null)
            {
                shootable.TakeShot(transform.right * shootForce);
            }
            
        }
	}

  
}
