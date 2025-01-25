using UnityEngine;
using UnityEngine.UI;

public class ClueItem : MonoBehaviour
{
    public Image clueIcon;
    public Text clueText;

    public void SetClue(Sprite icon, string text)
    {
        clueIcon.sprite = icon;
        clueText.text = text;
    }
}
