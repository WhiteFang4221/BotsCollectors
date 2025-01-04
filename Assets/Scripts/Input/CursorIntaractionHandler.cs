using UnityEngine;
using UnityEngine.InputSystem;

public class CursorIntaractionHandler : MonoBehaviour
{
    private InputHandler _inputHandler;
    private Selectable _currentSelectable;
    private IShowPanel _currentObject;

    private void OnDisable()
    {
        _inputHandler.Input.Mouse.Click.started -= OnClickStarted;
    }

    public void Initialize(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        _inputHandler.Input.Mouse.Click.started += OnClickStarted;
        
    }

    private void LateUpdate()
    {
        SelectObjectUnderCursor();
    }

    private void SelectObjectUnderCursor()
    {
        RaycastHit hit = _inputHandler.GetCursorPosition();

        if (IsColliderExist(hit))
        {
            if (hit.collider.TryGetComponent(out Selectable selectable))
            {
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
        }
    }

    private void OnClickStarted(InputAction.CallbackContext context)
    {
        if (_inputHandler.IsCursorOverUI())
        {
            return;
        }

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
                return;
            }
            else
            {
                HideCurrentPanel();
                return;
            }
        }

        HideCurrentPanel();
    }

    private bool IsColliderExist(RaycastHit hit)
    {

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
    }

    private void HideCurrentPanel()
    {
        if (_currentObject != null)
        {
            _currentObject.HidePanel();
        }
    }
}
