using UnityEngine;
using UnityEngine.UI;

public class ClueTrackerItem : MonoBehaviour
{
    public Image timeIcon;
    public Image seenAtIcon;
    public Image nextLocationIcon;

    /// <summary>
    /// Sets the icons for the tracker item.
    /// </summary>
    public void SetIcons(Sprite time, Sprite seenAt, Sprite nextLocation)
    {
        if (timeIcon != null) timeIcon.sprite = time;
        if (seenAtIcon != null) seenAtIcon.sprite = seenAt;
        if (nextLocationIcon != null) nextLocationIcon.sprite = nextLocation;
    }

    /// <summary>
    /// Configures the clue tracker item based on the provided clue.
    /// </summary>
    /// <param name="clue">The clue to display on this tracker item.</param>
    public void SetClue(Clue clue)
    {
        if (clue == null)
        {
            Debug.LogError("SetClue was called with a null clue.");
            return;
        }

        Debug.Log($"Setting clue: Time={clue.Time}, SeenAt={clue.SeenAt}, NextLocation={clue.NextLocation}");

        Sprite timeSprite = ClueIconManager.GetIconForTime(clue.Time);
        Sprite seenAtSprite = ClueIconManager.GetIconForLocation(clue.SeenAt);
        Sprite nextLocationSprite = ClueIconManager.GetIconForLocation(clue.NextLocation);

        SetIcons(timeSprite, seenAtSprite, nextLocationSprite);
    }
}
