using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestScene : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] CameraManager cameraManager;
    
    void Start()
    {
        cameraManager?.SetTarget(player.transform);
        
        InputManager.Instance.EnableInput(EInputActionAssetName.Camera);
    }

}
