using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    Transform thisTransform = null;

   [SerializeField] private float rotateSpeed=360f;

    private void Awake()
    {
        thisTransform = GetComponent<Transform>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // TODO take rotValue from swipes

        float rotValue = Input.GetAxis("Vertical");
        transform.localRotation *= Quaternion.AngleAxis(rotValue * rotateSpeed * Time.deltaTime, Vector3.up);

        
		
	}
}
