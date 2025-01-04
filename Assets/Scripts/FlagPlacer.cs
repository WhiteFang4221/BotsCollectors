using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlagPlacer : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;
    private FlagPlacementValidator _placementValidator;

    private InputHandler _inputHandler;
    private IFlagKeeper _currentFlagKeeper;
    private Flag _currentFlag;

    private Coroutine _flagPositionCoroutine;

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

    public void ListenMotherbase(IFlagKeeper flagKeeper)
    {
        flagKeeper.FlagGot += GetFlag;
        flagKeeper.FlagKeeperDisabled += UnlistenMotherbase;
    }

    public void UnlistenMotherbase(IFlagKeeper flagKeeper)
    {
        flagKeeper.FlagGot -= GetFlag;
        flagKeeper.FlagKeeperDisabled -= UnlistenMotherbase;
    }

    private void GetFlag(IFlagKeeper flagKeeper)
    {
        if (_currentFlag == null) 
        {
            _currentFlag = Instantiate(_flagPrefab);
        }

        if (_currentFlag.TryGetComponent(out FlagPlacementValidator placementValidator))
        {
            _placementValidator = placementValidator;
        }

        _currentFlagKeeper = flagKeeper;
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
            _placementValidator.SetOffRenderer();
            StopUpdateFlagPositionCoroutine();
            _currentFlagKeeper.SetBuildMotherbasePriority(_currentFlag);
            _inputHandler.Input.CameraMovement.Enable();
            _inputHandler.Input.FlagPlacement.Disable();
        }
    }

    private void CancelPutTheFlag(InputAction.CallbackContext context)
    {
        StopUpdateFlagPositionCoroutine();
        DeleteFlag(_currentFlag);
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