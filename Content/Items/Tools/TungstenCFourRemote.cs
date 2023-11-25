namespace BombOPlenty.Content.Items.Tools;

public class TungstenCFourRemote : CFourRemote
{
    protected override int MaxCFourCount => 5;
    protected override float ShootSpeed => 4.5f;
    
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GrayPressurePlate);
        recipe.AddIngredient(ItemID.TungstenWatch);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}