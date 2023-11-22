namespace BombOPlenty.Items;

public class TileBomb : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.shootSpeed = 6f;
        Item.shoot = ModContent.ProjectileType<Projectiles.TileBombProjectile>();
        Item.consumable = true;
        Item.maxStack = 9999;
        Item.width = 40;
        Item.height = 40;
        Item.useAnimation = 1 * UnitHelpers.SecondsToTicks;
        Item.useTime = 1 * UnitHelpers.SecondsToTicks;
        Item.autoReuse = false;
    }
}