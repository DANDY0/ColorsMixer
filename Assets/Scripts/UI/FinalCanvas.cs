using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class FinalCanvas : MonoBehaviour
{
    [SerializeField] private Image successImage;
    [SerializeField] private Image failImage;
    [SerializeField] private TextMeshProUGUI finalPercent;
    [SerializeField] private int percentToWin;
    private float fadeDuration;
    private float nextLvlDelay;
    private void OnEnable()
    {
        fadeDuration = 2f;
        nextLvlDelay = 4f;
        
        EventsManager.OnLevelCompleted += Success;
        EventsManager.OnLevelFailed += Fail;
        EventsManager.OnLevelEnd += ShowPercent;
    }

    private void OnDisable()
    {
        EventsManager.OnLevelCompleted -= Success;
        EventsManager.OnLevelFailed -= Fail;
        EventsManager.OnLevelEnd -= ShowPercent;
    }

    private void Success()
    {
        successImage.DOFade(1f, fadeDuration).SetEase(Ease.InOutFlash);
    }

    private void ShowPercent(int result)
    {
        finalPercent.gameObject.SetActive(true);
        if(result<0)
            finalPercent.text = "0%";
        else
            finalPercent.text = result+"%";

        if (result >= percentToWin)
        {
            EventsManager.OnLevelCompleted?.Invoke();
            StartCoroutine(NextLevel());
        }
        else
        {
            EventsManager.OnLevelFailed?.Invoke();
            StartCoroutine(RestartGame());
        }
    }

    private IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(nextLvlDelay);
        FadeCanvas();
        EventsManager.OnNextLevelStarted.Invoke();
    }
    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(nextLvlDelay);
        FadeCanvas();
        EventsManager.OnRedrop.Invoke();
    }
    private void FadeCanvas()
    {
        float fadeOffDuration = 2f;
        successImage.DOFade(0f, fadeOffDuration).SetEase(Ease.InOutFlash);
        failImage.DOFade(0f, fadeOffDuration);
        finalPercent.gameObject.SetActive(false);
    }
    
    private void Fail()
    {
        float fadeOnDuration = 2f;
        failImage.DOFade(1f, fadeOnDuration);
    }
}
