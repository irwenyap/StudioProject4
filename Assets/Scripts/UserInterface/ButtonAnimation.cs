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
        GetComponent<Image>().color = new Color(255, 255, 255);
    }

    public void OffClick()
    {
        GetComponent<Image>().color = new Color(0, 0, 0);
    }
}
