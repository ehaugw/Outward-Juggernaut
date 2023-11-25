using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Juggernaut
{
    public class JuggernautFormulas
    {
        public const float UNYIELDING_MOVEMENT_SPEED_FORGIVENESS = 0.5f;
        public const float UNYIELDING_STAMINA_COST_FORGIVENESS = 0.5f;
        public const float RUTHLESS_ATTACK_STAMINA_COST_REDUCTION = 0.5f;
        public const float RUTHLESS_DAMAGE_BONUS = 0.3f;
        public const float RUTHLESS_RAW_DAMAGE_RATIO = 0.3f;
        
        public static float GetBastardDamageBonus()
        {
            return 0.1f;
        }

        public static float GetBastardImpactBonus()
        {
            return 0.1f;
        }

        public static float GetUnyieldingMovementSpeedForgivenes(Character character)
        {
            return UNYIELDING_MOVEMENT_SPEED_FORGIVENESS;
        }

        public static float GetUnyieldingStaminaCostForgivenes(Character character)
        {
            return UNYIELDING_STAMINA_COST_FORGIVENESS;
        }

        public static float GetRuthlessAttackStaminaCostReduction(Character character)
        {
            return RUTHLESS_ATTACK_STAMINA_COST_REDUCTION;
        }

        public static float GetRuthlessDamageBonus(Character character)
        {
            return RUTHLESS_DAMAGE_BONUS;
        }

        public static float GetRuthlessRawDamageRatio(Character character)
        {
            return RUTHLESS_RAW_DAMAGE_RATIO;
        }

        public static float GetRuthlessBodySize(Character character)
        {
            return SkillRequirements.ApplyRuthlessSize(character) ? 1.1f : 1.0f;
        }
    }
}
