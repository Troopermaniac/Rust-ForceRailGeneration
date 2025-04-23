
// ============================================================================
// Author: TrooperManiac aka EmberDev
// Version: 1.0.0
// Description: Configuration notes for Rust procedural generation
// Note: Ensure 'abovegroundrails' is enabled in world.cfg if present
// Reference: https://wiki.facepunch.com/rust/procedural_generation_customization
// ============================================================================

using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

public class ForceRailGeneration
{
    private const string HarmonyId = "com.troop.forcerailgeneration";
    private static Harmony _harmony;

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        try
        {
            if (!World.Config.AboveGroundRails)
            {
                UnityEngine.Debug.LogWarning("[ForceRailGeneration] AboveGroundRails is disabled. Enabling it.");
                World.Config.AboveGroundRails = true;
            }

            _harmony = new Harmony(HarmonyId);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            UnityEngine.Debug.Log("[ForceRailGeneration] Harmony patch applied for rail generation.");
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"[ForceRailGeneration] Error initializing: {ex.Message}");
        }
    }

    [HarmonyPatch(typeof(GenerateRailRing), "Process")]
    public class RailPatch
    {
        static bool Prefix(ref int ___MinWorldSize)
        {
            try
            {
                ___MinWorldSize = 0;
                UnityEngine.Debug.Log($"[ForceRailGeneration] Bypassed MinWorldSize check. Set to: {___MinWorldSize}, World.Size: {ConVar.Server.worldsize}, AboveGroundRails: {World.Config.AboveGroundRails}");
                return true;
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"[ForceRailGeneration] Error in RailPatch: {ex.Message}");
                return false;
            }
        }
    }
}