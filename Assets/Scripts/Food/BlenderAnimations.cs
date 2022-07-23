using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BlenderAnimations : MonoBehaviour
{
    [SerializeField] private Transform cover;
    private Vector3 closeCoverPos;
    private Vector3 openCoverPos;

    [SerializeField] private float shakeStrength;
    [SerializeField] private float coverDegrees;
    [SerializeField] private float toggleCoverTime;
    [SerializeField] private float shakeDelay;
    [SerializeField] private float timeToClose;
    private bool canClose;
    
    private Tween currentTween;
    private void Awake()
    {
        EventsManager.OnFoodJumped += OpenBlender;
    }
    private void Start()
    {
        closeCoverPos = cover.localPosition;
        openCoverPos = closeCoverPos + new Vector3(-.1f, .2f, 0);
    }

    private void OpenBlender()
    {
        DOTween.Clear();
        canClose = false;
        StartCoroutine(Timer(timeToClose));
        
        cover.DOLocalRotate(new Vector3(0, 0, coverDegrees), toggleCoverTime);
        cover.DOLocalMove(openCoverPos, toggleCoverTime);
       
        StartCoroutine(DelayToAction(ShakeBlender, shakeDelay));
    }
    
    IEnumerator Timer(float delay)
    {
        var speed = 1;
         while (delay > 0)
        {
            delay -= Time.deltaTime * speed;
            yield return null;
        }
        canClose = true;
        CloseBlender();
    }
    private void CloseBlender()
    {
        if (canClose)
        {
            cover.DOLocalMove(closeCoverPos, toggleCoverTime);
            cover.DOLocalRotate(new Vector3(0,0,0), toggleCoverTime);
        }
    }
    private void ShakeBlender()
    {
        transform.DOShakeRotation(1,new Vector3(0,0,shakeStrength));
    }
    IEnumerator DelayToAction(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
    private void OnDisable()
    {
        EventsManager.OnFoodJumped -= OpenBlender;
    }
}
