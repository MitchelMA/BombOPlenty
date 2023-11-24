using System;

namespace BombOPlenty.Content.Projectiles;

public class CFourProjectile : BombProjectile
{
    private const int CollidedIndex = 1;
    private const int RotationIndex = 2;
    
    public override void SetDefaults()
    {
        NormalSize = new Point(22, 22);
        ExplodingSize = new Point(180, 180);
        Damage = 110;
        KnockBack = 8f;
        
        Projectile.CloneDefaults(ProjectileID.Bomb);
        FuseRelPosition = new Vector2(0, 0);
        Projectile.width = NormalSize.X;
        Projectile.height = NormalSize.Y;

        Projectile.ai[CollidedIndex] = 0;
        Projectile.ai[RotationIndex] = 0;
    }

    protected override void PositionalAi()
    {
        if (Projectile.ai[CollidedIndex] > 0f)
        {
            Projectile.velocity = Vector2.Zero;
            Projectile.rotation = Projectile.ai[RotationIndex];
        }
    }

    protected override void ParticleOnKill()
    {
    }

    protected override void ExplosionEffect()
    {
    }

    protected override void OnHorizontalCollision(Vector2 oldVelocity, bool wasCeiling)
    {
        if (Projectile.ai[CollidedIndex] > 0f) return;
        
        Projectile.ai[CollidedIndex] = 1f;
        Projectile.ai[RotationIndex] = MathF.PI * Convert.ToInt32(wasCeiling);
    }

    protected override void OnVerticalCollision(Vector2 oldVelocity, bool wasLeft)
    {
        if (Projectile.ai[CollidedIndex] > 0f) return;
        
        Projectile.ai[CollidedIndex] = 1f;
        Projectile.ai[RotationIndex] = (MathHelper.TwoPi - MathHelper.PiOver2) - MathF.PI * Convert.ToInt32(wasLeft);
    }
}