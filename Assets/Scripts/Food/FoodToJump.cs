using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FoodToJump : MonoBehaviour
{
    
    private Dictionary<string, float> rgbColors;
    private Rigidbody rb;
    public FoodType foodType;
    public enum FoodType
    {
        Apple = 0,
        Banana = 1,
        Orange = 2,
        Cherry = 3,
        Tomato = 4,
        Pepper = 5,
        Eggplant = 6
    }

    public GameObject FoodToMix;
    public Transform SpawnTransform;
    public Transform TargetTransform;

    public float AngleInDegrees;

    float gravity = Physics.gravity.y;

    private const string r = "r";
    private const string g = "g";
    private const string b = "b";
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        SpawnTransform.localEulerAngles = new Vector3(-AngleInDegrees, 0f, 0f);
    }
    private void OnMouseDown()
    {
        Shot();
        EventsManager.OnFoodJumped.Invoke();
    }
    private void Shot() {
        Vector3 fromTo = TargetTransform.position - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float angleInRadians = AngleInDegrees * Mathf.PI / 180;

        float v2 = (gravity * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));
        
        GameObject newBullet = Instantiate(FoodToMix, SpawnTransform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody>().velocity = SpawnTransform.forward * v;
        
        gameObject.SetActive(false);
    }


}
