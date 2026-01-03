using UnityEngine;
using System.Collections.Generic;

public class RelicManager : MonoBehaviour
{
    public List<RelicInstance> ownedRelics = new List<RelicInstance>();

    // For testing/reference
    public List<RelicSO> startingRelics;

    private void Start()
    {
        foreach(var relicSO in startingRelics)
        {
            AddRelic(relicSO);
        }
    }

    public void AddRelic(RelicSO relicData)
    {
        if (relicData == null) return;
        
        RelicInstance newInstance = new RelicInstance(relicData);
        ownedRelics.Add(newInstance);
        
        Debug.Log($"Relic Added: {relicData.relicName}");
        
        // Handle "Upon pickup" effects immediately if any (optional, depends on design)
        // If we had an OnObtain relic trigger, we'd fire an event here.
    }

    public void RemoveRelic(RelicInstance relic)
    {
        if (ownedRelics.Contains(relic))
        {
            relic.OnRemove();
            ownedRelics.Remove(relic);
        }
    }
}
