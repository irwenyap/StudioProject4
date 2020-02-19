using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    enum BUTTON_HIT
    {
        NONE,
        PLAY,
        OPTIONS
    };

    public GameObject title;
    public GameObject playButton;
    public GameObject optionsButton;
    public GameObject quitButton;

    public GameObject mainMenuScene;
    public GameObject onlineLobbyScene;

    public CanvasGroup canvGroup;
    private float duration = 1.0f;

    private float timer;

    private BUTTON_HIT buttonHit = BUTTON_HIT.NONE;

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

        if (timer >= 0.1f)
        {
            LeanTween.rotateX(playButton, 0, 1.0f);
        }
        if (timer >= 0.2f)
        {
            LeanTween.rotateX(optionsButton, 0, 1.0f);
        }
        if (timer >= 0.3f)
        {
            LeanTween.rotateX(quitButton, 0, 1.0f);
        }

        // Scene swapper
        switch (buttonHit)
        {
            case BUTTON_HIT.NONE:
                {
                    if (canvGroup.alpha == 0.0f)
                    {
                        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 1.0f));
                    }
                }
                break;
            case BUTTON_HIT.PLAY:
                {
                    // If main menu screen faded, start main menu screen
                    if (canvGroup.alpha == 0.0f)
                    {
                        mainMenuScene.SetActive(false);
                        onlineLobbyScene.SetActive(true);
                        buttonHit = BUTTON_HIT.NONE;
                    }
                }
                break;
            case BUTTON_HIT.OPTIONS:
                break;
        }
    }

    public void PlayGame()
    {
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 0.0f));
        buttonHit = BUTTON_HIT.PLAY;
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
