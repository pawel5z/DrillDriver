using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    private TextMeshProUGUI scoreText;

    private int score = 0;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void Inc()
    {
        score++;
        scoreText.text = score.ToString();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        score = 0;
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText.text = score.ToString();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;    
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
