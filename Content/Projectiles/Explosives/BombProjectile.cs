using System;

namespace BombOPlenty.Content.Projectiles.Explosives;

public abstract class BombProjectile : ModProjectile
{
    protected Point NormalSize;
    protected Point ExplodingSize;
    protected Vector2 FuseRelPosition;
    
    protected int Damage;
    protected float KnockBack;
    protected float Radius;

    protected const int ExplodingTimeLeft = 3;
    
    protected bool KillOnTileHit = false;

    protected virtual bool EarlyAi()
    {
        return true;
    }
    protected virtual void PositionalAi() { }
    protected virtual void ExtraParticleAi() { }

    protected abstract void ParticleOnKill();
    protected abstract void ExplosionEffect();
    protected virtual void OnKillExtra(int timeLeft) { }

    protected virtual void OnCollision(Vector2 oldVelocity) { }
    protected virtual void OnHorizontalCollision(Vector2 oldVelocity, bool wasCeiling) { }
    protected virtual void OnVerticalCollision(Vector2 oldVelocity, bool wasLeft) { }

    public override void AI()
    {
        if (!EarlyAi()) return;
        if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= ExplodingTimeLeft)
        {
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            
            Projectile.Resize(ExplodingSize.X, ExplodingSize.Y);

            Projectile.damage = Damage;
            Projectile.knockBack = KnockBack;
            
            return;
        }
        
        FuseParticleAi();
        
        ExtraParticleAi();
        PositionalAi();
    }

    public override void OnKill(int timeLeft)
    {
        ParticleOnKill();
        Projectile.Resize(NormalSize.X, NormalSize.Y);
        ExplosionEffect();
        OnKillExtra(timeLeft);
    }

    protected virtual void FuseParticleAi()
    {
        var absFusePos = Projectile.Center + FuseRelPosition.RotatedBy(Projectile.rotation);
        if (!Main.rand.NextBool(2)) return;
        
        var fuseDust = Dust.NewDustDirect(absFusePos, 1, 1, DustID.Torch, 0, 0, 100);
        fuseDust.scale = 1f + Main.rand.Next(5) * 0.1f;
        fuseDust.noGravity = true;
        
        var fuseSmokeDust = Dust.NewDustDirect(absFusePos, 1, 1, DustID.Smoke, 1, -1, 100);
        fuseSmokeDust.scale = 1f + Main.rand.Next(5) * 0.5f;
        fuseSmokeDust.noGravity = true;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > 0.01f)
            OnVerticalCollision(oldVelocity, oldVelocity.X < 0);
        if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > 0.01f)
            OnHorizontalCollision(oldVelocity, oldVelocity.Y < 0);
        OnCollision(oldVelocity);
        return KillOnTileHit && base.OnTileCollide(oldVelocity);
    }
}