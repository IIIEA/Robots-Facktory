using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PartsPlacer : MonoBehaviour
{
    [SerializeField] private Transform _startPlacePoint;
    [Range(1,4)]
    [SerializeField] private int _maxRobotsParts;

    private List<DraggedObject> _draggedObjects = new List<DraggedObject>();

    private void Awake()
    {
        SetPlaces();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_draggedObjects.Count >= _maxRobotsParts)
            return;

        if(collision.TryGetComponent(out DraggedObject draggedObject))
        {
            draggedObject.SetStandPosition(GetPlace());
            draggedObject.transform.SetParent(transform);
            _draggedObjects.Add(draggedObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DraggedObject draggedObject))
        {
            draggedObject.ResetStandPosition();
            if (_draggedObjects.Contains(draggedObject))
                _draggedObjects.Remove(draggedObject);
        }
    }

    private Vector2 GetPlace()
    {
        return Vector2.zero;
    }

    private void SetPlaces()
    {

    }
}
