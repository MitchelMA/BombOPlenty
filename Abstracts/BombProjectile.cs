namespace BombOPlenty.Abstracts;

public abstract class BombProjectile : ModProjectile
{
    protected Point NormalSize;
    protected Point ExplodingSize;
    protected Vector2 FuseRelPosition;
    
    protected int Damage;
    protected float KnockBack;
    protected float Radius;
    
    protected bool KillOnTileHit = false;

    protected virtual void PositionalAi() { }
    protected virtual void ExtraParticleAi() { }

    protected abstract void ParticleOnKill();
    protected abstract void ExplosionEffect();

    protected virtual void OnCollision(Vector2 oldVelocity) { }
    protected virtual void OnGroundCollision(Vector2 oldVelocity) { }
    protected virtual void OnWallCollision(Vector2 oldVelocity) { }

    public override void AI()
    {
        if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
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
        OnCollision(oldVelocity);
        if (Projectile.velocity.X != oldVelocity.X) OnWallCollision(oldVelocity);
        if (Projectile.velocity.Y != oldVelocity.Y) OnGroundCollision(oldVelocity);
        return KillOnTileHit && base.OnTileCollide(oldVelocity);
    }
}