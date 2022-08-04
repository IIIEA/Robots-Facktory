using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DraggedObject), typeof(Animator))]
public class EffectsController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private DraggedObject _draggedObject;

    private static readonly int PickUp = Animator.StringToHash("PickUp");

    private void Awake()
    {
        _draggedObject = GetComponent<DraggedObject>();
    }

    private void OnEnable()
    {
        _draggedObject.DragBegined += OnDragBegined;
    }

    private void OnDisable()
    {
        _draggedObject.DragBegined -= OnDragBegined;
    }

    private void OnDragBegined(DraggedObject draggedObject)
    {
        _animator.SetTrigger(PickUp);
    }
}

public enum Animations
{
    PickUp
}
