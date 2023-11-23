namespace BombOPlenty.Content.Items;

public class HammerBomb : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.shootSpeed = 6f;
        Item.shoot = ModContent.ProjectileType<Projectiles.HammerBombProjectile>();
        Item.consumable = true;
        Item.maxStack = Item.CommonMaxStack;
        Item.width = 22;
        Item.height = 30;
        Item.useAnimation = 1 * UnitHelpers.SecondsToTicks;
        Item.useTime = 1 * UnitHelpers.SecondsToTicks;
        Item.autoReuse = false;
    }

    public override void AddRecipes()
    {
        const int outputAmount = 50;
        var recipe = CreateRecipe(outputAmount);
        recipe.AddIngredient(ItemID.WoodenHammer);
        recipe.AddIngredient(ItemID.Bomb, outputAmount);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}