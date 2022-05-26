using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Juggernaut
{
    class TackleEffect : PunctualDamage
    {
        protected override void ActivateLocally(Character _affectedCharacter, object[] _infos)
        {
            var defender = _affectedCharacter;
            var offender = OwnerCharacter;

            if (defender.Stability <= 0 || defender.IsInKnockback || offender.Stability <= 0 || offender.IsInKnockback) return;

            var impactResistDefender = Mathf.Min(defender.Stats.GetImpactResistance() * 0.01f, 1f);
            var impactResistOffender = Mathf.Min(offender.Stats.GetImpactResistance() * 0.01f, 1f);

            var knockVector = (Vector3)_infos[1];
            //Infinite effective stability cases
            if (impactResistOffender == impactResistDefender)
            {
                defender.AutoKnock(true, knockVector, offender);
                offender.AutoKnock(true, -knockVector, defender);
            }
            else if(impactResistOffender == 1)
            {
                defender.AutoKnock(true, knockVector, offender);
            }
            else if (impactResistDefender == 1)
            {
                offender.AutoKnock(true, -knockVector, defender);
            }

            //Finite effective stability cases
            else
            {
                var defrem = defender.Stability / (1 - impactResistDefender);
                var offrem = offender.Stability / (1 - impactResistOffender);

                Knockback = Mathf.Min(defrem, offrem);
                this.DamageAmplifiedByOwner = false;
                
                if (defrem < offrem) {
                    defender.AutoKnock(true, (Vector3) _infos[1], offender);
                    _infos[1] = -(Vector3)_infos[1];
                    base.ActivateLocally(offender, _infos);
                } else
                {
                    base.ActivateLocally(defender, _infos);
                    _infos[1] = -(Vector3)_infos[1];
                    offender.AutoKnock(true, (Vector3)_infos[1], defender);
                }

            }
        }
    }
}
