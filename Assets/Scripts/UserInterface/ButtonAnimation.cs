using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    public void OnHover()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);
    }

    public void OffHover()
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void OnClick()
    {
        GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
    }

    public void OffClick()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
    }

    public void StartOnClick()
    {
        GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.75f);
    }

    public void StartOffClick()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 0.75f);
    }
}
