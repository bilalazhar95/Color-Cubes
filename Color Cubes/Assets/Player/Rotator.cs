using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    Transform thisTransform = null;
    RotatorInput playerInput = null;
    
   [Range(0,1)][SerializeField] private float rotateSpeed=0;

    private void Awake()
    {
        thisTransform = GetComponent<Transform>();
        playerInput = FindObjectOfType<RotatorInput>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 swipeDirection = playerInput.SwipeDirection;
        if (swipeDirection == Vector2.zero)
        {
            return;
        }
       
        Quaternion lookRotation = Quaternion.LookRotation(swipeDirection, Vector3.forward);
        thisTransform.localRotation = Quaternion.Lerp(thisTransform.localRotation, lookRotation, rotateSpeed);

        
		
	}
}
