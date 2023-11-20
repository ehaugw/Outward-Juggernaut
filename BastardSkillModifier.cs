using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juggernaut
{
    class BastardSkillModifier : CustomWeaponBehaviour.IBastardModifier
    {
        public void ApplyDamageModifier(Weapon weapon, ref float modifier)
        {
            if (weapon != null && weapon.IsEquipped && weapon.OwnerCharacter is Character character)
            {
                if (SkillRequirements.CanAddBonusBastardWeaponDamage(character))
                {
                    modifier += JuggernautFormulas.GetBastardDamageBonus();
                }
            }
        }

        public void ApplySpeedModifier(Weapon weapon, ref float modifier)
        {
            if (weapon != null && weapon.IsEquipped && weapon.OwnerCharacter is Character character)
            {
                if (SkillRequirements.CanAddBonusBastardWeaponSpeed(character))
                {
                    modifier += JuggernautFormulas.GetBastardSpeedBonus();
                }
            }
        }

        public void ApplyImpactModifier(Weapon weapon, ref float modifier)
        {
            return;
        }
    }
}
