using Photon.Pun;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Objects;
    bool SpawnerActivate = false;
    int weight;
    void Start()
    {
        weight = 0;
        while (weight < DifficultySystem.maxWeight)
        {
            int rand = Random.Range(0, Objects.Length);
            if (Objects[rand] != null)
            {
                //_ = PhotonNetwork.Instantiate("Prefabs/AIs/Imp", transform.position, Quaternion.identity);
                GameObject instance = PhotonNetwork.InstantiateSceneObject(Objects[rand].GetComponent<AI_Base>().prefabPath, transform.position, Quaternion.identity);
                //This is for Quantity
                weight++;
                //This is for Ai class Weight
                weight += instance.GetComponent<AI_Base>().weight;
            }
        }
        Destroy(gameObject);
    }
}
