namespace BombOPlenty.Content.Items.Tools;

public class PlatinumCFourRemote : GoldCFourRemote
{
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GrayPressurePlate);
        recipe.AddIngredient(ItemID.PlatinumWatch);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}