using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] StarterAssets.MenuThirdPersonController menuBrawler;

    public void StartGame()
    {
        StartCoroutine(StartGameHelper());
    }

    public IEnumerator StartGameHelper()
    {
        menuBrawler.Punch();
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameHelper());
    }

    public IEnumerator QuitGameHelper()
    {
        menuBrawler.Punch();
        yield return new WaitForSeconds(0.75f);
        Application.Quit();
    }
}
