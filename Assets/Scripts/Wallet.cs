using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _money;

    public int Money => _money;

    public event Action<int> MoneyChanged;

    private void Start()
    {
        MoneyChanged?.Invoke(_money);
    }

    public bool TrySpendMoney(int amount)
    {
        if (_money - amount < 0)
            return false;

        _money -= amount;
        MoneyChanged?.Invoke(_money);

        return true;
    }

    public void AddMoney(int amount)
    {
        _money += amount;
        MoneyChanged?.Invoke(_money);
    }
}
