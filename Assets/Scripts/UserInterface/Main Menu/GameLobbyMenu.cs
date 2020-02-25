using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLobbyMenu : MonoBehaviour
{
    enum BUTTON_HIT
    {
        NONE,
        READY,
        LEAVE,
    };

    public GameObject gameLobbyScene;
    public GameObject onlineLobbyScene;

    public Image[] playerSprites = new Image[4];
    public Image[] playerColours = new Image[4];

    public CanvasGroup canvGroup;
    private float duration = 1.0f;

    private BUTTON_HIT buttonHit = BUTTON_HIT.NONE;

    private void Awake()
    {
        canvGroup.alpha = 0.0f;

        // Create independent material for each sprite
        for (int i = 0; i < 4; ++i)
        {
            Material m = playerSprites[i].material;
            playerSprites[i].material = new Material(m);
        }
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
            case BUTTON_HIT.READY:
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

        //if (Input.GetKeyDown(KeyCode.A))
        //    SetGrayscale(true, playerSprites[0]);
        //else if (Input.GetKeyDown(KeyCode.S))
        //    SetGrayscale(false, playerSprites[0]);
    }

    public void ReadyButton()
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

    public void SetGrayscale(bool isGreyscaled, Image player)
    {
        if (isGreyscaled)
            player.material.SetFloat("_GrayscaleAmount", 1.0f);
        else
            player.material.SetFloat("_GrayscaleAmount", 0.0f);
    }
}
