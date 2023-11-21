using BombOPlenty.Helpers;
using Terraria.ID;
using Terraria.ModLoader;

namespace BombOPlenty.Items;

public class WallBomb : ModItem
{
    public override void SetStaticDefaults()
    {
    }

    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.shootSpeed = 6f;
        Item.shoot = ModContent.ProjectileType<Projectiles.WallBombProjectile>();
        Item.consumable = true;
        Item.maxStack = 9999;
        Item.width = 40;
        Item.height = 40;
        Item.useAnimation = 1 * UnitHelpers.SecondsToTicks;
        Item.useTime = 1 * UnitHelpers.SecondsToTicks;
        Item.autoReuse = false;
    }

    public override void AddRecipes()
    {
        const int outputAmount = 25;
        var recipe = CreateRecipe(outputAmount);
        recipe.AddIngredient(ItemID.WoodenHammer);
        recipe.AddIngredient(ItemID.Bomb, outputAmount);
        recipe.AddTile(TileID.TNTBarrel);
        recipe.Register();
    }
}