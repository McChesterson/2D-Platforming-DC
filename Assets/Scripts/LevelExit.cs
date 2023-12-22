using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelExit : MonoBehaviour
{
    public Image fadeBlackImage;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        StartCoroutine(FadeOut());
        SceneManager.LoadScene(nextSceneIndex);
    }
    IEnumerator FadeIn()
    {
        yield return new WaitForEndOfFrame();
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForEndOfFrame();
    }
}
