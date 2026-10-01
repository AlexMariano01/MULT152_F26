using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerTargeting : MonoBehaviour
{
    public InputActionReference cycleTargetAction;
    private List<EnemyTarget> enemies = new List<EnemyTarget>();
    private int currentIndex = -1;
    private EnemyTarget previousTarget;
    
    public EnemyTarget CurrentTarget
    {
        get
        {
            if (currentIndex <0 || currentIndex >= enemies.Count)
            {
                return null;
            }

            return enemies[currentIndex];
        }
    }

    void Start()
    {
        RefreshTargets();

        Debug.Log("Enemies Found: " + enemies.Count);
    }

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            Debug.Log("Tab Pressed");
        }

        if (cycleTargetAction.action.triggered)
        {
            Debug.Log("Cycle Target Action");
            CycleTarget();
        }
    }

    public void RefreshTargets()
    {
        enemies.Clear();

        EnemyTarget[] foundTargets = FindObjectsByType<EnemyTarget>(FindObjectsInactive.Include);

        enemies.AddRange(foundTargets);
    }

    public void CycleTarget()
    {

        if (enemies.Count == 0)
        {
            return;
        }

        //Reset Previous Color
        if (previousTarget != null)
        {
            Renderer oldR = previousTarget.GetComponent<Renderer>();

            if (oldR != null)
            {
                oldR.material.SetColor("_BaseColor", Color.silver);
            }
        }

        currentIndex++;

        if (currentIndex >= enemies.Count)
        {
            currentIndex = 0;
        }

        previousTarget = CurrentTarget;

        Debug.Log("Target Selected: " + CurrentTarget.name);

        Renderer r = CurrentTarget.GetComponent<Renderer>();

        if (r != null)
        {
            r.material.SetColor("_BaseColor", Color.red);
        }
    }
}
