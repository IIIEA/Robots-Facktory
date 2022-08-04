using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(Collider2D))]
public class DraggedObject : MonoBehaviour, IDragged
{
    private Vector2 _offset;
    private Vector2 _standPosition;
    private Vector2 _originalPosition;
    private bool _isDragging;

    public event Action<DraggedObject> DragBegined;
    public event Action<DraggedObject> DragEnded;

    public bool IsDragging => _isDragging;

    private void Awake()
    {
        _originalPosition = transform.position;
        _standPosition = _originalPosition;
    }

    public void SetStandPosition(Vector2 position)
    {
        _standPosition = position;
    }

    public void ResetStandPosition()
    {
        _standPosition = _originalPosition;
    }

    public void BeginDrag(Vector2 mousePosition)
    {
        transform.SetParent(null);
        _offset = mousePosition - (Vector2)transform.position;

        DragBegined?.Invoke(this);
    }

    public void EndDrag()
    {
        DragEnded?.Invoke(this);

        transform.DOMove(_standPosition, 0.2f);
    }

    public void Drag(Vector2 position)
    {
        transform.position = position - _offset;
    }
}