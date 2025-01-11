using UnityEngine;

public class InputHandler
{
    private LayerMask _ignoreRaycast;
    public Camera Camera { get; private set; }
    public GameInput Input { get; private set; }

    public InputHandler(Camera camera, GameInput input, LayerMask ignoreRaycast)
    {
        Camera = camera;
        Input = input;
        _ignoreRaycast = ignoreRaycast;
    }

    public RaycastHit GetCursorPosition()
    {
        Vector2 pointerScreen = Input.Mouse.MousePosition.ReadValue<Vector2>();
        Ray ray = Camera.ScreenPointToRay(pointerScreen);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, ~_ignoreRaycast))
        {
            return hit;
        }

        return default;
    }

    public bool IsCursorOverUI()
    {
        return UnityEngine.EventSystems.EventSystem.current != null &&
               UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}
