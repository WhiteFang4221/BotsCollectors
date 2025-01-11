using UnityEngine;
using UnityEngine.InputSystem;

public class CursorIntaractionHandler : MonoBehaviour
{
    private InputHandler _inputHandler;
<<<<<<< HEAD
    private Selectable _hoveredSelectable;
    private Selectable _clickedSelectable;
    private IShowPanel _currentObject;

    private Vector2 _lastMousePosition;

    private void OnDisable()
    {
        _inputHandler.Input.Mouse.Click.started -= OnClickStarted;
        _inputHandler.Input.Mouse.MousePosition.performed -= OnMouseMoved;
=======
    private Selectable _currentSelectable;
    private IShowPanel _currentObject;

    private void OnDisable()
    {
        _inputHandler.Input.Mouse.Click.started -= OnClickStarted;
>>>>>>> ec5cbbcbc3b4ad89f95722c6c941dafc1256bde8
    }

    public void Initialize(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        _inputHandler.Input.Mouse.Click.started += OnClickStarted;
<<<<<<< HEAD
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
=======
        
    }

    private void LateUpdate()
    {
        SelectObjectUnderCursor();
    }

    private void SelectObjectUnderCursor()
>>>>>>> ec5cbbcbc3b4ad89f95722c6c941dafc1256bde8
    {
        RaycastHit hit = _inputHandler.GetCursorPosition();

        if (IsColliderExist(hit))
        {
            if (hit.collider.TryGetComponent(out Selectable selectable))
            {
<<<<<<< HEAD
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
=======
                if (_currentSelectable != selectable)
                {
                    DeselectObject();
                }

                SelectObject(selectable);
            }
            else
            {
                DeselectObject();
            }
        } 
        else
        {
            DeselectObject();
>>>>>>> ec5cbbcbc3b4ad89f95722c6c941dafc1256bde8
        }
    }

    private void OnClickStarted(InputAction.CallbackContext context)
    {
        if (_inputHandler.IsCursorOverUI())
        {
            return;
        }

<<<<<<< HEAD
        SelectClickedObject();
    }

    private void SelectClickedObject()
    {
=======
>>>>>>> ec5cbbcbc3b4ad89f95722c6c941dafc1256bde8
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
<<<<<<< HEAD
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

=======
>>>>>>> ec5cbbcbc3b4ad89f95722c6c941dafc1256bde8
                return;
            }
            else
            {
<<<<<<< HEAD
                DeselectObject(_clickedSelectable);
            }
        }
=======
                HideCurrentPanel();
                return;
            }
        }

        HideCurrentPanel();
>>>>>>> ec5cbbcbc3b4ad89f95722c6c941dafc1256bde8
    }

    private bool IsColliderExist(RaycastHit hit)
    {
<<<<<<< HEAD
        return hit.collider != null;
=======

        if (hit.collider != null)
        {
            return true;
        }

        return false;
    } 

    private void SelectObject(Selectable selectable)
    {
        _currentSelectable = selectable;
        _currentSelectable.Select();
    }

    private void DeselectObject()
    {
        if (_currentSelectable != null)
        {
            _currentSelectable.Deselect();
            _currentSelectable = null;
        }
>>>>>>> ec5cbbcbc3b4ad89f95722c6c941dafc1256bde8
    }

    private void HideCurrentPanel()
    {
        if (_currentObject != null)
        {
            _currentObject.HidePanel();
        }
    }
<<<<<<< HEAD

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
=======
>>>>>>> ec5cbbcbc3b4ad89f95722c6c941dafc1256bde8
}
