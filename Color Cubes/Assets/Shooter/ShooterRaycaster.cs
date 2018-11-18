using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRaycaster : MonoBehaviour
{
    public Transform shootPoint = null;
    public GameObject CurrentTarget { get { return currentTarget; } }

    [SerializeField] float shootAngle = 45;
    [SerializeField] float shootRadius = 5f;
    [SerializeField] LayerMask shootAbleLayer;

    private GameObject currentTarget = null;


    private void Update()
    {
        UpdateTarget();
    }


    private void UpdateTarget()
    {
        // getting all targets from ShootSphere
        Collider[] colliders = Physics.OverlapSphere(shootPoint.position + shootPoint.right * shootRadius, shootRadius, shootAbleLayer);
        foreach (Collider col in colliders)
        {
            Transform target = col.transform;

            Vector3 targetDirection = target.position - shootPoint.position;

                // checking whether current target is in shooting Angle
            float angleBetweenTarget = Vector3.Angle(shootPoint.right, targetDirection);

            if (angleBetweenTarget <= shootAngle)
            {
                currentTarget = col.gameObject;
                print(currentTarget);
                return;

            }
            else
            {
                currentTarget = null ;
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(shootPoint.position + shootPoint.transform.right * shootRadius, shootRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(shootPoint.position, shootPoint.right.normalized * shootRadius * 2);
        if (currentTarget!=null)
        {
            Gizmos.DrawSphere(currentTarget.transform.position,0.4f);
        }
       
        
        
       

    }

    
}
