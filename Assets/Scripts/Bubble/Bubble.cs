using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public Image timeIcon;         // Icon for the time clue
    public Image seenAtIcon;       // Icon for the "seen at" clue
    public Image nextLocationIcon; // Icon for the "next location" clue

public void SetIcons(Sprite time, Sprite seenAt, Sprite nextLocation)
{
    if (timeIcon != null) 
    {
        Debug.Log($"Setting Time Icon: {time?.name ?? "None"}");
        timeIcon.sprite = time;
    }
    if (seenAtIcon != null) 
    {
        Debug.Log($"Setting SeenAt Icon: {seenAt?.name ?? "None"}");
        seenAtIcon.sprite = seenAt;
    }
    if (nextLocationIcon != null) 
    {
        Debug.Log($"Setting NextLocation Icon: {nextLocation?.name ?? "None"}");
        nextLocationIcon.sprite = nextLocation;
    }
}

    /// <summary>
    /// Sets the icons in the bubble based on the provided clue.
    /// </summary>
    public void SetClue(Clue clue)
    {
        if (clue == null)
        {
            Debug.LogError("Clue is null. Cannot set bubble details.");
            return;
        }

        // Set time icon
        Sprite timeSprite = ClueIconManager.GetIconForTime(clue.Time);
        if (timeIcon != null)
        {
            timeIcon.sprite = timeSprite;
            timeIcon.gameObject.SetActive(timeSprite != null);
        }

        // Set seenAt icon
        Sprite seenAtSprite = ClueIconManager.GetIconForLocation(clue.SeenAt);
        if (seenAtIcon != null)
        {
            seenAtIcon.sprite = seenAtSprite;
            seenAtIcon.gameObject.SetActive(seenAtSprite != null);
        }

        // Set nextLocation icon
        Sprite nextLocationSprite = ClueIconManager.GetIconForLocation(clue.NextLocation);
        if (nextLocationIcon != null)
        {
            nextLocationIcon.sprite = nextLocationSprite;
            nextLocationIcon.gameObject.SetActive(nextLocationSprite != null);
        }
    }


    /// <summary>
    /// Sets the position of the bubble above the villager.
    /// </summary>
    public void SetPosition(Vector3 position)
    {
        transform.position = position + Vector3.up * 1.5f; // Offset for better visibility
    }
}
