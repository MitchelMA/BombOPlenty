using System.Collections.Generic;

namespace BombOPlenty.Trackers;

public abstract class ProjectileTracker<T>
    where T : ModProjectile
{
    protected static readonly Dictionary<int, HashSet<T>> Tracked = new ();

    public abstract T? Register(int playerIdx, T projectile);
    public abstract T? Kill(int playerIdx, T projectile);
    public abstract int TrackedCount(int playerIdx);
}