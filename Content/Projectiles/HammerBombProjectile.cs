using Terraria.Audio;
using Terraria.GameContent.Achievements;

namespace BombOPlenty.Content.Projectiles;

public class HammerBombProjectile : BombProjectile
{
    public override void SetDefaults()
    {
        NormalSize = new Point(22, 30);
        ExplodingSize = new Point(165, 165);
        FuseRelPosition = new Vector2(0, -Projectile.height / 2f - 2f);
        Damage = 85;
        KnockBack = 10f;
        Radius = 6f;
        
        Projectile.aiStyle = ProjectileID.Bomb;
        DrawOriginOffsetY = 5;
        Projectile.width = NormalSize.X;
        Projectile.height = NormalSize.Y;
        Projectile.scale = 1.1f;
        Projectile.friendly = true;
        Projectile.hostile = true;
        Projectile.penetrate = -1;

        Projectile.timeLeft = 4 * UnitHelpers.SecondsToTicks;
    }

    protected override void PositionalAi()
    {
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] > 5f)
        {
            Projectile.ai[0] = 10f;
            Projectile.velocity.X *= 0.99f;
            Projectile.velocity.Y += 1 - 0.89f;
        }

        Projectile.rotation += Projectile.velocity.X * 0.1f;
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
        
        var fireLoc = Projectile.Center - new Vector2(1);
        for (var i = 0; i < 70; i++)
        {
            var fireDust = Dust.NewDustDirect(fireLoc, 1, 1, DustID.Torch, 0, 0, 100);
            fireDust.velocity *= 3f;
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