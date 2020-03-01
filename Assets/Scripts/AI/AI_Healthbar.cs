using UnityEngine;
using UnityEngine.UI;

public class AI_Healthbar : MonoBehaviour {
    [SerializeField]
    private Image fillImage;
    
    private void Start() {
        fillImage = transform.Find("Fill").GetComponent<Image>();
    }

    public void SetHealth(float fill) {
        fillImage.fillAmount = fill;
    }
}
