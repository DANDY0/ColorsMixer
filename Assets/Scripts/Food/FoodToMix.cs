using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;


public class FoodToMix : MonoBehaviour
{
   [SerializeField] private Color color;
   public int idMaterial;
    public Color Color
    {
        get => color;
        set => color = value;
    }
    private void Start()
    { 
        color = GetComponent<Renderer>().materials[idMaterial].color;
    }
    private void OnEnable()
    {
        EventsManager.OnMixClicked += ScaleFood;
        EventsManager.OnFoodAdded.Invoke(this);
    }

    private void ScaleFood()
    {
        StartCoroutine(DestroyFood());
    }

    private IEnumerator DestroyFood()
    {
        var timeTodestroy = 1f;
        transform.DOScale(new Vector3(0,0,0),timeTodestroy);
        yield return new WaitForSeconds(timeTodestroy);
    }
    private void OnDisable()
    {
        EventsManager.OnMixClicked -= ScaleFood;
    }
    
    
}
