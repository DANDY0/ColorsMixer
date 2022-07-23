using UnityEngine;
using UnityEngine.UI;

public class RedropFood : MonoBehaviour
{
  [SerializeField] private Button button;
  private void OnEnable()
  {
    button.onClick.AddListener(Restart);
  }

  private void Restart()
  {
    EventsManager.OnRedrop.Invoke();
  }

  private void OnDisable()
  {
    button.onClick.RemoveListener(Restart);
  }
}
