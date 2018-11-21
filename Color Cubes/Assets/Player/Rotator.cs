﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    Transform thisTransform = null;
    PlayerInput playerInput = null;
    
   [Range(0,1)][SerializeField] private float rotateSpeed=0;

    private void Awake()
    {
        thisTransform = GetComponent<Transform>();
        playerInput = FindObjectOfType<PlayerInput>();
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
        float rotValue = playerInput.SwipeDirection.y;
        Quaternion lookRotation = Quaternion.LookRotation(swipeDirection, Vector3.back);
        thisTransform.localRotation = Quaternion.Slerp(thisTransform.localRotation, lookRotation, rotateSpeed);

        
		
	}
}
