using UnityEngine;
using UnityEngine.UI;

public class ExpIndicator : MonoBehaviour
{
    [SerializeField]
    protected Text expText;

    public void UpdateExp (int exp) {
        expText.text = "Experience: " + exp;
    }
}
