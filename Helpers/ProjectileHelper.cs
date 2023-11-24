namespace BombOPlenty.Helpers;

public static class ProjectileHelper
{
    public static Player OwnerPlayer(this Projectile projectile) =>
        Main.player[projectile.owner];
}