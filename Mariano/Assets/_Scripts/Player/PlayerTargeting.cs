using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerTargeting : MonoBehaviour
{
    public InputActionReference cycleTargetAction;

    public bool IsLockedOn {get; private set;}

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
        if (enemies.Count == 0)
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
            Renderer oldR = previousTarget.GetComponent<Renderer>();

            if (oldR != null)
            {
                oldR.material.SetColor("_BaseColor", gray73);
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