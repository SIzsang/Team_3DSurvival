
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputHandler : MonoBehaviour
{

    CameraBehaviour behaviour;
    private InputBinder inputBinder;

    public void Init()
    {
        behaviour = GetComponent<CameraBehaviour>();
        BindInputs();
    }

    void BindInputs()
    {
        inputBinder = InputManager.Instance?.GetInputEventBinder(EInputActionAssetName.Camera);

        if (inputBinder != null)
        {
            inputBinder.BindInputEvent(ECameraInputActionName.Look, OnLook);
        }

    }

    void OnLook(InputAction.CallbackContext context)
    {
        behaviour?.UpdateLookInput(context.ReadValue<Vector2>());
        //Debug.Log(context.ReadValue<Vector2>());
    }


}
