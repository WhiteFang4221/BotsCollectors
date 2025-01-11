using UnityEngine;

public class SmoothCameraMovementHandler : ICameraMovementHandler
{
    private readonly CameraMovementProperties _properties;
    private Vector3 _cachedCameraPosition;

    public SmoothCameraMovementHandler(CameraMovementProperties properties)
    {
        _properties = properties;
        _cachedCameraPosition = properties.Transform.position;
    }

    public void Move(Vector3 inputDelta)
    {
        _cachedCameraPosition -= new Vector3(inputDelta.x, 0, inputDelta.z) * _properties.Speed;
        _properties.Transform.position = Vector3.Lerp(_properties.Transform.position, _cachedCameraPosition, Time.deltaTime/ _properties.Smoothness);
    }
}
