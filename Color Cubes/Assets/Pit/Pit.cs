using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour {

    public ZoneType type = ZoneType.NEUTRAL;

    private void OnTriggerEnter(Collider other)
    {
        ICollectable collectable = other.transform.GetComponent<ICollectable>();
        if (collectable != null && !other.transform.CompareTag("striker"))
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
