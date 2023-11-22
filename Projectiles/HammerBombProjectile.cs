using Terraria.Audio;
using Terraria.GameContent.Achievements;

namespace BombOPlenty.Projectiles;

public class HammerBombProjectile : BombProjectile
{
    public override void SetDefaults()
    {
        NormalSize = new Point(22, 30);
        ExplodingSize = NormalSize * new Point(8, 8);
        FuseRelPosition = new Vector2(0, -Projectile.height / 2f - 6f);
        Damage = 85;
        KnockBack = 10f;
        Radius = 6f;
        
        Projectile.CloneDefaults(ProjectileID.Bomb);
        DrawOriginOffsetY = 5;
        Projectile.width = NormalSize.X;
        Projectile.height = NormalSize.Y;
        Projectile.scale = 1.1f;

        Projectile.timeLeft = 4 * UnitHelpers.SecondsToTicks;
    }

    protected override void ParticleOnKill()
    {
        for (var i = 0; i < 50; i++)
        {
            var smokeDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke,
                0, 0, 100);
            smokeDust.velocity *= 3f;
            smokeDust.noGravity = true;
        }
        
        var fireLoc = Projectile.Center - new Vector2(5);
        for (var i = 0; i < 90; i++)
        {
            var fireDust = Dust.NewDustDirect(fireLoc, 5, 5, DustID.Torch, 0, 0, 100);
            fireDust.velocity *= 5f;
        }
    }

    protected override void ExplosionEffect()
    {
        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        AchievementsHelper.CurrentlyMining = true;
        ExplosionHelpers.KillWalls(Projectile.position, Radius);
        AchievementsHelper.CurrentlyMining = false;
    }

    protected override void OnGroundCollision(Vector2 oldVelocity)
    {
        Projectile.velocity.Y = -oldVelocity.Y * 0.3f;
        Projectile.velocity.X *= 0.8f;
    }

    protected override void OnWallCollision(Vector2 oldVelocity)
    {
        Projectile.velocity.X = -oldVelocity.X * 0.56f;
    }
}