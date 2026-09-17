using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image stateImage;

    [SerializeField] private Slider TBAtimerSlider;

    public void SetHealth(float current)
    {
        healthSlider.value = current;
    }

    public void setTBATimer(float current)
    {       
        TBAtimerSlider.value = current;
    }

    public void SetIcon(UIManager.StateIcon icon)
    {
        Sprite newSprite = UIManager.Instance.GetIcon(icon);

        if (newSprite != null)
        {
            stateImage.sprite = newSprite;
        }
    }
}
