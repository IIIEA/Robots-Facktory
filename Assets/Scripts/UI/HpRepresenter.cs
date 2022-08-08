using TMPro;
using UnityEngine;
using DG.Tweening;

public class HpRepresenter : MonoBehaviour
{
    [SerializeField] private BotHealth _bot;
    [SerializeField] private TMP_Text _text;
    [Header("Sprite color")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _colorToBlink;
    [SerializeField] private float _blinkTime = 0.1f;

    private Color _startColor;

    private void Awake()
    {
        _startColor = _spriteRenderer.color;
    }

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
        Sequence colorSequence = DOTween.Sequence();

        colorSequence.Append(_spriteRenderer.DOColor(_colorToBlink, _blinkTime).SetEase(Ease.Linear));
        colorSequence.Append(_spriteRenderer.DOColor(_startColor, _blinkTime).SetEase(Ease.Linear));

        _text.transform.DOShakeScale(0.2f, 0.5f);
        _text.text = helath.ToString();
    }
}
