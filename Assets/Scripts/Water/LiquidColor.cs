using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LiquidColor : MonoBehaviour
{
    private void Start()
    {
        Shader.SetGlobalFloat("_Fill", -1);
    }

    private void OnEnable()
    {
        EventsManager.OnNextLevelStarted += ResetLiquid;
        EventsManager.OnRedrop += ResetLiquid;
        EventsManager.OnMixClicked += MixClicked;
    }

    public void ChangeColor(Color color)
    {
        Shader.SetGlobalColor("_LiquidColor", color);
        Shader.SetGlobalColor("_FresnelColor", color);
    }

    private void MixClicked()
    {
        StartCoroutine(MoveLiquidUp());
    }
    private IEnumerator MoveLiquidUp()
    {
        float fill = -1f;
        var speed = .6f;
        while (fill < .08f)
        {
            Shader.SetGlobalFloat("_Fill", fill);

            fill += Time.deltaTime * speed;
            yield return null;
        }
    }

    private void ResetLiquid()
    {
        StartCoroutine(MoveLiquidDown());
    }
    private IEnumerator MoveLiquidDown()
    {
        float fill =  Shader.GetGlobalFloat("_Fill");
        var speed = .6f;
        while (fill > -1f)
        {
            Shader.SetGlobalFloat("_Fill", fill);

            fill -= Time.deltaTime * speed;
            yield return null;
        }
    }


    private void OnDisable()
    {
        EventsManager.OnNextLevelStarted -= ResetLiquid;
        EventsManager.OnRedrop -= ResetLiquid;
        EventsManager.OnMixClicked -= MixClicked;
    }
}
