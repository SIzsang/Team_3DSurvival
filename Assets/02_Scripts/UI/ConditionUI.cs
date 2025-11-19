using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    private Condition targetCondition;

    public Image uiBar;

    float targetScale;
    float nowScale;
    public void Init(Condition _target)
    {
        targetCondition = _target;
        nowScale = targetCondition.GetPercentage();
        targetScale = nowScale;
    }

    private void Update()
    {
        if (targetCondition != null)
        {
            targetScale = targetCondition.GetPercentage();
            nowScale = Mathf.Lerp(nowScale, targetScale, Time.deltaTime * 5.0f);
            uiBar.rectTransform.localScale = new Vector3(nowScale, 1, 1);
            
        }

    }

}
