using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendMessageButton : MonoBehaviour
{
    private Button myButton;

    public GameObject clientDemo;
    // Chat
    public GameObject chatText;
    public GameObject displayChat;

    private void Start() {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(SendMessageToServer);
    }

    void SendMessageToServer() {
        clientDemo.GetComponent<Client_Demo>().SetChatButton(gameObject);
        Debug.Log("SENDING MESSAGE TO SERVER");
        clientDemo.GetComponent<Client_Demo>().SendClientMessage(chatText.GetComponent<Text>().text);
        //displayChat.GetComponent<Text>().text += chatText.GetComponent<Text>().text + "\n";
        chatText.GetComponent<Text>().text.Remove(0);
    }
}
