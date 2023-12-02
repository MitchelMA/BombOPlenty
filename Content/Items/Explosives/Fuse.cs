using BombOPlenty.Content.Projectiles.Explosives;

namespace BombOPlenty.Content.Items.Explosives;

public class Fuse : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item1;
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
        CreateRecipe(outputAmount)
            .AddIngredient(ItemID.Rope)
            .AddIngredient(ItemID.ExplosivePowder)
            .AddTile(TileID.WorkBenches)
            .Register();

        CreateRecipe(outputAmount)
            .AddIngredient(ItemID.VineRope)
            .AddIngredient(ItemID.ExplosivePowder)
            .AddTile(TileID.WorkBenches)
            .Register();

        CreateRecipe(outputAmount)
            .AddIngredient(ItemID.WebRope)
            .AddIngredient(ItemID.ExplosivePowder)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}