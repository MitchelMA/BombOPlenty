namespace BombOPlenty.Content.Items.Explosives;

public class CFour : ModItem
{
    public override void SetDefaults()
    {
        Item.ammo = ModContent.ItemType<CFour>();
        Item.consumable = true;
        Item.maxStack = Item.CommonMaxStack;
        Item.width = 18;
        Item.height = 18;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Wire);
        recipe.AddIngredient(ItemID.StickyBomb);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}