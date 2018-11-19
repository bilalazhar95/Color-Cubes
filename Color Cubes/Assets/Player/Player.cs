using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float shootSpeed = 10f;
    [SerializeField] ForceMode shootForceMode = ForceMode.Impulse;
    [SerializeField] float pullSpeed = 50f;
    [SerializeField] ForceMode pullForceMode = ForceMode.Impulse;
    [SerializeField] float pauseTimeAfterShoot = 0.5f;

    PlayerStates state = PlayerStates.READY_TO_PULL;
    Puller puller = null;
    Shooter shooter = null;
    PlayerRaycaster playerRaycaster = null;


	// Use this for initialization
	void Awake ()
    {
        puller = GetComponent<Puller>();
        shooter = GetComponent<Shooter>();
        playerRaycaster = GetComponent<PlayerRaycaster>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        

        UpdatePlayerState();

        //TODO implement player controls here in this script

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (state == PlayerStates.BUSY)
            {
                Debug.Log("Busy");
            }
            else if (state == PlayerStates.READY_TO_PULL)
            {
                GameObject currentTarget = playerRaycaster.CurrentTarget;
                puller.Pull(currentTarget, pullSpeed, pullForceMode);
            }
            else if (state == PlayerStates.READY_TO_SHOOT)
            {
                GameObject currentTarget = playerRaycaster.CurrentTarget;
                shooter.Shoot(currentTarget, shootSpeed, shootForceMode);
                puller.ReleaseCurrentTarget();
                puller.Pause(pauseTimeAfterShoot);
            }
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
}
