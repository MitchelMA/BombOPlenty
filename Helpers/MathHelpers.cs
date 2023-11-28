using System;

namespace BombOPlenty.Helpers;

public static class MathHelpers
{
    public static T Clamp<T>(T value, T min, T max)
        where T : struct, IComparable, IComparable<T>, IEquatable<T>
    {
        switch (value.CompareTo(max))
        {
            case > 0:
                return max;
            case < 0:
                if (value.CompareTo(min) < 0)
                    return min;
                break;
        }

        return value;
    }

    public static Point Vector2ToPoint(this Vector2 vec)
    {
        return new Point((int) vec.X, (int) vec.Y);
    }
}