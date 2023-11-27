using System;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.GameContent.Achievements;

namespace BombOPlenty.Content.Projectiles.Explosives;

public class BombArrowProjectile : BombProjectile 
{
    public override void SetDefaults()
    {
        NormalSize = new Point(20, 32);
        ExplodingSize = new Point(95, 95);
        Damage = 75;
        Radius = 4f;

        Projectile.width = NormalSize.X;
        Projectile.height = NormalSize.Y;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
    }

    protected override bool EarlyAi()
    {
        if (Projectile.timeLeft <= ExplodingTimeLeft)
            Projectile.hostile = true;
        
        return base.EarlyAi();
    }

    protected override void PositionalAi()
    {
        Projectile.ai[0]++;
        if (Projectile.ai[0] > 5)
        {
            Projectile.ai[0] = 10;
            Projectile.velocity.Y += 0.087f;
        }
        Projectile.rotation = Projectile.velocity.AngleFrom(Vector2.UnitX) + MathHelper.PiOver2;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (target.friendly) return;
        
        // Otherwise the explosion won't do any damage to the npc on a direct hit
        var didEnoughMultiplier = Convert.ToInt32(damageDone > Damage - 20);
        target.immune[Projectile.owner] = 2 + 18 * didEnoughMultiplier;
        
        Projectile.timeLeft = ExplodingTimeLeft + 1;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        if (!target.hostile) return;
        
        // Otherwise the explosion won't do any damage to the npc on a direct hit
        var didEnoughMultiplier = Convert.ToInt32(info.Damage > Damage - 20);
        target.immuneTime = 2 + 18 * didEnoughMultiplier;
        
        Projectile.timeLeft = ExplodingTimeLeft + 1;
    }

    protected override void ParticleOnKill()
    {
        Parallel.For(0, 50, _ =>
        {
            var smokeDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke,
                0, 0, 100);
            smokeDust.velocity *= 3f;
            smokeDust.noGravity = true;
        });
        
        var fireLoc = Projectile.Center - new Vector2(1);
        Parallel.For(0, 70, _ =>
        {
            var fireDust = Dust.NewDustDirect(fireLoc, 1, 1, DustID.Torch, 0, 0, 100);
            fireDust.velocity *= 3f;
        });
    }

    protected override void ExplosionEffect()
    {
         SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
         AchievementsHelper.CurrentlyMining = true;
         ExplosionHelpers.KillTiles(Projectile.Center, Radius);
         ExplosionHelpers.KillWalls(Projectile.Center, Radius + 1, true);
         AchievementsHelper.CurrentlyMining = false;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.timeLeft = ExplodingTimeLeft + 1;
        return false;
    }
}