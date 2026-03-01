using System;
using UnityEngine;

public class TargetingSystem : SystemBase
{
    private bool IsTargeting = false;

    private LayerMask targetLayerMask;
    
    public override void Initialize()
    {
        EventBusSystem.Subscribe<StartTargetingEvent>(OnWaitForTarget);
    }

    private void OnWaitForTarget(StartTargetingEvent @event)
    {
        IsTargeting = true;
        targetLayerMask = @event.LayerMask;
    }

    public override void Tick()
    {
        if(IsTargeting)
        {
            // Logic for targeting (e.g., highlight selectable targets, handle input)
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if(hit.collider != null)
                {
                    var target = hit.collider.GetComponent<CombatUnit>();
                    if(target != null && targetLayerMask == (1<<target.gameObject.layer))
                    {
                        EventBusSystem.Publish(new TargetSelectedEvent(target));
                        IsTargeting = false;
                    }
                }
            }
        }
    }
    public override void Dispose()
    {
        
    }
}
