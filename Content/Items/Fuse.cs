using BombOPlenty.Content.Projectiles;

namespace BombOPlenty.Content.Items;

public class Fuse : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.shootSpeed = 8f;
        Item.shoot = ModContent.ProjectileType<FuseProjectile>();
        Item.consumable = true;
        Item.material = true;
        Item.maxStack = Item.CommonMaxStack;
        Item.width = 8;
        Item.height = 14;
        Item.useAnimation = 1 * UnitHelpers.SecondsToTicks;
        Item.useTime = 1 * UnitHelpers.SecondsToTicks;
        Item.autoReuse = false;
    }

    public override void AddRecipes()
    {
        const int outputAmount = 5;
        var recipe = CreateRecipe(outputAmount);
        recipe.AddIngredient(ItemID.Rope);
        recipe.AddIngredient(ItemID.ExplosivePowder);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();

        recipe = CreateRecipe(outputAmount);
        recipe.AddIngredient(ItemID.VineRope);
        recipe.AddIngredient(ItemID.ExplosivePowder);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();

        recipe = CreateRecipe(outputAmount);
        recipe.AddIngredient(ItemID.WebRope);
        recipe.AddIngredient(ItemID.ExplosivePowder);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}