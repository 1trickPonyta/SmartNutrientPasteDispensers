﻿namespace SmartNutrientPasteDispensers
{
    public static class Debug
    {
        public static void Log(string message)
        {
#if DEBUG
            Verse.Log.Message($"[{SmartNutrientPasteDispensersMod.PACKAGE_NAME}] {message}");
#endif
        }
    }
}
