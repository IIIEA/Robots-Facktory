using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class CraftScreen : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _craftSprite;
    [SerializeField] private TMP_Text _lvlText;
    [SerializeField] private CraftTable _craftTable;
    [SerializeField] private RobotsDataBundle _dataBundle;

    public event Action CraftComplited;

    private void Start()
    {
        _lvlText.text = "?";
        _spriteRenderer.sprite = _defaultSprite;
    }

    private void OnEnable()
    {
        _craftTable.CraftLvlCalculated += OnCraftLvlCalculated;
        _craftTable.CraftComplited += OnCraftComplited;
    }

    private void OnDisable()
    {
        _craftTable.CraftLvlCalculated -= OnCraftLvlCalculated;
        _craftTable.CraftComplited -= OnCraftComplited;
    }

    private void OnCraftLvlCalculated(int lvl)
    {
        if(_dataBundle.TryGetRobotByLvl(lvl, out RobotData robotData))
        {
            _lvlText.text = lvl.ToString();
            _spriteRenderer.sprite = robotData.Sprite;
        }
        else
        {
            _lvlText.text = "?";
            _spriteRenderer.sprite = _defaultSprite;
        }
    }

    private void OnCraftComplited()
    {
        _lvlText.text = "$";
        _spriteRenderer.sprite = _craftSprite;
        _spriteRenderer.transform.DOShakeRotation(1f);

        CraftComplited?.Invoke();
    }
}
