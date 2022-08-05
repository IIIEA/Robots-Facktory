using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CraftTable : MonoBehaviour
{
    [SerializeField] private PartsPlacer _partsPlacer;
    [SerializeField] private Transform _craftPosition;
    [SerializeField] private Transform _robotSpawnPoint;
    [SerializeField] private Transform _positionToGo;
    [SerializeField] private float _timeToReachPosition;
    [SerializeField] private RobotsDataBundle _robotsDataBundle;

    private RobotPart[] _partsToCraft;

    public event Action<bool> PartsOnTablceChanged;
    public event Action<int> CraftLvlCalculated;
    public event Action CraftComplited;
    public UnityEvent Craftted;

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

    //Call with button event
    public void Craft()
    {
        if (_partsToCraft.Length <= 0)
            return;

        int lvl = 0;

        foreach (var part in _partsToCraft)
        {
            lvl += part.Lvl;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(part.transform.DOMoveY(_craftPosition.position.y, 0.3f).SetEase(Ease.OutBack, 6f));
            sequence.Append(part.transform.DOMoveX(_craftPosition.position.x, 0.15f).SetEase(Ease.InBack));
            sequence.Append(part.transform.DOScale(0, 0.15f));

            StartCoroutine(DestroyPart(sequence, part.gameObject));
        }

        StartCoroutine(SpawnRobot(lvl));

        CraftComplited?.Invoke();
        Craftted?.Invoke();
    }

    private IEnumerator SpawnRobot(int lvl)
    {
        yield return new WaitForSeconds(0.45f);

        if (_robotsDataBundle.TryGetRobotByLvl(lvl, out RobotData robotData))
        {
            try
            {
                var robot = Instantiate(robotData.RobotTemplate, _robotSpawnPoint.position, Quaternion.identity);
                robot.Init(_positionToGo.position, _timeToReachPosition);
                robot.transform.localScale = Vector3.zero;
                robot.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack, 5f);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

        }
    }

    private IEnumerator DestroyPart(Sequence sequence, GameObject gameObject)
    {
        yield return sequence.WaitForCompletion();

        Destroy(gameObject);
    }
}

