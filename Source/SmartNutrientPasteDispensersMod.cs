using Verse;
using HarmonyLib;

namespace SmartNutrientPasteDispensers
{
    public class SmartNutrientPasteDispensersMod : Mod
    {
        public const string PACKAGE_ID = "smartnutrientpastedispensers.1trickPonyta";
        public const string PACKAGE_NAME = "Smart Nutrient Paste Dispensers";

        public SmartNutrientPasteDispensersMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
