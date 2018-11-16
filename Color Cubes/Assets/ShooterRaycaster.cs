using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRaycaster : MonoBehaviour
{
    public GameObject CurrentTarget { get { return currentTarget; } }

    [SerializeField] Transform shootPoint = null;
    [SerializeField] float shootRange = 5f;

    private GameObject currentTarget = null;
    

    

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetFacingTarget();

    }

    private void GetFacingTarget()
    {
        RaycastHit hit;
        Ray ray = new Ray(shootPoint.transform.position, shootPoint.transform.right);
        if (Physics.Raycast(ray, out hit, shootRange))
        {
            currentTarget = hit.collider.gameObject;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(shootPoint.transform.position, transform.right * shootRange);
    }
}
