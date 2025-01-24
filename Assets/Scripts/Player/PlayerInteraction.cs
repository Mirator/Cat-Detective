using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject bubblePrefab; // Assign the Bubble prefab
    public float interactionRadius = 1.5f; // Interaction range
    private GameObject currentBubble;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRadius);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Villager"))
                {
                    Villager villager = hit.GetComponent<Villager>();
                    if (villager != null)
                    {
                        ShowBubble(villager);
                    }
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

        currentBubble = Instantiate(bubblePrefab);
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
