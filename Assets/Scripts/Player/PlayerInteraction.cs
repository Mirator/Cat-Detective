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
                    villager.Interact(); // Handle villager interaction
                    ShowBubble(villager);
                }
            }
            else if (hit.CompareTag("MasterVillager"))
            {
                PuzzleManager puzzleManager = FindObjectOfType<PuzzleManager>();
                if (puzzleManager != null)
                {
                    puzzleManager.InteractWithMasterVillager(); // Start location selection
                }
            }
        }
    }

    void ShowBubble(Villager villager)
    {
        if (currentBubble != null)
        {
            Destroy(currentBubble);
        }

        currentBubble = Instantiate(villager.bubblePrefab); // Bubble prefab defined in Villager
        Bubble bubbleScript = currentBubble.GetComponent<Bubble>();

        if (bubbleScript != null)
        {
            bubbleScript.SetClue(villager.GetClue());
            bubbleScript.SetPosition(villager.transform.position);
        }
    }

    void OnDrawGizmos()
    {
        // Visualize the interaction radius in the Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
