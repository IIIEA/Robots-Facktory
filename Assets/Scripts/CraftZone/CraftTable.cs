using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CraftTable : MonoBehaviour
{
    [SerializeField] private PartsPlacer _partsPlacer;
    [SerializeField] private Transform _craftPosition;

    private RobotPart[] _partsToCraft;

    public event Action<bool> PartsOnTablceChanged;
    public event Action<int> CraftLvlCalculated;
    public event Action CraftComplited;

    private void OnEnable()
    {
        _partsPlacer.CountPartsChanged += OnPartsCountChanged;
    }

    private void OnDisable()
    {
        _partsPlacer.CountPartsChanged -= OnPartsCountChanged;
    }

    private void OnPartsCountChanged()
    {
        _partsToCraft = GetComponentsInChildren<RobotPart>();
        int lvl = 0;

        for (int i = 0; i < _partsToCraft.Length; i++)
        {
            lvl += _partsToCraft[i].Lvl;
        }

        CraftLvlCalculated?.Invoke(lvl);
        PartsOnTablceChanged?.Invoke(lvl != 0);

    }

    public void Craft()
    {
        if (_partsToCraft.Length <= 0)
            return;

        foreach (var part in _partsToCraft)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(part.transform.DOMoveY(_craftPosition.position.y, 0.3f).SetEase(Ease.OutBack, 6f));
            sequence.Append(part.transform.DOMoveX(_craftPosition.position.x, 0.15f).SetEase(Ease.InBack));
            sequence.Append(part.transform.DOScale(0, 0.15f));

            StartCoroutine(DestroyPart(sequence, part.gameObject));
        }

        CraftComplited?.Invoke();
    }

    private IEnumerator DestroyPart(Sequence sequence, GameObject gameObject)
    {
        yield return sequence.WaitForCompletion();

        Destroy(gameObject);
    }
}

