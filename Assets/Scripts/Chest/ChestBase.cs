using Photon.Pun;
using UnityEngine;

public class ChestBase : MonoBehaviourPun {

    [SerializeField]
    protected GameObject item;
    [SerializeField]
    protected int cost;

    public string prefabPath;

    [PunRPC]
    protected void RPC_OpenChest() {
        item.SetActive(true);
        Destroy(gameObject);
    }
}
