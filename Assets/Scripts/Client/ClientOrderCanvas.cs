using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class ClientOrderCanvas : MonoBehaviour
{
    private Image[] cloud;
    [SerializeField] private LvlControl lvlControl;
    [SerializeField] private Image juiceImage;

    
    private void OnEnable()
    {
        EventsManager.OnClientOrder += CanvasAppearDelay;
        EventsManager.OnLevelCompleted += CanvasDisable;
        EventsManager.OnLevelFailed += CanvasDisable;
        cloud = GetComponentsInChildren<Image>();
        
        juiceImage.color = lvlControl.levels[lvlControl.currentLvl].color;
    }
    private void CanvasAppear()
    {
        foreach (var image in cloud)
            image.DOFade(1f, 1f);
    }
    private void CanvasDisable()
    {
        foreach (var image in cloud)
            image.DOFade(0, .1f);
    }
    private void CanvasAppearDelay()
    {
        Invoke(nameof(CanvasAppear) , 1f);
    }
    
    private void OnDisable()
    {
        EventsManager.OnClientOrder -= CanvasAppearDelay;
        EventsManager.OnLevelCompleted -= CanvasDisable;
        EventsManager.OnLevelFailed -= CanvasDisable;
    }
}
