namespace Juggernaut
{
    using UnityEngine;
    using InstanceIDs;
    using TinyHelper;

    class HordeBreakerEffect : Effect
    {

        public static void StaticActivate(HordeBreakerEffect effect, Character defender, Character offender, object[] _infos, Effect instance)
        {
            if (SkillRequirements.Careful(offender) && defender.StatusEffectMngr.HasStatusEffect(IDs.confusionNameID))
            {
                defender.AutoKnock(true, Vector3.zero, offender);
            } else
            {
                CasualStagger.Stagger(defender);
            }

            if (SkillRequirements.Vengeful(offender) && defender.StatusEffectMngr.HasStatusEffect(IDs.painNameID))
            {
                defender.StatusEffectMngr.AddStatusEffect(ResourcesPrefabManager.Instance.GetStatusEffectPrefab(IDs.slowNameID), offender);
            }

        }

        protected override void ActivateLocally(Character _affectedCharacter, object[] _infos)
        {
            StaticActivate(this, _affectedCharacter, this.OwnerCharacter, _infos, this);
        }
    }
}
