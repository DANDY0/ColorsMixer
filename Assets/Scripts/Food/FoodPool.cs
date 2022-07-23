using UnityEngine;
using Random = UnityEngine.Random;

public class FoodPool : MonoBehaviour
{
    [SerializeField] private FoodToJump[] pool;
    [SerializeField] private float spawnRange;

    public void ActivateCustomFood(FoodToJump.FoodType[] newPool)
    {
        OffAllFood();
        foreach (var newFood in newPool)
        {
            foreach (var food in pool)
            {
                if (food.foodType == newFood && !food.gameObject.activeInHierarchy)
                {
                    food.gameObject.SetActive(true);
                    SetPosition(food);
                    break;
                    //
                }
            }
        }
    }

    private void OffAllFood()
    {
        foreach (var food in pool)
            food.gameObject.SetActive(false);
    }
    
    private void SetPosition(FoodToJump food)
    {
        var conPos = transform.position;
        var rX = Random.Range(conPos.x-spawnRange,conPos.x + spawnRange);
        var rZ = Random.Range(conPos.z-spawnRange, conPos.z+spawnRange);
        var y = conPos.y;
        var rPosition = new Vector3(rX, y, rZ);
        food.transform.position = rPosition;
    }
}
