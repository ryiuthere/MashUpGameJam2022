using UnityEngine;
using UnityEngine.UI;

public class ItemIndicator : MonoBehaviour
{
    [SerializeField]
    private Image itemIndicatorImage;

    /** Updates sprite from GameObject SpriteRenderer **/
    public void UpdateItemIndicator(GameObject item) {
        var itemSprite = item.GetComponent<SpriteRenderer>().sprite;
        itemIndicatorImage.sprite = itemSprite;
        itemIndicatorImage.color = new Color(255, 255, 255, 255);
    }
}
