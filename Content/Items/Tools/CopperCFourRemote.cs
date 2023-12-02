namespace BombOPlenty.Content.Items.Tools;

public class CopperCFourRemote : CFourRemote
{
    protected override int MaxCFourCount => 2;
    protected override float ShootSpeed => 3f;

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.GrayPressurePlate)
            .AddIngredient(ItemID.CopperWatch)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}