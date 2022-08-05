using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DraggedObject), typeof(Animator))]
public class EffectsController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _pickUpSound;
    [SerializeField] private AudioClip _putSounds;

    private DraggedObject _draggedObject;

    private static readonly int PickUp = Animator.StringToHash("PickUp");

    private void Awake()
    {
        _draggedObject = GetComponent<DraggedObject>();
    }

    private void OnEnable()
    {
        _draggedObject.DragBegined += OnDragBegined;
        _draggedObject.ObjectPlaced += OnObjectPlaced;
    }

    private void OnDisable()
    {
        _draggedObject.DragBegined -= OnDragBegined;
        _draggedObject.ObjectPlaced -= OnObjectPlaced;
    }

    private void OnDragBegined(DraggedObject draggedObject)
    {
        _audio.PlayOneShot(_pickUpSound);
        _animator.SetTrigger(PickUp);
    }

    private void OnObjectPlaced()
    {
        _audio.PlayOneShot(_putSounds);
    }
}

public enum Animations
{
    PickUp
}
