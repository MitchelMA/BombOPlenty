namespace BombOPlenty.Helpers;

public static class ProjectileHelper
{
    public static Player OwnerPlayer(this Projectile projectile) =>
        Main.player[projectile.owner];

    public static bool HasTileBelow(this Projectile projectile)
    {
        var realCentre = projectile.Center.ProjectilePositionToReal();
        var belowPoint =
            (realCentre + new Vector2(0, 1).RotatedBy(projectile.rotation))
            .Vector2ToPoint();

        return Main.tile[belowPoint].HasTile;
    }
}