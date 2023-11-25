using System;

namespace BombOPlenty.Helpers;

public static class PlayerHelpers
{
    public static int GetIndex(this Player player) =>
        Array.IndexOf(Main.player, player);
}