using UnityEngine;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRadius = 0.25f; // Interaction range
    public GameObject clueTrackerPrefab; // Prefab for a clue tracker item
    public Transform clueTrackerParent; // Parent panel for clue tracker items
    public InstructionManager instructionManager; // Reference to the HelperManager script
    public AudioSource audioSource; // Reference to the AudioSource component

    private Villager currentVillager;
    private GameObject currentBubble;
    private HashSet<Villager> interactedVillagers = new HashSet<Villager>(); // Track villagers already interacted with

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
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance > interactionRadius)
                continue;

            if (hit.CompareTag("Villager"))
            {
                Villager villager = hit.GetComponent<Villager>();
                if (villager != null)
                {
                    Debug.Log($"Interacted with Villager: {villager.name}");
                    villager.Interact();
                    UpdateClueTracker(villager);
                    PlayMeowSound(); // Play meow sound on interaction
                }
            }
            else if (hit.CompareTag("MasterVillager"))
            {
                GamePuzzleManager gamePuzzleManager = Object.FindFirstObjectByType<GamePuzzleManager>();

                if (gamePuzzleManager == null)
                {
                    Debug.LogError("GamePuzzleManager not found in the scene!");
                    return;
                }

                Debug.Log("Interacted with Master Villager");
                // Trigger helper text logic
                instructionManager.InteractWithMasterVillager();
                PlayMeowSound(); // Play meow sound for master villager interaction

                // Show puzzle UI only if it's ready to be shown
                if (instructionManager.ShouldShowPuzzle() && !gamePuzzleManager.puzzleUI.activeSelf)
                {
                    Debug.Log("Displaying puzzle UI.");
                    gamePuzzleManager.Initialize();
                    gamePuzzleManager.InteractWithMasterVillager();
                }
            }
        }
    }

    /// <summary>
    /// Plays the meow sound, ensuring no overlapping sounds.
    /// </summary>
    private void PlayMeowSound()
    {
        if (audioSource != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    /// <summary>
    /// Updates the clue tracker panel for the interacted villager.
    /// </summary>
    void UpdateClueTracker(Villager villager)
    {
        if (interactedVillagers.Contains(villager))
        {
            return;
        }

        interactedVillagers.Add(villager);

        if (clueTrackerPrefab == null || clueTrackerParent == null)
        {
            Debug.LogError("Clue tracker prefab or parent is not assigned.");
            return;
        }

        if (villager.assignedClue == null)
        {
            Debug.LogWarning($"Villager {villager.name} has no clue assigned.");
            return;
        }

        GameObject trackerItem = Instantiate(clueTrackerPrefab, clueTrackerParent);
        ClueTrackerItem trackerScript = trackerItem.GetComponent<ClueTrackerItem>();

        if (trackerScript != null)
        {
            trackerScript.SetClue(villager.assignedClue);
            Debug.Log($"Added clue to tracker: Time: {villager.assignedClue.Time}, Seen At: {villager.assignedClue.SeenAt}, Next Location: {villager.assignedClue.NextLocation}");
        }
        else
        {
            Debug.LogError("ClueTrackerItem script not found on prefab.");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
