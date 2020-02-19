using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    private Button myButton;
    // Start is called before the first frame update
    void Start() {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(EnterGame);
    }

    private void EnterGame() {
        PhotonNetwork.LoadLevel(1);
    }
}
