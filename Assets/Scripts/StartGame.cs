using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : CarController
{
    public TextMeshProUGUI finalScore;
    public Button StartButton;

    private void Start()
    {
        StartButton.onClick.AddListener(() => Start_Game());
        finalScore.text = "Score: " + saveScore + " m";
    }

    private void OnDestroy()
    {
        StartButton.onClick.RemoveAllListeners();
    }

    public void Start_Game()
    {
        ObjectSpawner.Instance = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
