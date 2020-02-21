using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    public void HoverSound()
    {
        AudioManager.instance.Play("ButtonHoverSound");
    }

    public void ClickSound()
    {
        AudioManager.instance.Play("ButtonClickSound");
    }
}
