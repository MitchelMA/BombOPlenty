using BombOPlenty.Content.Items.Explosives;
using BombOPlenty.Content.Projectiles.Explosives;

namespace BombOPlenty.Content.Items.Tools;

public abstract class CFourRemote : ModItem
{
    protected virtual int MaxCFourCount => 0;
    protected virtual float ShootSpeed => 2f;

    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<CFourProjectile>();
        Item.useAmmo = ModContent.ItemType<CFour>();
        Item.noMelee = true;
        Item.shootSpeed = ShootSpeed;
        Item.maxStack = 1;
        Item.width = 8;
        Item.height = 22;
        Item.useAnimation = (int)(0.4f * UnitHelpers.SecondsToTicks);
        Item.useTime = (int)(0.4f * UnitHelpers.SecondsToTicks);
        Item.autoReuse = false;
    }

    public override bool CanShoot(Player player) =>
        CFourTracker.Instance.TrackedCount(player.GetIndex()) < MaxCFourCount;
    
    public override bool AltFunctionUse(Player player)
    {
        CFourTracker.Instance.KillAll(player.GetIndex());
        return base.AltFunctionUse(player);
    }
}