using UnityEngine;

public class InputHandler
{
    public Camera Camera { get; private set; }
    public GameInput Input { get; private set; }

    public InputHandler(Camera camera, GameInput input)
    {
        Camera = camera;
        Input = input;
    }

    public RaycastHit GetCursorPosition()
    {
        Vector2 pointerScreen = Input.Mouse.MousePosition.ReadValue<Vector2>();
        Ray ray = Camera.ScreenPointToRay(pointerScreen);
        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue);

        return hit;
    }

    public bool IsCursorOverUI()
    {
        return UnityEngine.EventSystems.EventSystem.current != null &&
               UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}
