using System;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Achievements;

namespace BombOPlenty.Content.Projectiles.Explosives;

public class CFourProjectile : BombProjectile
{
    private const int CollidedIndex = 1;
    private const int RotationIndex = 2;

    public bool Collided => Projectile.ai[CollidedIndex] > 0f;
    
    public override void SetDefaults()
    {
        NormalSize = new Point(18, 18);
        ExplodingSize = new Point(140, 140);
        Damage = 110;
        KnockBack = 8f;
        Radius = 5f;
        
        Projectile.CloneDefaults(ProjectileID.Bomb);
        FuseRelPosition = new Vector2(0, 0);
        Projectile.width = NormalSize.X;
        Projectile.height = NormalSize.Y;
        Projectile.timeLeft = 5 * UnitHelpers.MinutesToTicks;

        Projectile.ai[CollidedIndex] = 0;
        Projectile.ai[RotationIndex] = 0;
    }

    public override void OnSpawn(IEntitySource source)
    {
        CFourTracker.Instance.Register(Projectile.owner, this);
    }

    protected override void PositionalAi()
    {
        if (!Collided) return;
        
        Projectile.velocity = Vector2.Zero;
        Projectile.rotation = Projectile.ai[RotationIndex];
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
        ExplosionHelpers.KillTiles(Projectile.position, Radius);
        ExplosionHelpers.KillWalls(Projectile.position, Radius + 1f, true);
        AchievementsHelper.CurrentlyMining = false;
    }

    protected override void OnKillExtra(int timeLeft)
    {
        CFourTracker.Instance.Unregister(Projectile.owner, this);
    }

    protected override void OnHorizontalCollision(Vector2 oldVelocity, bool wasCeiling)
    {
        if (Collided) return;
        
        Projectile.ai[CollidedIndex] = 1f;
        Projectile.ai[RotationIndex] = MathF.PI * Convert.ToInt32(wasCeiling);
    }

    protected override void OnVerticalCollision(Vector2 oldVelocity, bool wasLeft)
    {
        if (Collided) return;
        
        Projectile.ai[CollidedIndex] = 1f;
        Projectile.ai[RotationIndex] = (MathHelper.TwoPi - MathHelper.PiOver2) - MathF.PI * Convert.ToInt32(wasLeft);
    }
}