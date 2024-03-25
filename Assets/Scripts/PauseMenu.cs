using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    // Une variable static est une variable accessible depuis une autre classe
    public GameObject settingsMenuUI;
    public static bool gameIsPaused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    void Paused()
    {

        // Desactiver aussi la lecture des input commme le jump du personnage sinon il va bouger quand on enlevera le le menu de pause
        //GridMovement.instance.enabled = false;

        // Activer notre menu de pause
        pauseMenuUI.SetActive(true);
        // Arreter le temps
        Time.timeScale = 0;
        // Changer le statut du jeu 
        gameIsPaused = true;

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;

    }

    public void OpenSettingsMenu()
    {
        settingsMenuUI.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        settingsMenuUI.SetActive(false);
    }

}
