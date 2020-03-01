﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenSync : MonoBehaviourPun {
    [SerializeField]
    private LevelGen myLevelGen = null;

    [SerializeField]
    private int seed = 0;

    void Start() {
        myLevelGen.Initialise(5);

        if (PhotonNetwork.IsMasterClient) {
            seed = Random.Range(1, 1000);
            base.photonView.RPC("RPC_InitLevelGen", RpcTarget.AllBuffered, seed);
        }
    }

    [PunRPC]
    private void RPC_InitLevelGen(int seed) {
        Debug.LogError(seed);
        myLevelGen.Initialise(5);
    }
}
