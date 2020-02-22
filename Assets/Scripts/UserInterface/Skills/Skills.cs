using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    public Image imageCooldown;
    public KeyCode key;
    public float cooldown;

    private bool isCooldown;

    private void Start()
    {
        isCooldown = false;
    }
    private void Update()
    {
        if (isCooldown)
        {
            imageCooldown.fillAmount -= 1 / cooldown * Time.deltaTime;
            if (imageCooldown.fillAmount <= 0)
            {
                imageCooldown.fillAmount = 0;
                isCooldown = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(key))
            {
                IsPressed();
            }
        }
    }

    public void IsPressed()
    {
        if (!isCooldown)
        {
            isCooldown = true;
            imageCooldown.fillAmount = 1;
        }
    }
}
