using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject optionsMenu;

    [Header("Scene")]
    public string gameSceneName = "Game";

    // PLAY BUTTON
    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // OPTIONS BUTTON
    public void OpenOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    // BACK BUTTON (OPTIONS → MAIN MENU)
    public void BackToMainMenu()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    // QUIT BUTTON
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
