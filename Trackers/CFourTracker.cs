using System.Collections.Generic;
using System.Linq;
using BombOPlenty.Content.Projectiles.Explosives;

namespace BombOPlenty.Trackers;

public class CFourTracker : ProjectileTracker<CFourProjectile>
{
    private static CFourTracker? _instance;

    public static CFourTracker Instance => _instance ??= new CFourTracker();


    public override CFourProjectile? Register(int playerIdx, CFourProjectile projectile)
    {
        if (!Tracked.ContainsKey(playerIdx))
        {
            Tracked.TryAdd(playerIdx, new HashSet<CFourProjectile> {projectile});
            return projectile;
        }

        if (Tracked[playerIdx].Contains(projectile))
            return null;

        Tracked[playerIdx].Add(projectile);
        return projectile;
    }

    public override CFourProjectile? Unregister(int playerIdx, CFourProjectile projectile)
    {
        if (!Tracked.ContainsKey(playerIdx))
            return null;

        if (!Tracked[playerIdx].Remove(projectile))
            return null;

        if (Tracked[playerIdx].Count == 0)
            Tracked.Remove(playerIdx);

        return projectile;
    }

    public override int TrackedCount(int playerIdx) =>
        !Tracked.ContainsKey(playerIdx) ? 0 : Tracked[playerIdx].Count;

    public bool KillAll(int playerIdx)
    {
        if (!Tracked.ContainsKey(playerIdx))
            return false;

        foreach (var cFour in Tracked[playerIdx].Where(cFour => cFour.HasCollided))
            cFour.Projectile.timeLeft = 0;

        if (TrackedCount(playerIdx) == 0)
            Tracked.Remove(playerIdx);
        
        return true;
    }
}