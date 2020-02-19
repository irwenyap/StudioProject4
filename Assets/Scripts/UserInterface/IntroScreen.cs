using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScreen : MonoBehaviour
{
    enum BUTTON_HIT
    {
        NONE,
        CONTINUE,
        ACCEPT,
        BACK
    };

    public GameObject title;
    public GameObject anyKey;

    public GameObject nameWindow;

    public GameObject introScene;
    public GameObject mainMenuScene;

    public CanvasGroup canvGroup;
    private float duration = 1.0f;

    private BUTTON_HIT buttonHit = BUTTON_HIT.NONE;

    private void Awake()
    {
        canvGroup.alpha = 0.0f;
        title.GetComponent<Text>().canvasRenderer.SetAlpha(0.0f);
        anyKey.GetComponent<Text>().canvasRenderer.SetAlpha(0.0f);
    }

    private void Start()
    {
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 1.0f));
    }

    private void Update()
    {
        // Scene swapper
        switch (buttonHit)
        {
            case BUTTON_HIT.NONE:
                {
                    if (canvGroup.alpha == 0.0f)
                    {
                        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 1.0f));
                    }

                    if (canvGroup.alpha == 1.0f && title.GetComponent<Text>().canvasRenderer.GetAlpha() == 0.0f)
                        title.GetComponent<Text>().CrossFadeAlpha(1.0f, 3.0f, false);

                    if (title.GetComponent<Text>().canvasRenderer.GetAlpha() == 1.0f)
                    {
                        if (anyKey.GetComponent<Text>().canvasRenderer.GetAlpha() == 0.0f)
                            anyKey.GetComponent<Text>().CrossFadeAlpha(1.0f, 1.0f, false);
                        if (anyKey.GetComponent<Text>().canvasRenderer.GetAlpha() == 1.0f)
                            anyKey.GetComponent<Text>().CrossFadeAlpha(0.0f, 1.0f, false);

                        if (Input.anyKeyDown)
                        {
                            AudioManager.instance.Play("ContinueSound");
                            Continue();
                        }
                    }
                }
                break;
            case BUTTON_HIT.CONTINUE:
                {
                    if (title.GetComponent<Text>().canvasRenderer.GetAlpha() == 0.0f && anyKey.GetComponent<Text>().canvasRenderer.GetAlpha() == 0.0f)
                    {
                        title.SetActive(false);
                        anyKey.SetActive(false);
                        nameWindow.SetActive(true);
                    }
                }
                break;
            case BUTTON_HIT.ACCEPT:
                {
                    // If intro screen faded, start main menu screen
                    if (canvGroup.alpha == 0.0f)
                    {
                        mainMenuScene.SetActive(true);
                        introScene.SetActive(false);
                    }
                }
                break;
            case BUTTON_HIT.BACK:
                break;
        }
    }


    public void Continue()
    {
        title.GetComponent<Text>().CrossFadeAlpha(0.0f, 1.0f, false);
        anyKey.GetComponent<Text>().CrossFadeAlpha(0.0f, 1.0f, false);
        buttonHit = BUTTON_HIT.CONTINUE;
    }

    public void NameAccept()
    {
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 0.0f));
        buttonHit = BUTTON_HIT.ACCEPT;
    }

    public void NameBack()
    {
        nameWindow.SetActive(false);
        title.SetActive(true);
        anyKey.SetActive(true);
        buttonHit = BUTTON_HIT.NONE;
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
