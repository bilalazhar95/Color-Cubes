using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour,IPointerClickHandler,IDragHandler,IPointerUpHandler
{
    // TODO Make a swipe deadzone
    //TODO Swipe speed based movement

    public Vector2 SwipeDirection { get { return swipeDirection; } }


    private Vector2 swipeStart = Vector2.zero;
    private Vector2 swipeDirection = Vector2.zero;

    

    // Use this for initialization
    void Start ()
    {
		
	}

  
	
	// Update is called once per frame
	void Update ()
    {



    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 swipeEnd = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        swipeDirection = swipeEnd - swipeStart;
        print(swipeDirection);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        swipeStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        swipeStart = Vector2.zero;
        swipeDirection = Vector3.zero;
    }
}
