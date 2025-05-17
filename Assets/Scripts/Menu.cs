using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement; // <-- Add this!

public class Menu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject CreditsMenu;

    public void OnStartClick()
    {
        SceneManager.LoadScene("Game"); // replace "GameScene" with your gameplay scene name
    }

    public void OnCreditsClick()
    {

        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }
    public void OnExitClick()
    {
        Application.Quit();
    }
    public void OnBackClick()
    {
        MainMenu.SetActive(true);
        CreditsMenu.SetActive(false);
    }

}
