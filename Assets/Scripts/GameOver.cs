using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public static class GameOver
{
    // Start is called before the first frame update
    public static void Restart()
    {
        SceneManager.LoadScene("Playground");
    }

    // Update is called once per frame
    public static void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
