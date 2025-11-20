using _02_Scripts.Core.Managers;
using TMPro;
using UnityEngine;

public class PlayerItemUI : MonoBehaviour
{
    // 빠르게!!! 따로 슬롯 안 만들고 4개만 고려해서!! 
    [SerializeField] private GameObject axNotExistSign;
    [SerializeField] private GameObject swordNotExistSign;
    [SerializeField] private TextMeshProUGUI mealCountText;
    [SerializeField] private TextMeshProUGUI waterCountText;


    [Header("Debug")]
    [SerializeField] private ItemData water;
    // 인벤토리에 아이템 추가할 때 이벤트가 있으면 좋게따!
    private Inventory inventory;
    
    private void Start()
    {
        // 냅다 가져와
        inventory = GameManager.Instance.Player.Inventory;

        UpdateSlot();
    }

    
    private void Update()
    {
        // 돌려~~~
        UpdateSlot();
    }
    

    void UpdateSlot()
    {
        if(inventory ==null) return;
        
        
        // 나중에 inventory 에서 원하는 아이템의 갯수를 뽑을 수 있는 함수를 만들어 주시면 너무 좋을 것 같슴당
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            if (inventory.Items[i].DisplayName == "도끼")
            {
                axNotExistSign.SetActive(false);
            }
            else if (inventory.Items[i].DisplayName == "칼")
            {
                swordNotExistSign.SetActive(false);
            }
            else if (inventory.Items[i].DisplayName == "정화수")
            {
                waterCountText.text = inventory.Items[i].Count.ToString();
            }
            else if (inventory.Items[i].DisplayName == "식량")
            {
                mealCountText.text = inventory.Items[i].Count.ToString();
            }
        }
        
    }
    
    

}
