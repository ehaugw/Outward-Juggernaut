using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juggernaut
{
    public class FortifiedSpell
    {
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Fortified",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.steadyArmID,
                New_ItemID = IDs.fortifiedID,
                SLPackName = "Juggernaut",
                SubfolderName = "Fortified",
                Description = String.Format("Gives resistance bonuses equal to your Protection."),//\n\n{0}: Gives impact resistance equal to your armor protection.\n\n{1}: Gives elemental resistance equal to your armor protection.", CarefulSpell.NAME, VengefulSpell.NAME),
                IsUsable = false,
                CastType = Character.SpellCastType.NONE,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1f,
            };
            myitem.ApplyTemplate();
            return ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;
        }
    }

    [HarmonyPatch(typeof(CharacterEquipment), "GetEquipmentDamageResistance")]
    public class CharacterEquipment_GetEquipmentDamageResistance
    {
        [HarmonyPostfix]
        public static void Postfix(CharacterEquipment __instance, ref float __result, ref DamageType.Types _type)
        {
            if (_type == DamageType.Types.Physical && At.GetField<CharacterEquipment>(__instance, "m_character") is Character character)
            {
                if (SkillRequirements.CanAddProtectionToPhysicalResistance(character) || SkillRequirements.CanAddProtectionToAnyDamageResistance(character))
                {
                    __result += character.Stats.GetDamageProtection(DamageType.Types.Physical);
                    //__instance.GetEquipmentDamageProtection(DamageType.Types.Physical);
                }
            }
        }
    }

    [HarmonyPatch(typeof(CharacterEquipment), "GetEquipmentImpactResistance")]
    public class CharacterEquipment_GetEquipmentImpactResistance
    {
        [HarmonyPostfix]
        public static void Postfix(CharacterEquipment __instance, ref float __result)
        {
            if (At.GetField<CharacterEquipment>(__instance, "m_character") is Character character && SkillRequirements.CanAddProtectionToImpactResistance(character))
            {
                __result += character.Stats.GetDamageProtection(DamageType.Types.Physical);
                //__result += __instance.GetEquipmentDamageProtection(DamageType.Types.Physical);
            }
        }
    }
}
