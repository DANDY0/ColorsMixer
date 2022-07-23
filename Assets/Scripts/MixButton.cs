using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MixButton : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private BlenderMix blender;
    private bool canOrder;

    private void OnEnable()
    {
        EventsManager.OnClientOrder += ToggleCanOrder;
        EventsManager.OnMixClicked += ToggleCanOrder;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(canOrder)
            blender.MixFood();
    }

    private void ToggleCanOrder()
    {
        canOrder = !canOrder;
    }
    
    private void OnDisable()
    {
        EventsManager.OnClientOrder -= ToggleCanOrder;
        EventsManager.OnMixClicked -= ToggleCanOrder;
    }
}
