using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRaycaster : MonoBehaviour
{
    public GameObject CurrentTarget { get { return currentTarget; } }
    public Vector3 HitPosition { private set; get; }
    public Transform shootPoint = null;


    [SerializeField] float shootAngle = 45;
    [SerializeField] float shootRadius = 5f;
    [SerializeField] LayerMask shootAbleLayer;

    private GameObject currentTarget = null;
    

    

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateCurrentTarget();

    }

    private void UpdateCurrentTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(shootPoint.position + shootPoint.right * shootRadius, shootRadius, shootAbleLayer);
        foreach (Collider col in colliders)
        {
            Transform target = col.transform;
            IShootable shootable = target.GetComponent<IShootable>();
            if (shootable != null)
            {
                Vector3 targetDirection = target.position - shootPoint.position;
                float angleBetweenTarget = Vector3.Angle(shootPoint.right, targetDirection);
                if (angleBetweenTarget <= shootAngle)
                {
                    currentTarget = target.gameObject;
                    break;
                }


            }
            currentTarget = null;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shootPoint.position+shootPoint.transform.right*shootRadius,shootRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(shootPoint.position,shootPoint.right.normalized * shootRadius*2);

    }
}
