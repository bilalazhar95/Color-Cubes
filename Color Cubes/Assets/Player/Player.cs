using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;


public class Player : MonoBehaviour,IDamageable
{
    public PlayerStates State { get { return state; } }

    [SerializeField] HealthBar healthbar = null;
    [SerializeField] float maxhealth=100f;
    [SerializeField] ForceMode shootForceMode = ForceMode.Impulse;
    [SerializeField] float shootSpeed = 10f;
    [SerializeField] float pullSpeed = 50f;
    [SerializeField] float pauseTimeAfterShoot = 0.5f;

    PlayerStates state = PlayerStates.READY_TO_PULL;

    Shooter shooter = null;
    float currentHealth;
    CameraShaker cameraShaker=null;
 
    

	// Use this for initialization
	void Awake ()
    {
        shooter = GetComponent<Shooter>();
        currentHealth = maxhealth;
  

	}
	
	// Update is called once per frame
	void Update ()
    {
        

        //This is for debug purpose

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();

            
        }



    }

    public void Shoot()
    {
       
            shooter.Shoot();
           
      
    }

   

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        float normalizedHealth = Mathf.Clamp(currentHealth / maxhealth, 0, 1);
        healthbar.SetBarSize(normalizedHealth);
        
        

    }
}
