using System.Collections;
using UnityEngine;


public class FlagPlacementValidator : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Material _validMaterial;
    [SerializeField] private Material _invalidMaterial;
    [SerializeField] private LayerMask _groundLayer; 
    [SerializeField] private LayerMask invalidPlacementLayers;
    
    private float _raycastLength = 1f;
    private float _zCenterDivider = 2f;

    private Coroutine _validatePlacementCoroutine;

    public bool IsValidPlace { get; private set; } = false;

    public void StartValidatePlacementCoroutine()
    {
        StopValidatePlacementCoroutine();

        _validatePlacementCoroutine = StartCoroutine(ValidatePlacement());
    }

    public void StopValidatePlacementCoroutine()
    {
        if (_validatePlacementCoroutine != null)
        {
            StopCoroutine(_validatePlacementCoroutine);
        }
    }

    public void SetOnRenderer()
    {
        _renderer.enabled = true;
    }

    public void SetOffRenderer()
    {
        _renderer.enabled = false;
    }

    private IEnumerator ValidatePlacement()
    {
        while (true)
        {
            Vector3 center = _boxCollider.bounds.center;
            Vector3 halfExtents = _boxCollider.bounds.extents;
            Collider[] overlaps = Physics.OverlapBox(center, halfExtents, Quaternion.identity, invalidPlacementLayers);

            if (overlaps.Length <= 0 && IsFlagPlacementOnGround())
            {
                _renderer.material = _validMaterial;
                IsValidPlace = true;

            }
            else
            {
                _renderer.material = _invalidMaterial;
                IsValidPlace = false;
            }

            yield return null;
        }
    }

    private bool IsFlagPlacementOnGround()
    {
        Vector3[] corners = GetColliderCorners();

        foreach (var corner in corners)
        {
            if (!IsPointOnGround(corner))
            {
                return false;
            }
        }

        return true;
    }

    private Vector3[] GetColliderCorners()
    {
        Bounds bounds = _boxCollider.bounds;

        return new Vector3[]
        {
        bounds.min,
        bounds.max,
        new Vector3(bounds.min.x, bounds.min.y, bounds.max.z),
        new Vector3(bounds.max.x, bounds.min.y, bounds.min.z), 
        new Vector3(bounds.min.x, bounds.min.y, bounds.min.z + bounds.size.z / _zCenterDivider), 
        new Vector3(bounds.max.x, bounds.min.y, bounds.min.z + bounds.size.z / _zCenterDivider), 
        };
    }

    private bool IsPointOnGround(Vector3 point)
    {
        if (Physics.Raycast(point, Vector3.down, out RaycastHit hit, _raycastLength, _groundLayer))
        {
            return hit.collider != null;
        }

        return false; 
    }
}
