namespace BombOPlenty.Content.Items.Tools;

public class CopperCFourRemote : CFourRemote
{
    protected override int MaxCFourCount => 2;
    protected override float ShootSpeed => 3f;

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GrayPressurePlate);
        recipe.AddIngredient(ItemID.CopperWatch);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}