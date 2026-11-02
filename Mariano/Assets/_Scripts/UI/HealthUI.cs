using UnityEngine; 
using TMPro; 

public class HealthUI : MonoBehaviour 
{ 
    public Health playerHealth; 
    public TextMeshProUGUI healthText; 
    void Update() 
    { 
        if (playerHealth != null) 
        { healthText.text = "Health: " + playerHealth.currentHealth; } 
    } 
}
