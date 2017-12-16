﻿using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.WarningOptions
{
    [StaticConstructorOnStartup]
    internal class Main
    {
        static Main()
        {
            Log.Message("Patching EnhancedDevelopment.WarningOptions");
            //HarmonyInstance.Create("EnhancedDevelopment.WarningOptions")PatchAll(Assembly.GetExecutingAssembly());
            HarmonyInstance _Harmony = HarmonyInstance.Create("EnhancedDevelopment.WarningOptions");
            _Harmony.PatchAll(Assembly.GetExecutingAssembly());
            //_Harmony.
            //_Harmony.Patch(Patch_ReceiveLetter);


            Log.Message("Patching EnhancedDevelopment.WarningOptions Complete");
        }
    }

    [HarmonyPatch(typeof(LetterStack))]
    [HarmonyPatch("ReceiveLetter")]
    [HarmonyPatch(new Type[] { typeof(Letter), typeof(string) })]
    class Patch_ReceiveLetter
    {
        static bool Prefix(ref Letter let)
        {
            //Log.Message("Big Threat");

            if (let.def == LetterDefOf.ThreatBig)
            {
                return Mod_WarningOptions.Settings.ShowLettersThreatBig;
            }
            
            if (let.def == LetterDefOf.ThreatSmall)
            {
                return Mod_WarningOptions.Settings.ShowLettersThreatSmall;
            }

            if (let.def == LetterDefOf.NegativeEvent)
            {
                return Mod_WarningOptions.Settings.ShowLettersNegativeEvent;
            }

            if (let.def == LetterDefOf.NeutralEvent)
            {
                return Mod_WarningOptions.Settings.ShowLettersNeutralEvent;
            }

            if (let.def == LetterDefOf.PositiveEvent)
            {
                return Mod_WarningOptions.Settings.ShowLettersPositiveEvent;
            }

            if (let.def == LetterDefOf.ItemStashFeeDemand)
            {
                return Mod_WarningOptions.Settings.ShowLettersItemStashFeeDemand;
            }

            ////AllowGood version only
            //if (let. == LetterType.Good)
            //{
            //    return true;
            //}

            // Allow any other types of Letters
            return true;
        }
    }

}
