using CustomWeaponBehaviour;
using InstanceIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Juggernaut
{
    public class SkillRequirements
    {
        //# (T)ackle:             A Deals equal impact damage to yourself and the tackled target, untill either is knocked back.
        //# (B)astard:            P Bonus damage and speed with Bastard Swords.
        //# (U)nyielding:         P Gain physical resistance equal to protection from equipment.
        //#                         Armor speed penalty reduced.
        //#                         If Vengeful:    Gain impact resistance equal to protection from equipment.
        //#                         If Careful:     Gain elemental resistance equal to protection from equipment.
        //# (C)areful:            P Parry much sooner when not using a shield.
        //# (V)engeful:           P Taking damage (before resistances) generates rage buildup.
        //# (R)uthless:           P Savage: Vengeful + Rage, Iron Will: Careful + Discipline
        //#                         If Savage:      Increases all damage dealt with weapons by 30%.
        //#                         If Savage:      Attack stamina cost reduced by 50%.
        //#                         If Savage:      Purges all boons except rage while Ruthless remains active.
        //#                         If Iron Will:   Armor stamina penalties from armor are reduced significantly.
        //#                         If Iron Will:   Purges rage while you remain discipined.
        //#                         If Iron Will:   A portion of your weapon damage is converted to raw/true damage, ignoring bonuses and resistances.
        //#    NOT YET FINISHED     ??????????:     When lethaly hit, damages burnt health instead of current health. You lose enrage.
        //# (W)ar cry:            A Enemies are knocked back
        //#                         If Careful:     Applies confusion to all nearby enemies.
        //#                         If Vengeful:    Applies pain to all nearby enemies.
        //#
        //# (H)orde Breaker:      A Double spinning attack (Moon Swipe animation) with any weapon.
        //#                         Enemies are staggered.
        //#                         If Careful:     Confused enemies are knocked down.
        //#                         If Vengeful:    Pained enemies are slowed.

        private static bool SafeHasSkillKnowledge(Character character, int skillID)
        {
            return character?.Inventory?.SkillKnowledge?.IsItemLearned(skillID)??false;
        }
        public static bool IsRageEffect(StatusEffect effect)
        {
            return effect?.HasMatch(TagSourceManager.Instance.GetTag(IDs.rageTagUID.ToString()))??false;
        }
        public static bool IsDisciplineEffect(StatusEffect effect)
        {
            return effect?.HasMatch(TagSourceManager.Instance.GetTag(IDs.disciplineTagUID.ToString()))??false;
        }
        public static bool Enraged(Character character)
        {
            return character.StatusEffectMngr.HasStatusEffect("Rage") || character.StatusEffectMngr.HasStatusEffect("Rage Amplified")/* || character.StatusEffectMngr.HasStatusEffect("Ruthless")*/;
        }
        public static bool Disciplined(Character character)
        {
            return character.StatusEffectMngr.HasStatusEffect("Discipline") || character.StatusEffectMngr.HasStatusEffect("Discipline Amplified");
        }
        public static bool Careful(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.unyieldingID);
        }
        public static bool Vengeful(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.vengefulID);
        }
 
        //Unyielding
        public static bool CanParryCancelAnimations(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.unyieldingID);
        }
        public static bool CanSkillCancelAnimations(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.unyieldingID);
        }
        public static bool CanDelayDamageEqualToProtection(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.unyieldingID);
        }

        //Fortified
        public static bool CanAddProtectionToImpactResistance(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.fortifiedID);// && Careful(character);
        }
        public static bool CanAddProtectionToPhysicalResistance(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.fortifiedID)/* && Enraged(character)*/;

        }
        public static bool CanAddProtectionToAnyDamageResistance(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.fortifiedID);// && Vengeful(character);
        }
        
        //Bastard
        public static bool CanAddBonusBastardWeaponSpeed(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.bastardWeaponTrainingID);
        }
        public static bool CanAddBonusBastardWeaponDamage(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.bastardWeaponTrainingID);
        }

        //Vengeful
        public static bool CanEnrageFromDamage(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.vengefulID);
        }

        //Ruthless - Vengeful
        public static bool CanAddBonusRageDamage(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.ruthlessID) && Enraged(character) && Vengeful(character);
        }
        public static bool CanReduceWeaponAttackStaminaCost(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.ruthlessID) && Enraged(character) && Vengeful(character);
        }
        public static bool ShouldPurgeAllExceptRageGivenEnraged(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.ruthlessID) && Vengeful(character);
        }

        //Ruthless - Unyielding
        public static bool CanReduceMoveSpeedArmorPenalty(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.ruthlessID) && SafeHasSkillKnowledge(character, IDs.unyieldingID);
        }
        public static bool CanReduceStaminaCostArmorPenalty(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.ruthlessID) && SafeHasSkillKnowledge(character, IDs.unyieldingID);
        }
        public static bool CanTerrify(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.ruthlessID) && SafeHasSkillKnowledge(character, IDs.unyieldingID);
        }

        //Ruthless - Any
        public static bool ApplyRuthlessSize(Character character)
        {
            return SafeHasSkillKnowledge(character, IDs.ruthlessID);
        }

        //Discarded
        public static bool CanConvertToRawDamage(Character character)
        {
            return false;// SafeHasSkillKnowledge(character, IDs.ruthlessID) && Disciplined(character) && Careful(character);
        }
        public static bool ShouldPurgeOnlyRageGivenDisciplined(Character character)
        {
            return false;// SafeHasSkillKnowledge(character, IDs.ruthlessID) && Careful(character);
        }
    }
}
