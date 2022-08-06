using TMPro;
using UnityEngine;

public class HpRepresenter : MonoBehaviour
{
    [SerializeField] private BotHealth _bot;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _bot.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _bot.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int helath)
    {
        _text.text = helath.ToString();
    }
}
