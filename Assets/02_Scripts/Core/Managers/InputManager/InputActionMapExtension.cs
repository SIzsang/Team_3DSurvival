using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

// 배운거 써보기
public static class InputActionMapExtension
{
    public static bool TryGetAction(this Dictionary<string, InputActionMap> map, string keyName, string actionName, out InputAction action)
    {
        action = map[keyName].actions.FirstOrDefault(a => a.name == actionName);
        if (action == null) return false;
        return true;
    }
    
    // public static bool TryGetAction(this Dictionary<string, InputActionMap> map, string keyName, string actionName, out InputAction action)
    // {
    //     action = map[keyName].actions.FirstOrDefault(a => a.name == actionName);
    //     if (action == null) return false;
    //     return true;
    // }


    public static bool TryGetAction(this InputActionMap map, string actionName, out InputAction action)
    {
        action = map.FindAction(actionName);
        if (action == null) return false;
        return true;
    }
}
