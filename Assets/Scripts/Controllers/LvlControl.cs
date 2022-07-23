using UnityEngine;

public class LvlControl : MonoBehaviour
{
    [SerializeField] private FoodPool foodPool;
    [SerializeField] private Follower[] clients;
    public LvlConfig[] levels;
    public int currentLvl { get; private set; }
    
    private void OnEnable()
    {
        EventsManager.OnLevelCompleted += NextLvl;
        EventsManager.OnRedrop += Restart;
    }

    private void Start()
    {
        currentLvl = 0;
        foodPool.ActivateCustomFood(levels[currentLvl].lvlFood);
    }
    
    private void NextLvl()
    {
        clients?[currentLvl].gameObject.SetActive(false);
        currentLvl++;
        if (clients != null && currentLvl<clients.Length)
        {
            foodPool.ActivateCustomFood(levels[currentLvl].lvlFood);
            clients[currentLvl].gameObject.SetActive(true);
        }
    }

    private void Restart()
    {
  
        foodPool.ActivateCustomFood(levels[currentLvl].lvlFood);
    }
}
