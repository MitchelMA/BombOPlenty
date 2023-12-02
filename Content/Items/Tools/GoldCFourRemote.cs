using System;

namespace BombOPlenty.Content.Items.Tools;

public class GoldCFourRemote : CFourRemote
{
    protected override int MaxCFourCount => int.MaxValue;
    protected override float ShootSpeed => 6f;
    
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.GrayPressurePlate)
            .AddIngredient(ItemID.GoldWatch)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}