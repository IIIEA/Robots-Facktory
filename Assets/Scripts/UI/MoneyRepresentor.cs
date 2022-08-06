using TMPro;
using UnityEngine;

public class MoneyRepresentor : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _wallet.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _wallet.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int helath)
    {
        _text.text = helath.ToString() + " $";
    }
}
