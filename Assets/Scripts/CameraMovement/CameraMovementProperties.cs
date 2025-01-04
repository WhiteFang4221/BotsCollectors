using System;
using UnityEngine;

[Serializable]
public class CameraMovementProperties
{
    [SerializeField] Transform _transform;
    [SerializeField] float _speed = 0.1f;
    [SerializeField] float _smoothness = 0.1f;

    public Transform Transform => _transform;
    public float Speed => _speed;
    public float Smoothness => _smoothness;
}
