using Photon.Pun;
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab = null;

    [SerializeField]
    private Camera camera = null;
    [SerializeField]
    private PlayerHealthBar healthBar = null;
    [SerializeField]
    private Skills m1UI = null;
    [SerializeField]
    private Skills m2UI = null;



    private void Awake() {
        //MasterManager.NetworkInstantiate(_prefab, transform.position, Quaternion.identity);
        GameObject player = PhotonNetwork.Instantiate("Prefabs/Player", transform.position, Quaternion.identity);
        PlayerController myPC = player.GetComponent<PlayerController>();
        myPC.healthBar = healthBar;
        myPC.m1 = m1UI;
        myPC.m2 = m2UI;

        camera.GetComponent<CameraController>().SetTarget(player.transform);

        //PhotonNetwork.InstantiateSceneObject("Prefabs/Pistol", new Vector3(0, 10, 0), Quaternion.identity);
    }
}
