namespace BombOPlenty.Content.Items.Tools;

public class TungstenCFourRemote : CFourRemote
{
    protected override int MaxCFourCount => 5;
    protected override float ShootSpeed => 4.5f;
    
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.GrayPressurePlate)
            .AddIngredient(ItemID.TungstenWatch)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}