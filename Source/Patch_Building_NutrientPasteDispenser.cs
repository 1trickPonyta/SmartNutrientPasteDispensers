using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace SmartNutrientPasteDispensers
{
    [HarmonyPatch(typeof(Building_NutrientPasteDispenser))]
    [HarmonyPatch("FindFeedInAnyHopper")]
    public static class Patch_Building_NutrientPasteDispenser_FindFeedInAnyHopper
    {
        public static bool Prefix(Building_NutrientPasteDispenser __instance, ref Thing __result) 
        {
			Thing bestThing = null;

			for (int i = 0; i < __instance.AdjCellsCardinalInBounds.Count; i++)
			{
				Thing foodThing = null;
				Thing hopperThing = null;

				List<Thing> thingList = __instance.AdjCellsCardinalInBounds[i].GetThingList(__instance.Map);
				for (int j = 0; j < thingList.Count; j++)
				{
					Thing thing = thingList[j];
					if (Building_NutrientPasteDispenser.IsAcceptableFeedstock(thing.def))
					{
						foodThing = thing;
					}
					if (thing.def == ThingDefOf.Hopper)
					{
						hopperThing = thing;
					}
				}

				if (foodThing != null && hopperThing != null)
				{
					bestThing = betterThing(bestThing, foodThing);
				}
			}

			__result = bestThing;
			return false;
        }

		private static Thing betterThing(Thing thing1, Thing thing2)
        {
			// If either thing is null, the other thing is better
			if (thing1 == null) return thing2;
			if (thing2 == null) return thing1;

			// If either thing does not rot, the other thing is better
			CompRottable rottable1 = thing1.TryGetComp<CompRottable>();
			CompRottable rottable2 = thing2.TryGetComp<CompRottable>();
			if (rottable1 == null) return thing2;
			if (rottable2 == null) return thing1;

			// Whichever thing rots sooner is better
			if (rottable1.TicksUntilRotAtCurrentTemp < rottable2.TicksUntilRotAtCurrentTemp) return thing1;
			else return thing2;
		}
    }
}
