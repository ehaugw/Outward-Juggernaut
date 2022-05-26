using InstanceIDs;
using SideLoader;
using System.Collections.Generic;
using System.Linq;
using TinyHelper;

namespace Juggernaut
{
    public class WarCryEffect : Effect
    {
        public const float Range = 20;

        protected override void ActivateLocally(Character _affectedCharacter, object[] _infos)
        {
            List<Character> charsInRange = new List<Character>();
            CharacterManager.Instance.FindCharactersInRange(_affectedCharacter.CenterPosition, Range, ref charsInRange);
            charsInRange = charsInRange.Where(c => !c.IsAlly(_affectedCharacter)).ToList();

            
            foreach (Character character in charsInRange)
            {
                if (SkillRequirements.Careful(_affectedCharacter))
                    character.StatusEffectMngr.AddStatusEffect(ResourcesPrefabManager.Instance.GetStatusEffectPrefab(IDs.confusionNameID), _affectedCharacter);
                if (SkillRequirements.Vengeful(_affectedCharacter))
                    character.StatusEffectMngr.AddStatusEffect(ResourcesPrefabManager.Instance.GetStatusEffectPrefab(IDs.painNameID), _affectedCharacter);

                CasualStagger.Stagger(character);
                character.TargetingSystem.SwitchTarget(_affectedCharacter.LockingPoint);

                //var dir = (character.transform.position - _affectedCharacter.transform.position).normalized;
                //character.ReceiveHit(null, 0, dir, character.transform.position - dir, 1, 1, _affectedCharacter, 20);
            }
        }
    }
}
