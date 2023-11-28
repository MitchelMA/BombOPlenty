using System;
using System.IO;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Achievements;

namespace BombOPlenty.Content.Projectiles.Explosives;

public class CFourProjectile : BombProjectile
{
    private const int CollisionIndex = 1;
    private const int RotationIndex = 2;

    public bool HasCollided => ((int)Projectile.ai[CollisionIndex] & 1) > 0;
    public bool IsColliding => ((int) Projectile.ai[CollisionIndex] & 2) > 0;

    public override void SetDefaults()
    {
        NormalSize = new Point(18, 18);
        ExplodingSize = new Point(140, 140);
        Damage = 110;
        KnockBack = 8f;
        Radius = 5f;
        
        Projectile.width = NormalSize.X;
        Projectile.height = NormalSize.Y;
        Projectile.timeLeft = 5 * UnitHelpers.MinutesToTicks;

        Projectile.ai[RotationIndex] = 0;
        Projectile.ai[CollisionIndex] = 0;
    }

    public override void OnSpawn(IEntitySource source)
    {
        CFourTracker.Instance.Register(Projectile.owner, this);
    }

    protected override void PositionalAi()
    {
        Projectile.ai[0]++;
        if (Projectile.ai[0] > 5)
        {
            Projectile.ai[0] = 10f;
            Projectile.velocity.X *= 0.99f;
            Projectile.velocity.Y += 0.1f;
        }

        Projectile.rotation += Projectile.velocity.X * 0.12f;
        
        if (!IsColliding) return;

        Projectile.velocity = Vector2.Zero;
        Projectile.rotation = Projectile.ai[RotationIndex];

        var below = Projectile.HasTileBelow();
        
        if (!below)
            UnsetIsColliding();
    }

    protected override void FuseParticleAi()
    {
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
        ExplosionHelpers.KillTiles(Projectile.Center, Radius);
        ExplosionHelpers.KillWalls(Projectile.Center, Radius + 1f, true);
        AchievementsHelper.CurrentlyMining = false;
    }

    protected override void OnKillExtra(int timeLeft)
    {
        CFourTracker.Instance.Unregister(Projectile.owner, this);
    }

    protected override void OnHorizontalCollision(Vector2 oldVelocity, bool wasCeiling)
    {
        if (IsColliding) return;
        
        SetHasCollided();
        SetIsColliding();
        Projectile.velocity = Vector2.Zero;
        Projectile.ai[RotationIndex] = MathF.PI * Convert.ToInt32(wasCeiling);
    }
    
    protected override void OnVerticalCollision(Vector2 oldVelocity, bool wasLeft)
    {
        if (IsColliding) return;

        SetHasCollided();
        SetIsColliding();
        Projectile.velocity = Vector2.Zero;
        Projectile.ai[RotationIndex] = (MathHelper.TwoPi - MathHelper.PiOver2) - MathF.PI * Convert.ToInt32(wasLeft);
    }
    
    private void SetHasCollided()
    {
        Projectile.ai[CollisionIndex] = (int) Projectile.ai[CollisionIndex] | 1;
    }

    private void UnsetHasCollided()
    {
        Projectile.ai[CollisionIndex] = (int) Projectile.ai[CollisionIndex] & ~1;
    }

    private void SetIsColliding()
    {
        Projectile.ai[CollisionIndex] = (int) Projectile.ai[CollisionIndex] | 1 << 1;
    }

    private void UnsetIsColliding()
    {
        Projectile.ai[CollisionIndex] = (int) Projectile.ai[CollisionIndex] & ~ (1 << 1);
    }
}