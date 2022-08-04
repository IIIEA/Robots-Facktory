using UnityEngine;

public class Grabber : MonoBehaviour
{
    private IDragged _draggedObject;
    private Camera _main;

    private void Awake()
    {
        _main = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_draggedObject == null)
            {
                RaycastHit2D hit = CastRay();

                if (hit.collider == null)
                    return;

                if (hit.collider.TryGetComponent(out IDragged draggedObject))
                {
                    _draggedObject = draggedObject;
                    _draggedObject.BeginDrag(GetMousePosition());
                }
            }
        }
        else if (_draggedObject != null && Input.GetMouseButtonUp(0)) 
        {
            _draggedObject.EndDrag();
            _draggedObject = null;
        }

        if(_draggedObject != null)
        {
            _draggedObject.Drag(GetMousePosition());
        }
    }

    private RaycastHit2D CastRay()
    {
        Vector3 screenMousePositionFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _main.farClipPlane);
        Vector3 screenMousePositionNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _main.nearClipPlane);

        Vector3 worldMousePositionFar = _main.ScreenToWorldPoint(screenMousePositionFar);
        Vector3 worldMousePositionNear = _main.ScreenToWorldPoint(screenMousePositionNear);

        RaycastHit2D hit2D = Physics2D.Raycast(worldMousePositionNear, worldMousePositionFar - worldMousePositionNear);

        return hit2D;
    }

    private Vector2 GetMousePosition()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mouseWorldPosition = _main.ScreenToWorldPoint(mousePosition);

        return mouseWorldPosition;
    }
}
