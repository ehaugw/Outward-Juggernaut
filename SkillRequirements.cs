using InstanceIDs;
namespace Juggernaut
{
    public class SkillRequirements
    {
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
        public static bool CanAddBonusBastardImpactBonus(Character character)
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
