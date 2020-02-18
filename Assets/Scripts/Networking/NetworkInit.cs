using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkInit : MonoBehaviour
{
    public GameObject serverObj;
    public GameObject clientObj;
    public ClientScript currentObject;
    public Camera mainCamera;
    //public GameObject clientChat;

    public void InitServer(int _port) {
        GameObject client = Instantiate(serverObj);
        client.GetComponent<ServerScript>().Init(_port);
    }
    public void InitClient(string _ip, int _port, string _name) {
        GameObject client = Instantiate(clientObj);
        currentObject = client.GetComponent<ClientScript>();
        currentObject.Init(_ip, _port, _name);

        //clientChat.GetComponent<SendMessageButton>().clientDemo = client;
    }
}
