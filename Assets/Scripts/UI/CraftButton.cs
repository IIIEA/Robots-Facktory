using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class CraftButton : MonoBehaviour
{
    [SerializeField] private CraftTable _craftTable;

    private Button _button;
    private Image _image;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _button.interactable = false;
        _image.color = Color.red;
    }

    private void OnEnable()
    {
        _craftTable.PartsOnTablceChanged += OnCraftRead;
        _button.onClick.AddListener(Craft);
    }

    private void OnDisable()
    {
        _craftTable.PartsOnTablceChanged -= OnCraftRead;
        _button.onClick.RemoveListener(Craft);
    }

    private void OnCraftRead(bool isReady)
    {
        _button.interactable = isReady;

        if (isReady)
        {
            _image.color = Color.green;
        }
        else
        {
            _image.color = Color.red;
        }
    }

    private void Craft()
    {
        _button.interactable = false;
        _image.color = Color.red;
    }
}
