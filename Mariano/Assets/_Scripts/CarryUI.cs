using UnityEngine;
using TMPro;

public class CarryUI : MonoBehaviour
{
    public PlayerGrab playerGrab;
    public TextMeshProUGUI carryInfoText;
    public TextMeshProUGUI targetInfoText;
    public PlayerJump playerJump;
    public PlayerTargeting targeting;
    string targetName = "None";

    void Update()
    {
        if (targeting.CurrentTarget != null)
        {
            targetName = targeting.CurrentTarget.name;
        }
        carryInfoText.text = "Carrying: " + playerGrab.carriedItemName + "\nWeight: " + playerGrab.carriedWeight + "\nJump Force: " + playerJump.CurrentJumpForce.ToString("F1");

        targetInfoText.text = "Target: " + targetName;
    }
}
