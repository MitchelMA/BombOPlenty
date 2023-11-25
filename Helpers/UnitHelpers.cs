using Microsoft.Xna.Framework;

namespace BombOPlenty.Helpers;

public static class UnitHelpers
{
    public const float TicksToSeconds = 60 / 1f;
    public const int SecondsToTicks = 60;
    public const int MinutesToSeconds = 60;
    public const int MinutesToTicks = MinutesToSeconds * SecondsToTicks;

    public static Vector2 ProjectilePositionToReal(this Vector2 pos)
    {
        return pos / 16;
    }
}