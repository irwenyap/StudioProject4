using UnityEngine;
using UnityEngine.UI;

public class PlaySound : MonoBehaviour {
    public AudioClip sound;

    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source { get { return GetComponent<AudioSource>(); } }


    // Start is called before the first frame update
    void Start() {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
        button.onClick.AddListener(ApplySound);
    }

    void ApplySound() {
        source.PlayOneShot(sound);
    }
}