using TMPro;
using UnityEngine;
using DG.Tweening;

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
        _text.transform.DOShakeScale(0.2f, 0.5f);
        _text.text = helath.ToString();
    }
}
