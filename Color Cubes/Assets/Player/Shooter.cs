using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float shootingRate = 1f;
    [SerializeField] GameObject strikerPrefab = null;
    [SerializeField] Transform shootPoint=null;
    [SerializeField] float shootForce = 5f;
    [Header("These values does not effect Acquired Target but nearby targets")]
    [SerializeField] float shootExplosionRadius = 1f;
    [SerializeField] float shootExplosionForce = 10f;
    [SerializeField] float upwardsModifier = 0.1f;
    [SerializeField] ForceMode forceMode = ForceMode.Impulse;

    StrikerPool strikerPool = null;

    private float timeToSpawnStriker = 0f;
    private GameObject currentStriker = null;

 



    private void Awake()
    {
        strikerPool = StrikerPool.Instance;
    }

    private void Start()
    {
        if (shootPoint.childCount<0 && Time.time>=timeToSpawnStriker)
        {
            SpawnStriker();
        }
    }

    private void Update()
    {
        if (Time.time>=timeToSpawnStriker && currentStriker==null && shootPoint.childCount==0)
        {
            SpawnStriker();
        }
    }



    public void Shoot()
    {

        if (currentStriker==null) { return; }
     
        IShootable shootable = currentStriker.transform.GetComponent<IShootable>();
        if (shootable != null)
        {
            
            shootable.TakeShot(shootPoint.forward, shootForce, forceMode);
            MakeExplosion();
            currentStriker.transform.SetParent(null);
            currentStriker = null;
            timeToSpawnStriker = Time.time + 1 / shootingRate;
        }
        else
        {
            Debug.Log("No shootable target");
        }
    }

    private void SpawnStriker()
    {
        //currentStriker = Instantiate(strikerPrefab, shootPoint.position, Quaternion.identity);
        currentStriker = strikerPool.GetStrikerFromPool();
        currentStriker.SetActive(true);
        currentStriker.transform.position = shootPoint.position;
        currentStriker.transform.rotation = Quaternion.identity;
        currentStriker.transform.SetParent(shootPoint);
        currentStriker.GetComponent<Rigidbody>().isKinematic = true;

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
            if (rigidbody!=null && col.transform != currentStriker.transform)
            {
                rigidbody.AddExplosionForce(shootExplosionForce,shootPoint.position,shootExplosionRadius,upwardsModifier,forceMode);
            }
        }

    }


}
