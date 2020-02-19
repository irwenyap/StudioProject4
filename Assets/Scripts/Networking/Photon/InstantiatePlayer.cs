﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;


    private void Awake() {
        //MasterManager.NetworkInstantiate(_prefab, transform.position, Quaternion.identity);
        PhotonNetwork.Instantiate("Prefabs/Player 1", transform.position, Quaternion.identity);
    }
}
