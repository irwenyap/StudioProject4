using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkInit : MonoBehaviour
{
    public GameObject serverObj;
    public GameObject clientObj;
    public Client_Demo currentObject;
    public GameObject clientChat;

    public void InitServer(int _port) {
        GameObject client = Instantiate(serverObj);
        client.GetComponent<Server_Demo>().Init(_port);
    }
    public void InitClient(string _ip, int _port, string _name) {
        GameObject client = Instantiate(clientObj);
        currentObject = client.GetComponent<Client_Demo>();
        currentObject.Init(_ip, _port, _name);
        clientChat.GetComponent<SendMessageButton>().clientDemo = client;
    }
}
