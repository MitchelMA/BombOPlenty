namespace BombOPlenty.Content.Items.Tools;

public class PlatinumCFourRemote : GoldCFourRemote
{
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.GrayPressurePlate)
            .AddIngredient(ItemID.PlatinumWatch)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}