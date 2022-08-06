using System;
using UnityEngine;
using UnityEngine.Events;

public class BotHealth : MonoBehaviour
{
    [SerializeField] private int _health;

    public event Action<int> HealthChanged;
    public UnityEvent Dead;

    private void Start()
    {
        HealthChanged?.Invoke(_health);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        HealthChanged?.Invoke(_health);

        if (_health <= 0)
        {
            Dead?.Invoke();
        }
    }
}
