using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static Action<FoodToMix> OnFoodAdded;
    public static Action OnFoodJumped;
    public static Action OnClientOrder;
    public static Action OnMixClicked;
    
    public static Action OnLevelCompleted;
    public static Action OnLevelFailed;
    public static Action OnNextLevelStarted;
    public static Action<int> OnLevelEnd;

    public static Action OnRedrop;
}
