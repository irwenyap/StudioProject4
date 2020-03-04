using UnityEngine;

public class DifficultySystem : MonoBehaviour {
    [SerializeField]
    static public int difficulty;
    [SerializeField]
    static public int roomEntered;
    [SerializeField]
    static public int maxWeight;

    private void Awake() {
        difficulty = 1;
        roomEntered = 0;
        maxWeight = 4;
    }

    void Update() {
        if (roomEntered >= 10) {
            maxWeight += 20;
            difficulty++;
            roomEntered = 0;
        }
    }
}
