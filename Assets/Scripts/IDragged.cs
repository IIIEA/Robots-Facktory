using System;
using UnityEngine;

public interface IDragged
{
    event Action DragBeggined;
    event Action DragEnded;

    void Drag(Vector2 position);
}
