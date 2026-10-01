using UnityEngine;

public class GrabObject : MonoBehaviour
{
    [Header("Item Information")]
    public string itemName = "New Item";
    [Header("Object Weight")]
    public float weight = 10f;
    [Header("Attack Settings")]
    public bool canBeThrown = false;
    [HideInInspector]
    public bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Press E to pick up the object");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player Dropped Object");
        }
    }
}
