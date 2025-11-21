using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUI : MonoBehaviour
{
    //public RecipeSlots recipeSlot; // 테스트할 슬롯 연결
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    //private void Update() // 테스트용 삭제 예정
    //{
    //    if (Input.GetKeyDown(KeyCode.C))
    //    {
    //        if (recipeSlot != null)
    //        {
    //            // 버튼 클릭 이벤트 직접 호출
    //            recipeSlot.OnClickCreativeForTest();
    //        }
    //    }
    //}
}
