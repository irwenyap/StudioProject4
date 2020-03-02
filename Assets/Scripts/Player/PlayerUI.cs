using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    // PlayerController
    private PlayerController myPlayerController;

    // UI
    public PlayerHealthBar healthBar;
    public WeaponBase currentWeapon;
    public Skills m1;
    public Skills m2;
    public Text coins;
    public Text gems;
    public Text shards;

    void Start() {
        myPlayerController = GetComponent<PlayerController>();

        coins.text = myPlayerController.coins.ToString();
        gems.text = myPlayerController.gems.ToString();
        shards.text = myPlayerController.shards.ToString();
    }

    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 12) {
            m1.imageSkill.sprite = collision.gameObject.GetComponent<WeaponBase>().imageM1;
            m2.imageSkill.sprite = collision.gameObject.GetComponent<WeaponBase>().imageM2;
        }
    }
}
