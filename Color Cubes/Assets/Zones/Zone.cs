using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public ZoneType type = ZoneType.NEUTRAL;
    [SerializeField] private Vector3 rotation = Vector3.zero;
    [Tooltip("Angle per second")]
    [SerializeField] private float rotationRate = 0f;




    private void Update()
    {
        transform.Rotate(rotation,rotationRate * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollectable collectable = other.transform.GetComponent<ICollectable>();
        if (collectable!=null && !other.transform.CompareTag("striker"))
        {
            collectable.Collect(type);
            

        }
    }

    private void OnTriggerStay(Collider other)
    {
        ICollectable collectable = other.transform.GetComponent<ICollectable>();
        if (collectable != null && !other.transform.CompareTag("striker"))
        {
            collectable.Collect(type);
            
        }
    }

  
}
