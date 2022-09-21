using UnityEngine;

[CreateAssetMenu(fileName = "NewItemAbility", menuName = "ScriptableObject/Shop Item/Ability")]
public class ShopAbility : ShopItem
{
    private Upgradeable Ability => (Upgradeable)Item;

    public override string Label
    { 
        get
        {
            string subtitle = Ability.Level == 0 ? "" : $"Level {Ability.Level}";
            return $"{ItemName}\n<size=80%>{subtitle}</size>";
        }
    }

    protected override int GetPrice(Price price)
    {
        int priceDistance = price.endingPrice - price.startingPrice;
        return (int)(price.startingPrice + priceDistance * Ability.UnlockProgression);
    }
}
