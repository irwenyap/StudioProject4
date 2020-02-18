using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineLobbyMenu : MonoBehaviour
{
    public CanvasGroup canvGroup;
    private float duration = 1.0f;

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