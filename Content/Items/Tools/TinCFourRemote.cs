namespace BombOPlenty.Content.Items.Tools;

public class TinCFourRemote : CopperCFourRemote
{
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.GrayPressurePlate)
            .AddIngredient(ItemID.TinWatch)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}