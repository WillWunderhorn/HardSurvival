using MelonLoader;
using Il2Cpp;
using Il2CppTLD.Gear;
using UnityEngine;

namespace HardSurvival
{
    public class Implementation : MelonMod
    {

    }

    //--------------------------------------------------------------------------------------------------------- Condition improvements
    // All damage increased by 20%
    [HarmonyLib.HarmonyPatch(typeof(Condition), nameof(Condition.Start))]
    internal class HardSurvivalPatch
    {
        internal static void Postfix(ref Condition __instance)
        {
            __instance.m_MaxHP = 80;
        }
    }

    // Improved Hunger
    [HarmonyLib.HarmonyPatch(typeof(Hunger), nameof(Hunger.Start))]
    public static class IncreaseHungerRate
    {
        internal static void Postfix(ref Hunger __instance)
        {
            __instance.m_CalorieBurnPerHourWalking *= 1.3f;
            __instance.m_CalorieBurnPerHourBuildingSnowShelter *= 3;
            __instance.m_CalorieBurnPerHourDismantlingSnowShelter *= 3f;
            __instance.m_CalorieBurnPerHourClimbing *= 3;
            __instance.m_CalorieBurnPerHourSprinting *= 2;
            __instance.m_CalorieBurnPerHourStanding *= 1.2f;
            __instance.m_CalorieBurnPerHourHarvestingCarcass *= 1.4f;

        }
    }

    // Improved Fatigue
    [HarmonyLib.HarmonyPatch(typeof(Fatigue), nameof(Fatigue.Start))]
    public static class IncreaseFatigueRate
    {
        internal static void Postfix(ref Fatigue __instance)
        {
            __instance.m_FatigueIncreasePerHourStanding *= 1.3f;
            __instance.m_FatigueIncreasePerHourWalking *= 1.5f;
            __instance.m_FatigueIncreasePerHourSprintingMax *= 2f;
            __instance.m_FatigueIncreasePerHourSprintingMin *= 1.5f;

        }
    }

    // Improved Thirst
    [HarmonyLib.HarmonyPatch(typeof(Thirst), nameof(Thirst.Start))]
    public static class IncreaseThirstRate
    {
        internal static void Postfix(ref Thirst __instance)
        {
            __instance.m_ThirstIncreasePerDay *= 1.1f;
        }
    }

    // Improved Stim
    [HarmonyLib.HarmonyPatch(typeof(EmergencyStim), nameof(EmergencyStim.ApplyEmergencyStim))]
    public static class ApplyEmergencyStimRedused
    {
        internal static void Postfix(ref EmergencyStim __instance)
        {
            __instance.m_EmergencyStimParams.m_HoursStimulatedWhenInjected = 0.5f;
            __instance.m_EmergencyStimParams.m_FatigueIncreaseWhenComplete -= 40f;
        }
    }

    // *IMPORTANT* add instant death from weak ice brasking

    //--------------------------------------------------------------------------------------------------------- Improved cold
    // Improved blizzards
    [HarmonyLib.HarmonyPatch(typeof(Weather), nameof(Weather.CalculateCurrentTemperature))]
    public static class ImprovedWeather
    {
        internal static void Postfix(ref Weather __instance)
        {
            float multiplier = ExperienceModeManager.GetCurrentExperienceModeType() == ExperienceModeType.Interloper ? 3f : 9f; // don't delete, I'll use it later

            // *IMPORTANT* In the next update fix temp indoors
            if (!GameManager.GetWeatherComponent().IsIndoorScene() && __instance.IsBlizzard())
            {
                __instance.m_CurrentTemperature *= 4f;
            }
            else
            {
                __instance.m_CurrentTemperature -= multiplier;
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------- Improved Afflictions

    // Hypothermia now 20% faster
    [HarmonyLib.HarmonyPatch(typeof(Hypothermia), nameof(Hypothermia.Start))]
    public static class IncreaseHypothermiaRate
    {
        internal static void Postfix(ref Hypothermia __instance)
        {
            __instance.m_HoursSpentFreezingRequired = 0.8f;
        }
    }

    // *IMPORTANT* make bleeding more deadly
    // *IMPORTANT* make infection more deadly
    // *IMPORTANT* fall damage x2

    //--------------------------------------------------------------------------------------------------------- Wildlife improvements

    // Improved bleeding for animals
    [HarmonyLib.HarmonyPatch(typeof(BodyDamage), nameof(BodyDamage.GetBleedOutMinutes))]
    public static class ModifyBleedOutTime
    {
        internal static void Postfix(BodyPart bodyPart, WeaponSource w, ref float __result)
        {
            __result *= 4;
        }
    }

    // Improved AI for bears
    [HarmonyLib.HarmonyPatch(typeof(BaseAi), nameof(BaseAi.Awake))]
    public static class IncreaseBearDetectionRange
    {
        internal static void Postfix(BaseAi __instance)
        {
            if (__instance.m_AiSubType == AiSubType.Bear)
            {
                __instance.m_SmellRange *= 4f;
                __instance.m_HearFootstepsRange *= 4f;
                __instance.m_DetectionRange *= 4f;
            }
        }
    }


    // Improved AI for wolves
    [HarmonyLib.HarmonyPatch(typeof(BaseAi), nameof(BaseAi.Awake))]
    public static class IncreaseWolfDetectionRange
    {
        internal static void Postfix(BaseAi __instance)
        {
            if (__instance.m_AiSubType == AiSubType.Wolf)
            {
                float multiplier = ExperienceModeManager.GetCurrentExperienceModeType() == ExperienceModeType.Interloper ? 2.5f : 2f;
                __instance.m_SmellRange *= multiplier;
                __instance.m_HearFootstepsRange *= multiplier;
                __instance.m_RangeMeleeAttack *= multiplier;
            }
        }
    }

    // Improved AI for moose
    [HarmonyLib.HarmonyPatch(typeof(BaseAi), nameof(BaseAi.Awake))]
    public static class IncreaseMooseDanger
    {
        internal static void Postfix(BaseAi __instance)
        {
            if (__instance.m_AiSubType == AiSubType.Moose)
            {
                __instance.m_RangeMeleeAttack *= 2f;
                __instance.m_DetectionRange *= 2f;
            }
        }
    }

    // Improved AI for rabbits
    [HarmonyLib.HarmonyPatch(typeof(BaseAi), nameof(BaseAi.Awake))]
    public static class IncreaseRabbitDetectionRange
    {
        internal static void Postfix(BaseAi __instance)
        {
            if (__instance.m_AiSubType == AiSubType.Rabbit)
            {
                __instance.m_DetectionRange *= 5f;
            }
        }
    }

    // Improved AI for stags
    [HarmonyLib.HarmonyPatch(typeof(BaseAi), nameof(BaseAi.Awake))]
    public static class IncreaseStagDetectionRange
    {
        internal static void Postfix(BaseAi __instance)
        {
            if (__instance.m_AiSubType == AiSubType.Stag)
            {
                __instance.m_DetectionRange *= 2.3f;
            }
        }
    }
    

    // *IMPORTANT* make timderwolves morale 2 times bigger
    // *IMPORTANT* make bears left only 10% - 25% of condition after attack

    //--------------------------------------------------------------------------------------------------------- Fishing improvements

    // Fishing is less profitable
    [HarmonyLib.HarmonyPatch(typeof(IceFishingHole), nameof(IceFishingHole.Awake))]
    public static class DecreaseIceFishingHoleFishAmount
    {
        internal static void Postfix(IceFishingHole __instance)
        {
            __instance.m_MaxGameMinutesCatchFish *= 1.6f;
            __instance.m_MinGameMinutesCatchFish = 20;
        }
    }

    //--------------------------------------------------------------------------------------------------------- Improved tools

    // Improved lantern fuel consumption
    [HarmonyLib.HarmonyPatch(typeof(KeroseneLampItem), nameof(KeroseneLampItem.ReduceFuel))]
    public static class increasedLanternFuelConsumption
    {
        internal static void Prefix(ref float hoursBurned)
        {
            hoursBurned *= 1.7f;
        }
    }

    // Increase heat
    [HarmonyLib.HarmonyPatch(typeof(HeatSource), nameof(HeatSource.Start))]
    public static class IncreaseLanternHeat
    {
        internal static void Postfix(HeatSource __instance)
        {
            if (__instance.gameObject.GetComponent<KeroseneLampItem>() != null)
            {
                __instance.m_MaxTempIncrease *= 2f;
                __instance.m_MaxTempIncreaseInnerRadius /= 2f;
                __instance.m_MaxTempIncreaseOuterRadius /= 2f;
            }

            if (__instance.gameObject.GetComponent<TorchItem>() != null)
            {
                __instance.m_MaxTempIncrease *= 0.5f;
                __instance.m_MaxTempIncreaseInnerRadius /= 2f;
                __instance.m_MaxTempIncreaseOuterRadius /= 2f;
            }

            if (__instance.gameObject.GetComponent<FlareItem>() != null)
            {
                __instance.m_MaxTempIncrease *= 10.5f;
                __instance.m_MaxTempIncreaseInnerRadius /= 2f;
                __instance.m_MaxTempIncreaseOuterRadius /= 2f;
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------- Permanent death

    [HarmonyLib.HarmonyPatch(typeof(Panel_LifeAfterDeath), nameof(Panel_LifeAfterDeath.Initialize))]
    public static class PermanentDeath
    {
        internal static void Postfix(Panel_LifeAfterDeath __instance)
        {
            __instance.m_CheatDeathButtonWidget.gameObject.SetActive(false);
            __instance.m_ReviveInteractions.SetActive(false);

            //Not working yet...
            var grid = __instance.m_ButtonLegendParent.GetComponent<UIGrid>();
            if (grid != null)
            {
                grid.cellWidth *= 0.1f;
                grid.cellHeight *= 0.1f;
                grid.transform.localPosition = new Vector3(grid.transform.localPosition.x - 500f, grid.transform.localPosition.y, grid.transform.localPosition.z);
                grid.Reposition();
            }
        }
    }

}