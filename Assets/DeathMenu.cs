using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public static void Restart()
    {
        SceneManager.LoadScene("Playground");
    }

    // Update is called once per frame
    public static void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
