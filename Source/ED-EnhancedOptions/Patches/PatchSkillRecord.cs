﻿using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    class PatchSkillRecord : Patch
    {

        protected override void ApplyPatch(HarmonyInstance harmony = null)
        {
            //Get the Origional CheckSpring Method
            MethodInfo _RimWorld_SkillRecord_LearnRateFactor = typeof(RimWorld.SkillRecord).GetMethod("LearnRateFactor", BindingFlags.Public | BindingFlags.Instance);
            Patcher.LogNULL(_RimWorld_SkillRecord_LearnRateFactor, "_RimWorld_SkillRecord_LearnRateFactor");

           // MethodInfo _RimWorld_FireWatcher_LargeFireDangerPresent_Getter = _RimWorld_FireWatcher_LargeFireDangerPresent.GetGetMethod();
            //Patcher.LogNULL(_RimWorld_FireWatcher_LargeFireDangerPresent_Getter, "_RimWorld_FireWatcher_LargeFireDangerPresent_Getter");
            

            //Get the Prefix Patch
            MethodInfo _LearnRateFactorPrefix = typeof(PatchSkillRecord).GetMethod("LearnRateFactorPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_LearnRateFactorPrefix, "_LearnRateFactorPrefix");

            //Apply the Prefix Patch
            harmony.Patch(_RimWorld_SkillRecord_LearnRateFactor, new HarmonyMethod(_LearnRateFactorPrefix), null);
        }

        protected override string PatchDescription()
        {
            return "PatchSkillRecord";
        }

        protected override bool ShouldPatchApply()
        {
            return true;
            //return Mod_EnhancedOptions.Settings.SuppressRainFire;
        }


        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean LearnRateFactorPrefix(ref float __result, ref SkillRecord __instance, bool direct = false)
        {

            //This is the result that will be used, note that it was passed as a ref.
            
            if (DebugSettings.fastLearning)
            {
                __result = 200f;
                return false;
            }
            float num;
            switch (__instance.passion)
            {
                case Passion.None:
                    num = 990.35f;
                    break;
                case Passion.Minor:
                    num = 991f;
                    break;
                case Passion.Major:
                    num = 991.5f;
                    break;
                default:
                    throw new NotImplementedException("Passion level " + __instance.passion);
            }
            if (!direct)
            {
               // num *= __Instance.pawn.GetStatValue(StatDefOf.GlobalLearningFactor, true);
                if (__instance.LearningSaturatedToday)
                {
                    num *= 0.2f;
                }
            }


            __result = num;
            
            //Retuen False so the origional method is not executed, overriting the false result.
            return false;
        }
    }
}