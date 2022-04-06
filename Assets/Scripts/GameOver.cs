using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public void RestartButton()
    {
        SceneManager.LoadScene("Playground");
    }

    // Update is called once per frame
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
