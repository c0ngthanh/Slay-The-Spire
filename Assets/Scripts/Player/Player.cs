using UnityEngine;

public class Player : MonoBehaviour
{
    private CombatUnit CombatUnit;

    private void Awake()
    {
        CombatUnit = GetComponent<CombatUnit>();
    }
}
