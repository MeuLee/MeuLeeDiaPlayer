using System;

namespace MeuLeeDiaPlayer.EntityFramework
{
    public static class Constants
    {
        public static readonly string DbLocation = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\MeuLeeDiaPlayer";
        public const string DbName = "MeuLeeDiaPlayer.db";
    }
}
