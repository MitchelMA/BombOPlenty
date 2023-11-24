using BombOPlenty.Content.Projectiles;
using BombOPlenty.Trackers;
using Terraria.Audio;
using Terraria.DataStructures;

namespace BombOPlenty.Content.Items;

public class CFour : ModItem
{
    private const int MaxCount = 4;
    private readonly SoundStyle _sound = SoundID.Item1;
    
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Shoot;
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

    public override bool AltFunctionUse(Player player)
    {
        CFourTracker.Instance.KillAll(Main.myPlayer);
        return base.AltFunctionUse(player);
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type,
        int damage, float knockback)
    {
        if (CFourTracker.Instance.TrackedCount(Main.myPlayer) >= MaxCount)
            return false;

        SoundEngine.PlaySound(_sound);
        return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }
}