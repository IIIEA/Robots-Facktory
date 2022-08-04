using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class PartsPlacer : MonoBehaviour
{
    [SerializeField] private Transform _startPlacePoint;
    [Range(1,4)]
    [SerializeField] private int _maxRobotsParts;
    [SerializeField] private float _spacing;

    private List<DraggedObject> _draggedObjects = new List<DraggedObject>();
    private List<Vector2> _positions = new List<Vector2>();

    public event Action CountPartsChanged;

    private void Awake()
    {
        CalculatePlaces();
    }

    private void OnDisable()
    {
        if(_draggedObjects.Count != 0)
        {
            foreach (var draggedObject in _draggedObjects)
            {
                draggedObject.DragEnded -= OnDragEnded;
                draggedObject.DragBegined -= OnDragBegined;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_draggedObjects.Count >= _maxRobotsParts)
            return;

        if(collision.TryGetComponent(out DraggedObject draggedObject))
        {
            if (_draggedObjects.Contains(draggedObject) == false)
            {
                _draggedObjects.Add(draggedObject);
            }

            draggedObject.DragEnded += OnDragEnded;
            draggedObject.DragBegined += OnDragBegined;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DraggedObject draggedObject))
        {
            draggedObject.ResetStandPosition();
            draggedObject.DragEnded -= OnDragEnded;
            draggedObject.DragBegined -= OnDragBegined;

            if (_draggedObjects.Contains(draggedObject))
            {
                _draggedObjects.Remove(draggedObject);
            }
        }
    }

    private void OnDragEnded(DraggedObject draggedObject)
    {
        draggedObject.transform.SetParent(transform);
        CountPartsChanged?.Invoke();
        CalculatePlaces();
        draggedObject.SetStandPosition(GetPlace());
        SnapObjectsToPlaces();
    }

    private void OnDragBegined(DraggedObject draggedObject)
    {
        CountPartsChanged?.Invoke();
        CalculatePlaces();
        SnapObjectsToPlaces();
    }

    private Vector2 GetPlace()
    {
        if (_positions.Count <= 0)
            return _startPlacePoint.position;

        return _positions[_positions.Count - 1];
    }

    private void SnapObjectsToPlaces()
    {
        var positions = GetComponentsInChildren<DraggedObject>();

        if (positions.Length == 0)
            return;

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i].transform.DOMove(_positions[i], 0.2f);
        }
    }

    private void CalculatePlaces()
    {
        _positions.Clear();

        var positions = GetComponentsInChildren<DraggedObject>();
       
        if (positions.Length == 0)
            return;

        var firstPositionX = _startPlacePoint.position.x - _spacing * (positions.Length - 1) / 2;
        _positions.Add(new Vector2(firstPositionX, _startPlacePoint.position.y));

        if (positions.Length <= 1)
            return;

        for (int i = 1; i < positions.Length; i++)
        {
            _positions.Add(new Vector2(firstPositionX + _spacing * i, _startPlacePoint.position.y));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        if(_positions.Count != 0)
        {
            for (int i = 0; i < _positions.Count; i++)
            {
                Gizmos.DrawSphere(_positions[i], 0.2f);
            }
        }
    }
}
