using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Core.Managers;
using _02_Scripts.Narrative;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class HouseObject : MonoBehaviour, IInteractable
{
    private Recipe recipe;
    public GameObject houseObject;

    public void OnInteract()
    {
        string message = "";
        DialogueManager dialogueManager = DialogueManager.Instance;
        var inventory = GameManager.Instance.Player.Inventory;
        Item wood = inventory.Items.Find(x => x.DisplayName == "나무");

        if (wood == null || wood.Count < 10)
        {
            message = "나무가 부족합니다. (필요: 10개)";
            Debug.Log("나무가 부족합니다. (필요: 10개)");
            dialogueManager.StartDialogue(message);
            return;
        }

        // 3. 재료 감소
        wood.AddCount(-10);

        // 3. 집 생성
        houseObject.SetActive(true);
        NarrativeManager.Instance.ProgressAfterHouseComplete();
        dialogueManager.StartDialogue(message);
    }

    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        Instantiate(houseObject, housePoint);
    //    }
    //}
}
