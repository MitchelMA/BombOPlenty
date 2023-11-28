namespace BombOPlenty.Helpers;

public static class VectorHelpers
{
    public static Vector2 AlignToTiles(this Vector2 pos)
    {
        return pos / Constants.TileSize;
    }
}