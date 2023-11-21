using BombOPlenty.Helpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace BombOPlenty.Projectiles;

public class WallBombProjectile : ModProjectile
{
    private const int NormalSize = 40;
    private const int ExplodingSize = 160;
    
    private const int Damage = 95;
    private const float KnockBack = 10f;
    
    private const float Radius = 7f;

    public override void SetDefaults()
    {
        Projectile.aiStyle = ProjectileID.Bomb;
        Projectile.width = NormalSize;
        Projectile.height = NormalSize;
        Projectile.friendly = true;
        Projectile.hostile = true;
        Projectile.penetrate = -1;

        Projectile.timeLeft = 4 * UnitHelpers.SecondsToTicks;
    }

    public override void AI()
    {
        if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
        {
            Projectile.tileCollide = false;
            // Set to transparant. This Projectile technically lives as  transparant for about 3 frames
            Projectile.alpha = 255;
            // change the hitbox size, centered about the original Projectile center. This makes the Projectile damage enemies during the explosion.
            Projectile.Resize(ExplodingSize, ExplodingSize);
            
            Projectile.damage = Damage;
            Projectile.knockBack = KnockBack;
            return;
        }

        // Smoke and fuse dust spawn.
        if (Main.rand.NextBool(2))
        {
            // smoke
            var dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y),
                Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100);
            var dust = Main.dust[dustIndex];
            dust.scale = 0.1f + Main.rand.Next(5) * 0.1f;
            dust.fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
            dust.noGravity = true;
            dust.position = Projectile.Center +
                            new Vector2(0f, -Projectile.height / 2f).RotatedBy(
                                Projectile.rotation) * 1.1f;
            // fuse
            dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width,
                Projectile.height, DustID.Torch, 0f, 0f, 100);
            dust = Main.dust[dustIndex];
            dust.scale = 1f + Main.rand.Next(5) * 0.1f;
            dust.noGravity = true;
            dust.position = Projectile.Center + new Vector2(0f, -Projectile.height / 2f - 6).RotatedBy(Projectile.rotation) * 1.1f;
        }

        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] > 5f)
        {
            Projectile.ai[0] = 10f;
            Projectile.velocity.X *= 0.99f;
            Projectile.velocity.Y += 1 - 0.89f;
        }

        // Rotation increased by velocity.X
        Projectile.rotation += Projectile.velocity.X * 0.1f;
    }

    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        // Smoke Dust spawn
        for (var i = 0; i < 50; i++)
        {
            var dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width,
                Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
            Main.dust[dustIndex].velocity *= 1.4f;
        }

        // Fire Dust spawn
        for (var i = 0; i < 80; i++)
        {
            var dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width,
                Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3f);
            var dust = Main.dust[dustIndex];
            dust.noGravity = true;
            dust.velocity *= 5f;
            dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width,
                Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2f);
            Main.dust[dustIndex].velocity *= 3f;
        }

        Projectile.Resize(NormalSize, NormalSize);
        
        AchievementsHelper.CurrentlyMining = true;
        ExplosionHelpers.KillWalls(Projectile.position, Radius);
        AchievementsHelper.CurrentlyMining = false;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        return false;
    }
}