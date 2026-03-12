using System;
using UnityEngine;

public class TargetingSystem : SystemBase
{
    private TargetingModel targetingModel => GameModel.Instance.GetModel<TargetingModel>();
    private LayerMask targetLayerMask;
    
    public override void Initialize()
    {
        EventBusSystem.Subscribe<StartTargetingEvent>(OnWaitForTarget);
    }

    private void OnWaitForTarget(StartTargetingEvent @event)
    {
        targetingModel.StartTargeting(true);
        targetLayerMask = @event.LayerMask;
    }

    public override void Tick()
    {
        if(targetingModel.IsTargeting)
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
                        targetingModel.StartTargeting(false);
                    }
                }
            }
        }
    }
    public override void Dispose()
    {
        
    }
}
