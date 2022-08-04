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
    private Camera _mainCamera;

    public event Action DragBeggined;
    public event Action DragEnded;

    public bool IsDragging => _isDragging;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _originalPosition = transform.position;
        _standPosition = _originalPosition;
    }

    //private void OnMouseDown()
    //{
    //    _isDragging = true;

    //    _offset = GetMousePos() - (Vector2)transform.position;
    //}

    //private void OnMouseDrag()
    //{
    //    var mousePosition = GetMousePos();
    //    transform.position = mousePosition - _offset;
    //}

    //private void OnMouseUp()
    //{
    //    _isDragging = false;

    //    transform.DOMove(_standPosition, 0.2f);
    //}

    public void SetStandPosition(Vector2 position)
    {
        _standPosition = position;
    }

    public void ResetStandPosition()
    {
        _standPosition = _originalPosition;
    }

    private Vector2 GetMousePos()
    {
        return _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Drag(Vector2 position)
    {
        transform.position = position;
    }
}