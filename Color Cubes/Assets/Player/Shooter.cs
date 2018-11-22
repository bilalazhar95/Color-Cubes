using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] Transform shootPoint=null;
    [Header("These values does not effect Acquired Target but nearby targets")]
    [SerializeField] float shootExplosionRadius = 1f;
    [SerializeField] float shootExplosionForce = 10f;
    [SerializeField] float upwardsModifier = 0.1f;
    [SerializeField] ForceMode forceMode = ForceMode.Impulse;

    PlayerRaycaster shooterRaycaster = null;



    private void Awake()
    {
        shooterRaycaster = GetComponent<PlayerRaycaster>();
    }

   

    public void Shoot(GameObject targetObject, float shootSpeed,ForceMode forceMode)
    {
        if (targetObject==null) { return; }
     
        IShootable shootable = targetObject.transform.GetComponent<IShootable>();
        if (shootable != null)
        {
            MakeExplosion();
            shootable.TakeShot(shootPoint.forward, shootSpeed, forceMode);
        }
        else
        {
            Debug.Log("No shootable target");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shootPoint.position,shootExplosionRadius);
    }

    private void MakeExplosion()
    {
        Collider[] colliders= Physics.OverlapSphere(shootPoint.position,shootExplosionRadius);
        foreach (Collider col in colliders)
        {
            Rigidbody rigidbody = col.transform.GetComponent<Rigidbody>();
            if (rigidbody!=null)
            {
                rigidbody.AddExplosionForce(shootExplosionForce,shootPoint.position,shootExplosionRadius,upwardsModifier,forceMode);
            }
        }

    }


}
