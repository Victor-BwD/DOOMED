using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public GameObject quitButton;

    private void Start()
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
            quitButton.SetActive(true);
        #endif

    }

    public void PlayGame()
    {
        StartCoroutine(ChangeScene("Game"));
    }

    IEnumerator ChangeScene(string name)
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        StartCoroutine(Quit());
    }

    IEnumerator Quit()
    {
        yield return new WaitForSeconds(0.3f);
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
