using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRadius = 1.5f; // Interaction range
    private Villager currentVillager;
    private GameObject currentBubble;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InteractWithNearbyObjects();
        }
    }

    /// <summary>
    /// Handles interactions with nearby objects.
    /// </summary>
    void InteractWithNearbyObjects()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Villager"))
            {
                Villager villager = hit.GetComponent<Villager>();
                if (villager != null)
                {
                    villager.Interact();
                    ShowBubble(villager);
                }
            }
            else if (hit.CompareTag("MasterVillager"))
            {
                PuzzleManager puzzleManager = FindFirstObjectByType<PuzzleManager>();
                if (puzzleManager != null)
                {
                    puzzleManager.InteractWithMasterVillager();
                }
            }
        }
    }

    /// <summary>
    /// Displays the clue bubble for a villager.
    /// </summary>
    void ShowBubble(Villager villager)
    {
        if (currentBubble != null)
        {
            Destroy(currentBubble);
        }

        if (villager.bubblePrefab != null)
        {
            currentBubble = Instantiate(villager.bubblePrefab);
            Bubble bubbleScript = currentBubble.GetComponent<Bubble>();

            if (bubbleScript != null && villager.assignedClue != null)
            {
                Sprite timeIcon = ClueIconManager.GetIconForTime(villager.assignedClue.Time);
                Sprite seenAtIcon = ClueIconManager.GetIconForLocation(villager.assignedClue.SeenAt);
                Sprite nextLocationIcon = ClueIconManager.GetIconForLocation(villager.assignedClue.NextLocation);

                bubbleScript.SetIcons(timeIcon, seenAtIcon, nextLocationIcon);
                bubbleScript.SetPosition(villager.transform.position);
            }
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
