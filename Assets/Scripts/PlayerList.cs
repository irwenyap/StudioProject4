using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour {
    public GameObject[] playerList;


    private void Start() {
        StartCoroutine("WaitFor2Seconds");
    }

    IEnumerator WaitFor2Seconds() {
        yield return new WaitForSeconds(2);
        StartCoroutine("AddToList");
    }

    IEnumerator AddToList() {
        playerList = GameObject.FindGameObjectsWithTag("Player");
        yield return null;
    }
}
