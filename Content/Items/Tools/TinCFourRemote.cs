namespace BombOPlenty.Content.Items.Tools;

public class TinCFourRemote : CopperCFourRemote
{
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GrayPressurePlate);
        recipe.AddIngredient(ItemID.TinWatch);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}