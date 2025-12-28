using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game Stats")]
    public int totalDeaths = 0;
    public int currentLevel = 0;
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    private void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }
    
    public void PlayerDied()
    {
        totalDeaths++;
        Debug.Log($"Total deaths: {totalDeaths}");
    }
    
    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            currentLevel = nextSceneIndex;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Game Complete! No more levels.");
            // You can load a victory screen here
        }
    }
    
    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ResetGame()
    {
        totalDeaths = 0;
        currentLevel = 0;
        SceneManager.LoadScene(0);
    }
}
