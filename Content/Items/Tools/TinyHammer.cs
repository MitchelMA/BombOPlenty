namespace BombOPlenty.Content.Items.Tools;

public class TinyHammer : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.width = 22;
        Item.height = 22;
        Item.maxStack = 1;
        Item.hammer = 15;
        Item.damage = 1;
        Item.knockBack = 6;
        Item.tileBoost = -2;
        Item.scale = 1.2f;
        
        Item.autoReuse = true;
        Item.material = true;
        Item.useTurn = true;
        
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