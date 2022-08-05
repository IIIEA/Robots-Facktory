using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTable : MonoBehaviour
{
    [SerializeField] private PartsPlacer _partsPlacer;

    public event Action<bool> PartsOnTablceChanged;
    public event Action<int> CraftLvlCalculated;

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
        var parts = GetComponentsInChildren<RobotPart>();
        int lvl = 0;

        for (int i = 0; i < parts.Length; i++)
        {
            lvl += parts[i].Lvl;
        }

        CraftLvlCalculated?.Invoke(lvl);
        PartsOnTablceChanged?.Invoke(lvl != 0);

    }

    public void Craft()
    {

    }
}
