using UnityEngine;
using UnityEngine.InputSystem;

public class CursorIntaractionHandler : MonoBehaviour
{
    private InputHandler _inputHandler;
    private Selectable _hoveredSelectable;
    private Selectable _clickedSelectable;
    private IShowPanel _currentObject;

    private Vector2 _lastMousePosition;

    private void OnDisable()
    {
        _inputHandler.Input.Mouse.Click.started -= OnClickStarted;
        _inputHandler.Input.Mouse.MousePosition.performed -= OnMouseMoved;
    }

    public void Initialize(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        _inputHandler.Input.Mouse.Click.started += OnClickStarted;
        _inputHandler.Input.Mouse.MousePosition.performed += OnMouseMoved;
    }

    private void OnMouseMoved(InputAction.CallbackContext context)
    {
        Vector2 currentMousePosition = context.ReadValue<Vector2>();

        if (_lastMousePosition != currentMousePosition)
        {
            _lastMousePosition = currentMousePosition;
            UpdateHoveredObject();
        }
    }

    private void UpdateHoveredObject()
    {
        RaycastHit hit = _inputHandler.GetCursorPosition();

        if (IsColliderExist(hit))
        {
            if (hit.collider.TryGetComponent(out Selectable selectable))
            {
                if (_hoveredSelectable != selectable)
                {
                    DeselectObject(_hoveredSelectable);
                }

                if (_clickedSelectable != selectable)
                {
                    _hoveredSelectable = selectable;
                    _hoveredSelectable.Select(); 
                }
            }
            else
            {
                DeselectObject(_hoveredSelectable);
            }
        }
        else
        {
            DeselectObject(_hoveredSelectable);
        }
    }

    private void OnClickStarted(InputAction.CallbackContext context)
    {
        if (_inputHandler.IsCursorOverUI())
        {
            return;
        }

        SelectClickedObject();
    }

    private void SelectClickedObject()
    {
        RaycastHit hit = _inputHandler.GetCursorPosition();

        if (IsColliderExist(hit))
        {
            if (hit.collider.TryGetComponent(out IShowPanel @object))
            {
                if (_currentObject != null && _currentObject != @object)
                {
                    HideCurrentPanel();
                }

                _currentObject = @object;
                _currentObject.ShowPanel();
            }
            else
            {
                HideCurrentPanel();
            }

            if (hit.collider.TryGetComponent(out Selectable selectable))
            {
                if (_clickedSelectable != null && _clickedSelectable != selectable)
                {
                    _clickedSelectable.Deselect();
                }

                _clickedSelectable = selectable;
                _clickedSelectable.Select();

                if (_hoveredSelectable == selectable)
                {
                    _hoveredSelectable = null;
                }

                return;
            }
            else
            {
                DeselectObject(_clickedSelectable);
            }
        }
    }

    private bool IsColliderExist(RaycastHit hit)
    {
        return hit.collider != null;
    }

    private void HideCurrentPanel()
    {
        if (_currentObject != null)
        {
            _currentObject.HidePanel();
        }
    }

    private void SelectObject(Selectable selectable)
    {
        _hoveredSelectable = selectable;
        _hoveredSelectable.Select();
    }

    private void DeselectObject(Selectable selectable)
    {
        if (selectable != null)
        {
            selectable.Deselect();
            selectable = null;
        }
    }
}
