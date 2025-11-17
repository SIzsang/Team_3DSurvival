using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// 플레이어가 해당 오브젝트에 타게팅한 후,
    /// 상호작용 버튼 눌렀을 때 작동할 내용을 구현해 주세요! 
    /// </summary>
    public void OnInteract();
    
    // 타게팅이 됐을 때, 벗어났을 때 할 동작 고려
    // 지금은 있으면 선언하기 귀찮으니 나중에 필요하면 추가 합시다~!
    // public void OnInteractionRangeEnter();
    // public void OnInteractionRangeExit();
}
