using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlagPlacer : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private LayerMask _flagLayerBeforeSet;
    [SerializeField] private LayerMask _flagLayer;
    private IFlagSetter _currentFlagSetter;
    private Flag _currentFlag;
    private InputHandler _inputHandler;
    private FlagPlacementValidator _placementValidator;
    private Coroutine _flagPositionCoroutine;
    private Vector3 _previousFlagPosition;

    private void OnDisable()
    {
        _inputHandler.Input.FlagPlacement.LeftClick.started -= TryPutTheFlag;
        _inputHandler.Input.FlagPlacement.RightClick.started -= CancelPutTheFlag;
        StopUpdateFlagPositionCoroutine();
    }

    public void Initialize(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        _inputHandler.Input.FlagPlacement.LeftClick.started += TryPutTheFlag;
        _inputHandler.Input.FlagPlacement.RightClick.started += CancelPutTheFlag;
        _inputHandler.Input.FlagPlacement.Disable();
    }

    public void ListenMotherbase(IFlagSetter flagSetter)
    {
        flagSetter.FlagGot += TakeFlag;
        flagSetter.FlagSetterDisabled += UnlistenMotherbase;
    }

    public void UnlistenMotherbase(IFlagSetter flagSetter)
    {
        flagSetter.FlagGot -= TakeFlag;
        flagSetter.FlagSetterDisabled -= UnlistenMotherbase;
    }

    private void TakeFlag(IFlagSetter flagSetter, Flag motherbaseFlag)
    {
        if (motherbaseFlag == null)
        {
            _currentFlag = Instantiate(_flagPrefab);
        }
        else
        {
            _currentFlag = motherbaseFlag;
            _previousFlagPosition = _currentFlag.transform.position;
            LayerChanger.SetLayerRecursively(_currentFlag.gameObject, _flagLayerBeforeSet);
        }

        if (_currentFlag.TryGetComponent(out FlagPlacementValidator placementValidator))
        {
            _placementValidator = placementValidator;
        }
        else return;

        _currentFlagSetter = flagSetter;
        _inputHandler.Input.CameraMovement.Disable();
        _inputHandler.Input.FlagPlacement.Enable();
        StartUpdateFlagPositionCoroutine();
        _placementValidator.SetOnRenderer();
    }

    private IEnumerator UpdateFlagPosition()
    {
        while (true)
        {
            RaycastHit hit = _inputHandler.GetCursorPosition();

            if (hit.collider != null && hit.collider.gameObject.TryGetComponent(out Ground ground))
            {
                _currentFlag.transform.position = hit.point;
            }

            yield return null;
        }
    }

    private void TryPutTheFlag(InputAction.CallbackContext context)
    {
        if (_placementValidator.IsValidPlace)
        {
            LayerChanger.SetLayerRecursively(_currentFlag.gameObject, _flagLayer);
            StopUpdateFlagPositionCoroutine();

            if (_currentFlagSetter.CurrentFlag == null)
            {
                _currentFlagSetter.SetBuildMotherbasePriority(_currentFlag);
            }

            _placementValidator.SetOffRenderer();
            _currentFlag = null;
            _inputHandler.Input.CameraMovement.Enable();
            _inputHandler.Input.FlagPlacement.Disable();
        }
    }

    private void CancelPutTheFlag(InputAction.CallbackContext context)
    {
        StopUpdateFlagPositionCoroutine();

        if (_currentFlagSetter.CurrentFlag != null)
        {
            _currentFlag.transform.position = _previousFlagPosition;
            _placementValidator.SetOffRenderer();
        }
        else
        {
            DeleteFlag(_currentFlag);
        }

        _currentFlag = null;
        _inputHandler.Input.FlagPlacement.Disable();
    }

    private void DeleteFlag(Flag flag)
    {
        Destroy(flag.gameObject);
        _inputHandler.Input.CameraMovement.Enable();
    }

    private void StartUpdateFlagPositionCoroutine()
    {
        StopUpdateFlagPositionCoroutine();
        _flagPositionCoroutine = StartCoroutine(UpdateFlagPosition());
        _placementValidator.StartValidatePlacementCoroutine();
    }

    private void StopUpdateFlagPositionCoroutine()
    {
        if (_flagPositionCoroutine != null)
        {
            StopCoroutine(_flagPositionCoroutine);
            _flagPositionCoroutine = null;
            _placementValidator.StopValidatePlacementCoroutine();
        }
    }
}