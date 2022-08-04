using TMPro;
using UnityEngine;

public class CraftScreen : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private TMP_Text _lvlText;
    [SerializeField] private CraftTable _craftTable;
    [SerializeField] private RobotsDataBundle _dataBundle;

    private void Start()
    {
        _lvlText.text = "?";
        _spriteRenderer.sprite = _defaultSprite;
    }

    private void OnEnable()
    {
        _craftTable.CraftLvlCalculated += OnCraftLvlCalculated;
    }

    private void OnDisable()
    {
        _craftTable.CraftLvlCalculated -= OnCraftLvlCalculated;
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
}
