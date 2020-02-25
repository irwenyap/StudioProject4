using Photon.Pun;
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private PlayerList playerList;

    private void Awake() {
        //MasterManager.NetworkInstantiate(_prefab, transform.position, Quaternion.identity);
        GameObject player = PhotonNetwork.Instantiate("Prefabs/Player", transform.position, Quaternion.identity);
        playerList.playerList.Add(player);
        camera.GetComponent<CameraController>().SetTarget(player.transform);

        //PhotonNetwork.InstantiateSceneObject("Prefabs/Pistol", new Vector3(0, 10, 0), Quaternion.identity);
    }
}
