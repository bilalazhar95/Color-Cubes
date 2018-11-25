using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    Puller puller = null;
    Shooter shooter = null;
    PlayerRaycaster playerRaycaster = null;
    float currentHealth;
 
    

	// Use this for initialization
	void Awake ()
    {
        puller = GetComponent<Puller>();
        shooter = GetComponent<Shooter>();
        playerRaycaster = GetComponent<PlayerRaycaster>();
        currentHealth = maxhealth;

	}
	
	// Update is called once per frame
	void Update ()
    {
        

        UpdatePlayerState();

        //This is for debug purpose

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();

            
        }



    }

    public void Shoot()
    {
        if (state == PlayerStates.BUSY)
        {
            Debug.Log("Busy");
        }
        else if (state == PlayerStates.READY_TO_PULL)
        {
            GameObject currentTarget = playerRaycaster.CurrentTarget;
            puller.Pull(currentTarget, pullSpeed);
        }
        else if (state == PlayerStates.READY_TO_SHOOT)
        {
            GameObject currentTarget = playerRaycaster.CurrentTarget;
            shooter.Shoot(currentTarget, shootSpeed, shootForceMode);
            puller.ReleaseCurrentTarget();
            puller.Pause(pauseTimeAfterShoot);
        }
    }

    private void UpdatePlayerState()
    {
        // Player state management
        if (puller.IsPulling && !puller.HasPulledTarget && puller.PullZone.childCount>0)
        {
            state = PlayerStates.BUSY;
        }
        if (!puller.IsPulling && puller.HasPulledTarget)
        {
            state = PlayerStates.READY_TO_SHOOT;
        }
        if (!puller.IsPulling && !puller.HasPulledTarget)
        {
            state = PlayerStates.READY_TO_PULL;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        float normalizedHealth = Mathf.Clamp(currentHealth / maxhealth, 0, 1);
        healthbar.SetBarSize(normalizedHealth);

    }
}
