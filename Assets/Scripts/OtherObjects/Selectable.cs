using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Selectable : MonoBehaviour
{
    [SerializeField] private Color _selectColor;

    private Color _defaultColor;
    private Renderer _renderer;
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    public void Select()
    {
        _renderer.material.color = _selectColor;
    }

    public void Deselect()
    {
        _renderer.material.color = _defaultColor;
    }
}
