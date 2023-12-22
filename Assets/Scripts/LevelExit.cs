using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelExit : MonoBehaviour
{
    public Image fadeBlackImage;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Jimmy");

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
    }
    IEnumerator FadeIn()
    {
        fadeBlackImage.color = new Color(0, 0, 0, 1);
        for (float alpha = 1f; alpha >= 0; alpha -= 0.05f)
        {
            fadeBlackImage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator FadeOut()
    {
        //disables the player's movement and animator to they stop moving
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Animator>().enabled = false;
        
        //turns the opacity(alpha value) of the color from 0 (clear) to 1 (black)
        fadeBlackImage.color = new Color(0, 0, 0, 0);
        for (float alpha = 0f; alpha <= 1; alpha += 0.05f)
        {
            fadeBlackImage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.05f);
        }
        fadeBlackImage.color = new Color(0, 0, 0, 1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        yield return null;
    }
}
