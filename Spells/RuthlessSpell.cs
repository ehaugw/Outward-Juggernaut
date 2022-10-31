using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Juggernaut
{
    public class RuthlessSpell
    {
        public const float Range = 7f;
        public static Skill Init()
        {
            TinyHelper.TinyHelper.OnDescriptionModified += delegate (Item item, ref string description) {
                if (item.ItemID == IDs.ruthlessID)
                {
                    if (TinyHelper.SkillRequirements.SafeHasSkillKnowledge(CharacterManager.Instance.GetFirstLocalCharacter(), IDs.unyieldingID))
                    {
                        description = "Armor stamina and movement penalties are reduced. Damaging enemies causes confusion among their allies, and may even cause them to stagger in fear.";
                    }
                    else if (TinyHelper.SkillRequirements.SafeHasSkillKnowledge(CharacterManager.Instance.GetFirstLocalCharacter(), IDs.vengefulID))
                    {
                        description = "While enraged, all damage is increased and the attack stamina cost is reduced, but you can't be affected by boons other than Rage.";
                    }
                }
            };

            var myitem = new SL_Skill()
            {
                Name = "Ruthless",
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.steadyArmID,
                New_ItemID = IDs.ruthlessID,
                SLPackName = "Juggernaut",
                SubfolderName = "Ruthless",
                Description = String.Format("{0}: Armor stamina and movement penalties are reduced. Damaging enemies causes confusion among their allies, and may even cause them to stagger in fear.\n\n{1}: While enraged, weapon damage is increased and the attack stamina cost is reduced, but you can't be affected by boons other than Rage.", UnyieldingSpell.NAME, VengefulSpell.NAME),
                //String.Format("Requires Rage boon.\n\nWeapon attacks deal {0}% more damage and consume {1}% less stamina, but you lose all boons except Rage as long you remain enraged.", (JuggernautFormulas.RUTHLESS_DAMAGE_BONUS*100).ToString("F0"), (JuggernautFormulas.RUTHLESS_ATTACK_STAMINA_COST_REDUCTION * 100).ToString("F0")),
                CastType = Character.SpellCastType.Rage,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = 0f,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            //var condition = skill.gameObject.AddComponent<StatusEffectCondition>();
            //condition.StatusEffectPrefab = ResourcesPrefabManager.Instance.GetStatusEffectPrefab("Ruthless");

            //Skill predatorLeap = (Skill) ResourcesPrefabManager.Instance.GetItemPrefab(IDs.predatorLeapID);
            //var conditions = At.GetValue(typeof(Skill), predatorLeap, "m_additionalConditions") as Skill.ActivationCondition[];
            //At.SetValue(conditions, typeof(Skill), skill, "m_additionalConditions");

            return skill;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(CharacterStats), "GetAmplifiedDamage")]
    public class CharacterStats_GetAmplifiedDamage
    {
        [HarmonyPostfix]
        public static void Postfix(CharacterStats __instance, ref DamageList _damages)
        {
            if (At.GetField<CharacterStats>(__instance, "m_character") is Character character)
            {
                if (SkillRequirements.CanAddBonusRageDamage(character))
                {
                    _damages *= 1 + JuggernautFormulas.GetRuthlessDamageBonus(character);
                }
                if (SkillRequirements.CanConvertToRawDamage(character))
                {
                    var total = _damages.TotalDamage;
                    var ratio = JuggernautFormulas.GetRuthlessRawDamageRatio(character);
                    _damages *= (1 - ratio);
                    _damages.Add(new DamageType(DamageType.Types.Raw, total * ratio));
                }
            }
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(Weapon), "GetStamCost")]
    public class Weapon_GetStamCost
    {
        [HarmonyPostfix]
        public static void Postfix(Weapon __instance, ref float __result)
        {
            if (__instance.IsEquipped && __instance.OwnerCharacter is Character character)
            {
                if (SkillRequirements.CanReduceWeaponAttackStaminaCost(character))
                {
                    __result *= 1 - JuggernautFormulas.GetRuthlessAttackStaminaCostReduction(character);
                }
            }
        }
    }

    [HarmonyPatch(typeof(CharacterEquipment), "GetTotalMovementModifier")]
    public class CharacterEquipment_GetTotalMovementModifier
    {
        [HarmonyPrefix]
        public static void Prefix(CharacterEquipment __instance, out Tuple<float?, Stat, Character> __state)
        {
            __state = null;
            if (At.GetField<CharacterEquipment>(__instance, "m_character") is Character character && SkillRequirements.CanReduceMoveSpeedArmorPenalty(character))
            {
                CharacterStats stats = character.Stats;
                if (At.GetField<CharacterStats>(stats, "m_equipementPenalties") is Stat equipmentPenalty)
                {
                    __state = new Tuple<float?, Stat, Character>(equipmentPenalty.CurrentValue, equipmentPenalty, character);
                    At.SetField<Stat>(equipmentPenalty, "m_currentValue", (float)__state.Item1 * JuggernautFormulas.GetUnyieldingMovementSpeedForgivenes(character));
                }
            }
        }

        [HarmonyPostfix]
        public static void Postfix(CharacterEquipment __instance, Tuple<float?, Stat, Character> __state)
        {
            if (__state != null)
            {
                if (__state.Item1 != __state.Item2.CurrentValue / JuggernautFormulas.GetUnyieldingMovementSpeedForgivenes(__state.Item3))
                    Debug.Log("Logic error at CharacterEquipment_GetTotalMovementModifier in Juggernaut class. m_equipementPenalties changed during call!");
                At.SetField<Stat>(__state.Item2, "m_currentValue", (float)__state.Item1);
            }
        }
    }

    [HarmonyPatch(typeof(CharacterEquipment), "GetTotalStaminaUseModifier")]
    public class CharacterEquipment_GetTotalStaminaUseModifier
    {
        [HarmonyPrefix]
        public static void Prefix(CharacterEquipment __instance, out Tuple<float?, Stat, Character> __state)
        {
            __state = null;
            if (At.GetField<CharacterEquipment>(__instance, "m_character") is Character character && SkillRequirements.CanReduceStaminaCostArmorPenalty(character))
            {
                CharacterStats stats = character.Stats;
                if (At.GetField<CharacterStats>(stats, "m_equipementPenalties") is Stat equipmentPenalty)
                {
                    __state = new Tuple<float?, Stat, Character>(equipmentPenalty.CurrentValue, equipmentPenalty, character);
                    At.SetField<Stat>(equipmentPenalty, "m_currentValue", (float)__state.Item1 * JuggernautFormulas.GetUnyieldingStaminaCostForgivenes(character));
                }
            }
        }

        [HarmonyPostfix]
        public static void Postfix(CharacterEquipment __instance, Tuple<float?, Stat, Character> __state)
        {
            if (__state != null)
            {
                if (__state.Item1 != __state.Item2.CurrentValue / JuggernautFormulas.GetUnyieldingStaminaCostForgivenes(__state.Item3))
                    Debug.Log("Logic error at CharacterEquipment_GetTotalStaminaUseModifier in Juggernaut class. m_equipementPenalties changed during call!");
                At.SetField<Stat>(__state.Item2, "m_currentValue", (float)__state.Item1);
            }
        }
    }

    [HarmonyPatch(typeof(StatusEffectManager), "AddStatusEffect", new Type[] { typeof(StatusEffect), typeof(Character), typeof(string[]) })]
    public class StatusEffectManager_AddStatusEffect
    {
        [HarmonyPrefix]
        public static bool Prefix(StatusEffectManager __instance, ref StatusEffect _statusEffect/*, StatusEffect __result*/)
        {
            var newEffect = _statusEffect;
            var _ = newEffect.InheritedTags;

            if ((newEffect?.HasMatch(TagSourceManager.Boon) ?? false) && At.GetField<StatusEffectManager>(__instance, "m_character") is Character character)
            {
                bool? purgeRage = null;

                if (SkillRequirements.ShouldPurgeAllExceptRageGivenEnraged(character) && (SkillRequirements.Enraged(character) || SkillRequirements.IsRageEffect(newEffect))) purgeRage = false;
                if (SkillRequirements.ShouldPurgeOnlyRageGivenDisciplined(character) && (SkillRequirements.Disciplined(character) || SkillRequirements.IsDisciplineEffect(newEffect))) purgeRage = true;

                if (purgeRage != null)
                {
                    foreach (var effect in __instance.Statuses)
                    {
                        if (effect.HasMatch(TagSourceManager.Boon) && SkillRequirements.IsRageEffect(effect) == purgeRage)
                            effect.IncreaseAge(Convert.ToInt32(effect.RemainingLifespan));
                    }
                    if (SkillRequirements.IsRageEffect(newEffect) == purgeRage)
                        return false;
                }
            }
            return true;
        }
    }
}
