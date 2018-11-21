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


    [Range(0,1)][SerializeField] private float deadzoneWidth = 0f;


    private Vector2 swipeStart = Vector2.zero;
    private Vector2 swipeEnd = Vector2.zero;
    private Vector2 swipeDirection = Vector2.zero;
    RectTransform thisRectTransform = null;



    // Use this for initialization
    void Awake ()
    {
        thisRectTransform = transform.GetComponent<RectTransform>();
	}

    private void Start()
    {
        deadzoneWidth = deadzoneWidth / thisRectTransform.rect.width;
    }



    // Update is called once per frame
    void Update ()
    {



    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(thisRectTransform, eventData.position, eventData.pressEventCamera, out swipeEnd);
        swipeDirection = (swipeEnd - swipeStart);
        Vector2 rectRelativeVector = new Vector2(SwipeDirection.x/thisRectTransform.rect.width,swipeDirection.y/thisRectTransform.rect.height);
        if (rectRelativeVector.sqrMagnitude<deadzoneWidth)
        {
            swipeDirection = Vector2.zero;
        }
      

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(thisRectTransform,eventData.position,eventData.pressEventCamera,out swipeStart);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        swipeStart = Vector2.zero;
        swipeDirection = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
}
