using Terraria.Audio;
using Terraria.DataStructures;

namespace BombOPlenty.Content.Projectiles;

public class FuseProjectile : BombProjectile
{
    public override void SetDefaults()
    {
        NormalSize = new Point(8, 14);
        ExplodingSize = new Point(120, 120);
        Damage = 25;
        KnockBack = 3f;
        
        Projectile.CloneDefaults(ProjectileID.Bomb);
        Projectile.scale = 1.3f;
        FuseRelPosition = new Vector2(0, -NormalSize.Y) * Projectile.scale;
        Projectile.width = NormalSize.X;
        Projectile.height = NormalSize.Y;

        Projectile.timeLeft = 4 * UnitHelpers.SecondsToTicks;
    }
    protected override void ParticleOnKill()
    {
        for (var i = 0; i < 80; i++)
        {
            var smokeDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke,
                0, 0, 100);
            smokeDust.velocity *= 3f;
            smokeDust.noGravity = true;
        }
        
        var fireLoc = Projectile.Center - new Vector2(5);
        for (var i = 0; i < 30; i++)
        {
            var fireDust = Dust.NewDustDirect(fireLoc, 5, 5, DustID.Torch, 0, 0, 100);
            fireDust.velocity *= 2f;
        }
    }

    protected override void FuseParticleAi()
    {
    }

    protected override void OnVerticalCollision(Vector2 oldVelocity, bool wasLeft)
    {
        Projectile.velocity.X = -oldVelocity.X * 0.35f;
    }

    protected override void OnHorizontalCollision(Vector2 oldVelocity, bool wasCeiling)
    {
        Projectile.velocity.Y = -oldVelocity.Y * 0.23f;
        Projectile.velocity.X *= 0.65f;
    }

    protected override void ExplosionEffect()
    {
        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
    }
}