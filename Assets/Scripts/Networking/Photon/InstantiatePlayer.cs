using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class InstantiatePlayer : MonoBehaviourPun {
    [SerializeField]
    private GameObject _prefab = null;

    public Camera camera = null;
    public PlayerHealthBar healthBar = null;
    public Skills m1UI = null;
    public Skills m2UI = null;

    public Text coins;
    public Text gems;
    public Text shards;

    public LevelGen[] dungeons;

    private void Awake() {
        //MasterManager.NetworkInstantiate(_prefab, transform.position, Quaternion.identity);
        GameObject player = PhotonNetwork.Instantiate("Prefabs/Player", transform.position, Quaternion.identity);
        PlayerUI myPlayerUI = player.GetComponent<PlayerUI>();
        myPlayerUI.healthBar = healthBar;
        myPlayerUI.m1 = m1UI;
        myPlayerUI.m2 = m2UI;
        myPlayerUI.coins = coins;
        myPlayerUI.gems = gems;
        myPlayerUI.shards = shards;

        camera.GetComponent<CameraController>().SetTarget(player.transform);

        if (PhotonNetwork.IsMasterClient) {
            int seed = Random.Range(1, 1000);
            Debug.LogError(seed);
            base.photonView.RPC("RPC_InitLevelGen", RpcTarget.AllBuffered, seed);
        }
        //Random.InitState(5);
        //PhotonNetwork.InstantiateSceneObject("Prefabs/Pistol", new Vector3(0, 10, 0), Quaternion.identity);
    }

    [PunRPC]
    private void RPC_InitLevelGen(int seed) {
        for (int i = 0; i < dungeons.Length; ++i) {
            dungeons[i].Initialise(seed);
        }
    }
}
