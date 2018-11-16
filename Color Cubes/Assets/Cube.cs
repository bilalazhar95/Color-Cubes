using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    Rigidbody rigidbody=null;

    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float explosionRadius = 1f;
    [SerializeField] private Vector3 blastOffset = new Vector3(0f,-0.5f,0f);

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start ()
    {
        rigidbody.AddExplosionForce(explosionForce, transform.position + blastOffset, explosionRadius);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        print("Boom");
     
    }
}
