using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Puller))]
public class PlayerRaycaster : MonoBehaviour
{
    public Transform shootPoint = null;
    public GameObject CurrentTarget { get { return currentTarget; } }

    [SerializeField] LayerMask shootAbleLayer=8;
    [SerializeField] float shootRadius = 5f;
    [SerializeField] float cubeDetectionThickness = 1f;


    private GameObject currentTarget = null;
    private Puller puller = null;

    private void Awake()
    {
        puller = GetComponent<Puller>();
    }


    private void Update()
    {
        UpdateCurrentTarget();

    }

    private void UpdateCurrentTarget()
    {
        //gets target near from shootPoint
        Collider[] colliders = Physics.OverlapSphere(shootPoint.position+shootPoint.right,puller.PullZoneRadius,shootAbleLayer);
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

        print(currentTarget);
        
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(shootPoint.position + shootPoint.transform.right * shootRadius, shootRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(shootPoint.position, shootPoint.right.normalized * shootRadius * 2);
        
       

    }

    
}
