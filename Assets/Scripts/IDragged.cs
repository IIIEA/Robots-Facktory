using System;
using UnityEngine;

public interface IDragged
{
    event Action<DraggedObject> DragBegined;
    event Action<DraggedObject> DragEnded;

    void BeginDrag(Vector2 mousePosition);
    void EndDrag();
    void Drag(Vector2 position);
}
