namespace BombOPlenty.Content.Items.Tools;

public class SilverCFourRemote : TungstenCFourRemote
{
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GrayPressurePlate);
        recipe.AddIngredient(ItemID.SilverWatch);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}