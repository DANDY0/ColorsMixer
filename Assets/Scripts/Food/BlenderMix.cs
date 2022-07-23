using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class BlenderMix : MonoBehaviour
{

    [SerializeField] private List<FoodToMix> foodToMix;
    [SerializeField] private Color resultColor;
    [SerializeField] private LiquidColor liquidColor;
    [SerializeField] private LvlControl lvlControl;

    [SerializeField] private int percent; 
    
    private void OnEnable()
    {
        EventsManager.OnFoodAdded += OnFoodAdded;
    }
    private void OnFoodAdded(FoodToMix food)
    {
        foodToMix.Add(food);
    }

    public void MixFood()
    {
        Color color = new Color(0,0,0,0);
        
        foreach (var food in foodToMix)
            color += food.GetComponent<Renderer>().materials[food.idMaterial].color;
        
        color /= foodToMix.Count;

        resultColor = new Color(color.r,color.g,color.b,color.a);

        liquidColor.ChangeColor(resultColor);
        liquidColor.gameObject.SetActive(true);
        foodToMix = new List<FoodToMix>();
        EventsManager.OnMixClicked.Invoke();
        CompareColors(resultColor);
    }

    private void CompareColors(Color resultColor)
    {
        var lvlColor = lvlControl.levels[lvlControl.currentLvl].color;
        var rDiff = Mathf.Abs(resultColor.r - lvlColor.r);
        var gDiff = Mathf.Abs(resultColor.g - lvlColor.g);
        var bDiff = Mathf.Abs(resultColor.b - lvlColor.b);
        var Diff = (rDiff + gDiff + bDiff) / 3;
        percent = (int)(100-Diff*100);
        EventsManager.OnLevelEnd.Invoke(percent);
    }
    
    private void OnDisable()
    {
        EventsManager.OnFoodAdded -= OnFoodAdded;
    }

}
