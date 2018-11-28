using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Striker : MonoBehaviour,IShootable {

    Rigidbody thisRigidBody = null;
    [SerializeField] float lifeTime = 1f;

	// Use this for initialization
	void Awake ()
    {
        thisRigidBody = GetComponent<Rigidbody>();
	}


    // Update is called once per frame
    void Update ()
    {
		
	}

    public void TakeShot(Vector3 shootDirection, float shootSpeed, ForceMode forceMode)
    {
        thisRigidBody.isKinematic = false;
        thisRigidBody.AddForce(shootDirection.normalized * shootSpeed, ForceMode.Impulse);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("hitable"))
        {
            //Destroy(gameObject, lifeTime);
            StopAllCoroutines();
            StartCoroutine(DisableStrikerAfterTime());
        }
        

    }

    IEnumerator DisableStrikerAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
