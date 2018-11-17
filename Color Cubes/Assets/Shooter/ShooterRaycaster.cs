using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRaycaster : MonoBehaviour
{
    public Transform shootPoint = null;

    public IShootable CurrentShootableTarget { get { return currentShootableTarget; } }
    public Vector3 BlastPosition { get { return hitPosition; } }

    [SerializeField] float shootAngle = 45;
    [SerializeField] float shootRadius = 5f;
    [SerializeField] LayerMask shootAbleLayer;

    private Vector3 hitPosition = Vector3.zero;
    private IShootable currentShootableTarget = null;




    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateShootableTarget();

    }

    private void UpdateShootableTarget()
    {
        // getting all targets from ShootSphere
        Collider[] colliders = Physics.OverlapSphere(shootPoint.position + shootPoint.right * shootRadius, shootRadius, shootAbleLayer);
        foreach (Collider col in colliders)
        {
            Transform target = col.transform;

            //checking if current target is shootable
            currentShootableTarget = target.GetComponent<IShootable>();
            if (currentShootableTarget != null)
            {
                Vector3 targetDirection = target.position - shootPoint.position;

                // checking whther current target is in shooting Angle
                float angleBetweenTarget = Vector3.Angle(shootPoint.right, targetDirection);
                if (angleBetweenTarget <= shootAngle)
                {
                    RaycastHit hit;
                    Ray ray = new Ray(shootPoint.position, targetDirection);
                    if (Physics.Raycast(ray, out hit, shootRadius * 2))
                    {
                        hitPosition = hit.point;
                    }
                    break;
                }
                else
                {
                    // if not in shooting angle then null the hit position and target
                    currentShootableTarget = null;
                    hitPosition = Vector3.zero;
                }

            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(shootPoint.position + shootPoint.transform.right * shootRadius, shootRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(shootPoint.position, shootPoint.right.normalized * shootRadius * 2);
        
        
       

    }
}
