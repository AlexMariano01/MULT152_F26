using UnityEngine; public class Pickup : MonoBehaviour 

{ 
    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) 
        {
           Debug.Log("Drop Collected!");
           Debug.Log("Keep it up!");
           Destroy(gameObject);
        }
    }
}