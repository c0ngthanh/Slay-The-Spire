using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour, IPoolable
{
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Slider hpSlider;
    private int unitUID = -1;

    void Awake()
    {
        EventBusSystem.Subscribe<HPChangedEvent>(OnHPChanged);
    }

    private void OnHPChanged(HPChangedEvent @event)
    {
        if (@event.UnitID == unitUID && gameObject.activeInHierarchy)
        {
            UpdateHPBar(@event.CurrentHP, @event.MaxHP);
        }
    }

    public void UpdateHPBar(int currentHP, int maxHP)
    {
        hpSlider.value = (float)currentHP / maxHP;
        hpText.text = $"{currentHP} / {maxHP}";
    }

    public void SetUnitUID(int uid)
    {
        unitUID = uid;
    }

    public void OnSpawn()
    {
        // Reset HP bar fill to 100%
        // Start fade-in animation
        // gameObject.SetActive(true);
    }

    public void OnDespawn()
    {
        // Stop any running tweens/animations
        // Clear references
        // gameObject.SetActive(false);
    }
}
