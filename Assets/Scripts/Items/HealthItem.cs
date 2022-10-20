using UnityEngine;

public class HealthItem : BaseItem
{
    [SerializeField]
    int healAmount = 10;

    public override void OnPickup(Player player)
    {
        player.Heal(healAmount);
    }
}
