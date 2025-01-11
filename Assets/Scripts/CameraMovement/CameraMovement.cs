using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private CameraMovementProperties _properties;

    private InputHandler _inputHandler;
    private ICameraMovementHandler _cameraMovementHandler;
    private bool _isDragEnabled;

    private void Awake()
    {
        _cameraMovementHandler = CreateMovementHandler();
    }

    public void Initialize(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        _inputHandler.Input.CameraMovement.Click.started += OnClickStarted;
        _inputHandler.Input.CameraMovement.Click.canceled += OnClickCanceled;
    }

    private void OnDisable()
    {
        _inputHandler.Input.CameraMovement.Click.started -= OnClickStarted;
        _inputHandler.Input.CameraMovement.Click.canceled -= OnClickCanceled;
    }

    private void Update()
    {
        var inputDelta = ReadInputDelta();
        _cameraMovementHandler.Move(inputDelta);
    }

    private ICameraMovementHandler CreateMovementHandler()
    {
        return new SmoothCameraMovementHandler(_properties);
    }

    private Vector3 ReadInputDelta()
    {
        return ReadMouseInputDelta() + ReadKeyboardInputDelta();
    }

    private Vector3 ReadMouseInputDelta()
    {
        if (_isDragEnabled)
        {
            var mouseDelta = _inputHandler.Input.Mouse.MouseDelta.ReadValue<Vector2>();

            return new Vector3(mouseDelta.x, 0, mouseDelta.y);
        }

        return Vector3.zero;
    }

    private Vector3 ReadKeyboardInputDelta()
    {
        var keyboardDelta = _inputHandler.Input.KeyboardMovement.KeyboardMove.ReadValue<Vector2>();
        return new Vector3(-keyboardDelta.x, 0, -keyboardDelta.y);
    }

    private void OnClickStarted(InputAction.CallbackContext context)
    {
        if (_inputHandler.IsCursorOverUI())
        {
            return;
        }

        if (IsClickedOnGround())
        {
            _isDragEnabled = true;
        }
    }

    private void OnClickCanceled(InputAction.CallbackContext context)
    {
        _isDragEnabled = false;
    }

    private bool IsClickedOnGround()
    {
        RaycastHit hit = _inputHandler.GetCursorPosition();

        if (hit.collider != null && hit.collider.TryGetComponent(out Ground ground))
        {
            return true;
        }

        return false;
    }
}
