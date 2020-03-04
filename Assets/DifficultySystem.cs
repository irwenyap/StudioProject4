using UnityEngine;

public class DifficultySystem : MonoBehaviour {
    [SerializeField]
    static public int difficulty;
    [SerializeField]
    static public int roomentered;
    [SerializeField]
    static public int MaxWeight;

    private void Awake() {
        difficulty = 1;
        roomentered = 0;
        MaxWeight = 4;
    }

    void Update() {
        if (roomentered >= 10) {
            MaxWeight += 20;
            difficulty++;
            roomentered = 0;
        }
    }
}
