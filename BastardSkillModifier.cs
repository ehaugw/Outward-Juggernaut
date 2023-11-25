using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juggernaut
{
    class BastardSkillModifier : CustomWeaponBehaviour.IBastardModifier
    {
        public void ApplyDamageModifier(Weapon weapon, DamageList original, ref DamageList result)
        {
            if (weapon?.OwnerCharacter is Character character && weapon.IsEquipped)
            {
                if (SkillRequirements.CanAddBonusBastardWeaponDamage(character))
                {
                    result += original * JuggernautFormulas.GetBastardDamageBonus();
                }
            }
        }

        public void ApplyImpactModifier(Weapon weapon, float original, ref float result)
        {
            if (weapon?.OwnerCharacter is Character character && weapon.IsEquipped)
            {
                if (SkillRequirements.CanAddBonusBastardImpactBonus(character))
                {
                    result += original * JuggernautFormulas.GetBastardImpactBonus();
                }
            }
        }

        public void ApplySpeedModifier(Weapon weapon, float original, ref float result)
        {
            return;
        }

        public void ApplyStaminaModifier(Weapon weapon, float original, ref float stamina)
        {
            return;
        }
    }
}
