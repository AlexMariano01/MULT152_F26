using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerTargeting : MonoBehaviour
{
    public InputActionReference cycleTargetAction;

    public bool IsLockedOn { get; private set; }

    private List<EnemyTarget> enemies = new List<EnemyTarget>();

    private int currentIndex = -1;

    private EnemyTarget previousTarget;

    Color gray73 = new Color(0.7294f, 0.7294f, 0.7294f, 1f);

    public EnemyTarget CurrentTarget
    {
        get
        {
            if (currentIndex < 0 || currentIndex >= enemies.Count)
            {
                return null;
            }

            return enemies[currentIndex];
        }
    }

    public EnemyTarget LockedTarget
    {
        get
        {
            if (currentIndex < 0 || currentIndex >= enemies.Count)
            {
                return null;
            }

            return enemies[currentIndex];
        }
    }

    public void ToggleLock()
    {
        if (CurrentTarget == null)
        {
            return;
        }

        IsLockedOn = !IsLockedOn;

        if (IsLockedOn)
        {
           Debug.Log("Locked: " + LockedTarget.name); 
        }
        else
        {
            Debug.Log("Lock Released");
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

        //Rest Previous Color
        if (previousTarget != null)
        {
            previousTarget.SetTargeted(false);
        }

        currentIndex++;

        if (currentIndex >= enemies.Count)
        {
            ClearTarget();
            return;
        }

        previousTarget = CurrentTarget;
        Debug.Log("Target Selected: " + CurrentTarget.name);
        CurrentTarget.SetTargeted(true);
    }

    public void ClearTarget()
    {
        if (previousTarget != null)
        {
            previousTarget.SetTargeted(false);
        }

        currentIndex = -1;
        previousTarget = null;
        IsLockedOn = false;

        Debug.Log("Target Cleared");
    }
}