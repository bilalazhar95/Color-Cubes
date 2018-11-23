using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerRaycaster))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Puller))]
public class Laser : MonoBehaviour
{
    
    [SerializeField] private Transform laserTransform = null;
    [SerializeField] private float laserRange = 25;
    [SerializeField] private bool targetSnapping = false;

    PlayerRaycaster raycaster = null;
    LineRenderer laserRenderer = null;
    Player player = null;
    Puller puller = null;
    


	// Use this for initialization
	void Awake ()
    {
        raycaster = GetComponent<PlayerRaycaster>();
        laserRenderer = laserTransform.GetComponent<LineRenderer>();
        player = GetComponent<Player>();
        puller = GetComponent<Puller>();
	}

    private void Start()
    {
        laserRenderer.SetPosition(0, laserTransform.position);
    }
    // Update is called once per frame
    void Update ()
    {
        ToggleSnappingIfNeeded();
        RenderLaser();

    }

    private void ToggleSnappingIfNeeded()
    {
        if (player.State == PlayerStates.READY_TO_SHOOT)
        {
            targetSnapping = false;
        }
        else
        {
            targetSnapping = true;
        }
    }

    private void RenderLaser()
    {
        laserRenderer.SetPosition(0, laserTransform.position);

        GameObject currentTarget = raycaster.CurrentTarget;

        if (currentTarget!=null &&currentTarget.transform.CompareTag("striker") && puller.StrikerOnlyMode && player.State!=PlayerStates.READY_TO_SHOOT)
        {
            targetSnapping = true;
        }
        else
        {
            targetSnapping = false;
        }



        if (targetSnapping)
        {

            Vector3 laserHit = currentTarget.transform.position;
            laserRenderer.SetPosition(1, laserHit);
        }
        else
        {
            Vector3 laserEndPointInWorldSpace = laserTransform.position + laserTransform.forward * laserRange;
            laserRenderer.SetPosition(1, laserEndPointInWorldSpace);

        }
    }
}
