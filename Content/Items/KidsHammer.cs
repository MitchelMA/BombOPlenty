namespace BombOPlenty.Content.Items;

public class KidsHammer : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemUseStyleID.Swing);
        Item.width = 22;
        Item.height = 30;
        Item.maxStack = 1;
        Item.hammer = 15;
        Item.damage = 1;
        
        Item.useTime = 25;
        Item.useAnimation = 25;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Wood, 2);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}