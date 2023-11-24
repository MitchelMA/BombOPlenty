using BombOPlenty.Content.Projectiles;

namespace BombOPlenty.Content.Items;

public class CFour : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item1;
        Item.shootSpeed = 6f;
        Item.shoot = ModContent.ProjectileType<CFourProjectile>();
        Item.consumable = true;
        Item.maxStack = Item.CommonMaxStack;
        Item.width = 22;
        Item.height = 22;
        Item.useAnimation = 1 * UnitHelpers.SecondsToTicks;
        Item.useTime = 1 * UnitHelpers.SecondsToTicks;
        Item.autoReuse = false;
    }
}