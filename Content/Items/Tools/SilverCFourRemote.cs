namespace BombOPlenty.Content.Items.Tools;

public class SilverCFourRemote : TungstenCFourRemote
{
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.GrayPressurePlate)
            .AddIngredient(ItemID.SilverWatch)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}