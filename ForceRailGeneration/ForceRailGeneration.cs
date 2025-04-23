
// ============================================================================
// Author: TrooperManiac aka EmberDev
// Version: 1.0.0
// Note: Ensure 'abovegroundrails' is enabled in world.cfg if present
// Reference: https://wiki.facepunch.com/rust/procedural_generation_customization
// ============================================================================

using HarmonyLib;
using System;

public class ForceRailGeneration
{
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
