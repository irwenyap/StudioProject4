using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLobbyMenu : MonoBehaviour
{
    enum BUTTON_HIT
    {
        NONE,
        START,
        LEAVE,
    };

    public GameObject gameLobbyScene;
    public GameObject onlineLobbyScene;

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
        //Scene swapper
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
            case BUTTON_HIT.START:
                break;
            case BUTTON_HIT.LEAVE:
                if (canvGroup.alpha == 0.0f)
                {
                    gameLobbyScene.SetActive(false);
                    onlineLobbyScene.SetActive(true);
                    buttonHit = BUTTON_HIT.NONE;
                }
                break;
        }
    }

    public void StartButton()
    {

    }

    public void LeaveButton()
    {
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 0.0f));
        buttonHit = BUTTON_HIT.LEAVE;
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
