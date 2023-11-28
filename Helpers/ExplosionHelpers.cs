namespace BombOPlenty.Helpers;

public static class ExplosionHelpers
{
    public static void KillWalls(Vector2 centre, float radius, bool behindTiles = false)
    {
        var tileAligned = centre.AlignToTiles();
        var minTileX = MathHelpers.Clamp((int)(tileAligned.X - radius), 0, Main.maxTilesX);
        var maxTileX = MathHelpers.Clamp((int)(tileAligned.X + radius), 0, Main.maxTilesX);
        var minTileY = MathHelpers.Clamp((int)(tileAligned.Y - radius), 0, Main.maxTilesY);
        var maxTileY = MathHelpers.Clamp((int)(tileAligned.Y + radius), 0, Main.maxTilesY);

        for (var i = minTileX; i < maxTileX; i++)
        {
            for (var j = minTileY; j < maxTileY; j++)
            {
                var tilePos = new Point(i, j);
                
                if (Vector2.Distance(new Vector2(tilePos.X, tilePos.Y), tileAligned) > radius)
                    continue;

                var tile = Main.tile[tilePos];

                if (tile == null ||
                    tile.WallType == 0)
                    continue;
                
                if (!behindTiles && tile.HasTile)
                    continue;
                
                if (!WallLoader.CanExplode(i, j, tile.WallType))
                    continue;
                
                WorldGen.KillWall(i, j);
                if (tile.WallType == 0 && Main.netMode != NetmodeID.SinglePlayer)
                    NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, i, j, 0f, 0, 0, 0);
            }
        }
    }

    public static void KillTiles(Vector2 centre, float radius)
    {
        var tileAligned = centre.AlignToTiles();
        var minTileX = MathHelpers.Clamp((int)(tileAligned.X - radius), 0, Main.maxTilesX);
        var maxTileX = MathHelpers.Clamp((int)(tileAligned.X + radius), 0, Main.maxTilesX);
        var minTileY = MathHelpers.Clamp((int)(tileAligned.Y - radius), 0, Main.maxTilesY);
        var maxTileY = MathHelpers.Clamp((int)(tileAligned.Y + radius), 0, Main.maxTilesY);

        for (var i = minTileX; i < maxTileX; i++)
        {
            for (var j = minTileY; j < maxTileY; j++)
            {
                var tilePos = new Point(i, j);
                
                if (Vector2.Distance(new Vector2(tilePos.X, tilePos.Y), tileAligned) > radius)
                    continue;

                var tile = Main.tile[tilePos];
                
                if (tile == null ||
                    tile.HasTile == false)
                    continue;

                if (!TileLoader.CanExplode(i, j))
                    continue;
                
                WorldGen.KillTile(i, j);
                if (!tile.HasTile && Main.netMode != NetmodeID.SinglePlayer)
                    NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, i, j);
            }
        }
    }
}