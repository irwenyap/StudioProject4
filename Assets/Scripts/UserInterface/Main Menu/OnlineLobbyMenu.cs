using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineLobbyMenu : MonoBehaviour
{
    enum BUTTON_HIT
    {
        NONE,
        CREATE,
        BACK,
        ACCEPT,
        CANCEL
    };

    public GameObject onlineLobbiesTitle;
    public GameObject scrollView;
    public GameObject createButton;
    public GameObject backButton;

    public GameObject onlineLobbyScene;
    public GameObject mainMenuScene;
    public GameObject gameLobbyScene;

    public GameObject createWindow;

    public CanvasGroup canvGroup;
    private float duration = 1.0f;

    private BUTTON_HIT buttonHit = BUTTON_HIT.NONE;

    private void Awake()
    {
        canvGroup.alpha = 0.0f;
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
                }
                break;
            case BUTTON_HIT.CREATE:
                break;
            case BUTTON_HIT.BACK:
                {
                    if (canvGroup.alpha == 0.0f)
                    {
                        onlineLobbyScene.SetActive(false);
                        mainMenuScene.SetActive(true);
                        buttonHit = BUTTON_HIT.NONE;
                    }
                }
                break;
            case BUTTON_HIT.ACCEPT:
                {
                    if (canvGroup.alpha == 0.0f)
                    {
                        onlineLobbyScene.SetActive(false);
                        gameLobbyScene.SetActive(true);
                        buttonHit = BUTTON_HIT.NONE;
                    }
                }
                break;
            case BUTTON_HIT.CANCEL:
                break;
        }
    }

    public void CreateButton()
    {
        createWindow.SetActive(true);
        onlineLobbiesTitle.SetActive(false);
        scrollView.SetActive(false);
        createButton.SetActive(false);
        backButton.SetActive(false);
        buttonHit = BUTTON_HIT.CREATE;
    }

    public void BackButton()
    {
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 0.0f));
        buttonHit = BUTTON_HIT.BACK;
    }

    public void AcceptButton()
    {
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 0.0f));
        buttonHit = BUTTON_HIT.ACCEPT;
    }

    public void CancelButton()
    {
        onlineLobbiesTitle.SetActive(true);
        scrollView.SetActive(true);
        createButton.SetActive(true);
        backButton.SetActive(true);
        createWindow.SetActive(false);
        buttonHit = BUTTON_HIT.CANCEL;
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