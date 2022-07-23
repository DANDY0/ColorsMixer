using UnityEngine;

[CreateAssetMenu(fileName = "LvlConfig", menuName = "LvlConfigs")]
public class LvlConfig : ScriptableObject
{
    public FoodToJump.FoodType[] lvlFood;
    public Color color;
}
