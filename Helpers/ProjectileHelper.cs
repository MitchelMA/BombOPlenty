namespace BombOPlenty.Helpers;

public static class ProjectileHelper
{
    public static Player OwnerPlayer(this Projectile projectile) =>
        Main.player[projectile.owner];

    public static bool HasTileBelow(this Projectile projectile, float rotationalOffset = 0)
    {
        var realCentre = projectile.Center.AlignToTiles();
        var belowTileDist = MathHelper.Max(projectile.height / (2f * Constants.TileSize), 1);
        var belowPoint =
            (realCentre + new Vector2(0, belowTileDist).RotatedBy(projectile.rotation + rotationalOffset))
            .Vector2ToPoint();

        return Main.tile[belowPoint].HasTile;
    }
}