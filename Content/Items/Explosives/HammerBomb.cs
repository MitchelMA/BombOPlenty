using BombOPlenty.Content.Items.Tools;
using BombOPlenty.Content.Projectiles.Explosives;

namespace BombOPlenty.Content.Items.Explosives;

public class HammerBomb : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item1;
        Item.shootSpeed = 6f;
        Item.shoot = ModContent.ProjectileType<HammerBombProjectile>();
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
        CreateRecipe(5)
            .AddIngredient<TinyHammer>()
            .AddIngredient<Fuse>()
            .AddIngredient(ItemID.ExplosivePowder)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}