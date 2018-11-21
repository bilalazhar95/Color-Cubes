using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerActionInput : MonoBehaviour, IPointerClickHandler
{
    Player player = null;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        player.Shoot();
    }
}
