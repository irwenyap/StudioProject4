using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject title;
    public GameObject playButton;
    public GameObject optionsButton;
    public GameObject quitButton;

    public GameObject nameWindow;

    public GameObject mainMenuScene;
    public GameObject onlineLobbyScene;

    public CanvasGroup canvGroup;
    private float duration = 1.0f;

    private float timer;

    private bool doOnce01;
    private bool doOnce02;
    private bool doOnce03;

    private void Awake()
    {
        canvGroup.alpha = 0.0f;
        title.GetComponent<Text>().canvasRenderer.SetAlpha(0.0f);
    }

    private void Start()
    {
        timer = 0.0f;
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 1.0f));
    }

    private void Update()
    {
        if(canvGroup.alpha == 1.0f && title.GetComponent<Text>().canvasRenderer.GetAlpha() == 0.0f)
            title.GetComponent<Text>().CrossFadeAlpha(1.0f, 1.0f, false);

        if(title.GetComponent<Text>().canvasRenderer.GetAlpha() == 1.0f)
            timer += Time.deltaTime;

        if (timer >= 0.1f && !doOnce01)
        {
            LeanTween.rotateX(playButton, 0, 1.0f);
            doOnce01 = true;
        }
        if (timer >= 0.2f && !doOnce02)
        {
            LeanTween.rotateX(optionsButton, 0, 1.0f);
            doOnce02 = true;
        }
        if (timer >= 0.3f && !doOnce03)
        {
            LeanTween.rotateX(quitButton, 0, 1.0f);
            doOnce03 = true;
        }

        // If main menu screen faded, start main menu screen
        if (canvGroup.alpha == 0.0f)
        {
            mainMenuScene.SetActive(false);
            onlineLobbyScene.SetActive(true);
        }
    }

    public void PlayGame()
    {
        nameWindow.SetActive(true);
    }

    public void NameAccept()
    {
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 0.0f));
    }

    public void NameBack()
    {
        nameWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end)
    {
        float counter = 0.0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }
    }
}
