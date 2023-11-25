namespace BombOPlenty.Content.Items.Tools;

public class GoldCFourRemote : CFourRemote
{
    protected override int MaxCFourCount => 8;
    protected override float ShootSpeed => 6f;
    
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GrayPressurePlate);
        recipe.AddIngredient(ItemID.GoldWatch);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}