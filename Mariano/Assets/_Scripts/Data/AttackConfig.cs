using UnityEngine; 

[CreateAssetMenu(menuName = "Configs/Attack Config")] 
public class AttackConfig : ScriptableObject 
{ 
    public int damage = 10; 
    public float range = 1.5f; 
    public float cooldown = 1f; 
}