using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue; //현제값
    public float startValue; //시작값
    public float maxValue; //최대값
    public float passiveValue; //자동 회복
    public Image uibar; //ui
    
    void Start()
    {
        curValue = maxValue;
    }
    
    void Update()
    {
        uibar.fillAmount = GetPercentage();
    }
    
    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0);
    }
  
}
