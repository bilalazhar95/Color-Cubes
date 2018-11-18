using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRaycaster : MonoBehaviour
{
    public Transform shootPoint = null;
    public GameObject CurrentTarget { get { return currentTarget; } }

    [SerializeField] LayerMask shootAbleLayer;
    [SerializeField] float shootRadius = 5f;
    [SerializeField] float cubeDetectionThickness = 1f;


    private GameObject currentTarget = null;
    private SphereCollider sphereCollider = null;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }


    private void Update()
    {
        UpdateCurrentTarget();
        print(currentTarget);

    }

    private void UpdateCurrentTarget()
    {
        //gets target near from shootPoint
        Collider[] colliders = Physics.OverlapSphere(shootPoint.position+shootPoint.right,sphereCollider.radius,shootAbleLayer);
        foreach (Collider col in colliders)
        {
            IShootable shootable = col.transform.GetComponent<IShootable>();
            if (shootable!=null)
            {
                currentTarget = col.transform.gameObject;
                return;
            }

        }

        // gets target far from shootpoint
        RaycastHit hit;
        Ray ray = new Ray(shootPoint.position, shootPoint.right);
        if (Physics.SphereCast(ray, cubeDetectionThickness, out hit, shootRadius * 2, shootAbleLayer))
        {
            currentTarget = hit.collider.gameObject;


        }
        else
        {
            currentTarget = null;
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
