using UnityEngine;

public class InputHandler
{
<<<<<<< HEAD
    private LayerMask _ignoreRaycast;
    public Camera Camera { get; private set; }
    public GameInput Input { get; private set; }

    public InputHandler(Camera camera, GameInput input, LayerMask ignoreRaycast)
    {
        Camera = camera;
        Input = input;
        _ignoreRaycast = ignoreRaycast;
=======
    public Camera Camera { get; private set; }
    public GameInput Input { get; private set; }

    public InputHandler(Camera camera, GameInput input)
    {
        Camera = camera;
        Input = input;
>>>>>>> ec5cbbcbc3b4ad89f95722c6c941dafc1256bde8
    }

    public RaycastHit GetCursorPosition()
    {
        Vector2 pointerScreen = Input.Mouse.MousePosition.ReadValue<Vector2>();
        Ray ray = Camera.ScreenPointToRay(pointerScreen);
<<<<<<< HEAD

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, ~_ignoreRaycast))
        {
            return hit;
        }

        return default;
=======
        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue);

        return hit;
>>>>>>> ec5cbbcbc3b4ad89f95722c6c941dafc1256bde8
    }

    public bool IsCursorOverUI()
    {
        return UnityEngine.EventSystems.EventSystem.current != null &&
               UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}
