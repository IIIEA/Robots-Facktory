using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class BotHealth : MonoBehaviour
{
    [SerializeField] private int _health;

    private Camera _mainCamera;

    public event Action<int> HealthChanged;
    public UnityEvent Dead;

    private void Start()
    {
        _mainCamera = Camera.main;
        HealthChanged?.Invoke(_health);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        _mainCamera.DOShakePosition(0.2f, 0.2f, 25);

        HealthChanged?.Invoke(_health);

        if (_health <= 0)
        {
            Dead?.Invoke();
        }
    }
}
