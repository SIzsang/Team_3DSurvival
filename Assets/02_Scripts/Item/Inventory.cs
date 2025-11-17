using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI rockText;
    [SerializeField] private TextMeshProUGUI waterText;
    [SerializeField] private TextMeshProUGUI meatText;


    [SerializeField] ItemData wood;
    [SerializeField] ItemData rock;
    [SerializeField] ItemData water;
    [SerializeField] ItemData meat;




    void Update()
    {
        SetWood();
        SetRock();
        SetWater();
        SetMeat();
    }

    public void SetWood()
    {
        woodText.text = $"{wood.displayName}  0개";
    }

    public void SetRock()
    {
        rockText.text = $"{rock.displayName}  0개";
    }

    public void SetWater()
    {
        waterText.text = $"{water.displayName}  0개";
    }

    public void SetMeat()
    {
        meatText.text = $"{meat.displayName}  0개";
    }
}
