using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void LoadNextScene()
    {
        _text.text = "BITCH";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
