using CustomWeaponBehaviour;
using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Juggernaut
{
    public class HordeBreakerSpell
    {

        public static void Description(Item item, string description) { }


        public static Skill Init()
        {
            TinyHelper.TinyHelper.OnDescriptionModified += delegate (Item item, ref string description) {
                if (item.ItemID == IDs.hordeBreakerID)
                {
                    if (TinyHelper.SkillRequirements.SafeHasSkillKnowledge(CharacterManager.Instance.GetFirstLocalCharacter(), IDs.unyieldingID))
                    {
                        description = "Does two attacks in wide archs that stagger on hit. Confused enemies are knocked down.";
                    }
                    else if (TinyHelper.SkillRequirements.SafeHasSkillKnowledge(CharacterManager.Instance.GetFirstLocalCharacter(), IDs.vengefulID))
                    {
                        description = "Does two attacks in wide archs that stagger on hit. Enemies in pain are slowed down.";
                    }
                }
            };

            var myitem = new SL_AttackSkill()
            {
                Name = "Horde Breaker",
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.moonswipeID, //perfect strike
                New_ItemID = IDs.hordeBreakerID,
                SLPackName = "Juggernaut",
                SubfolderName = "HordeBreaker",
                Description = String.Format("Does two attacks in wide archs that stagger on hit.\n\n{0}: Confused enemies are knocked down.\n\n{1}: Enemies in pain are slowed down.", UnyieldingSpell.NAME, VengefulSpell.NAME),
                CastType = Character.SpellCastType.WeaponSkill1,
                CastModifier = Character.SpellCastModifier.Attack,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1,
                CastSheatheRequired = 2,

                RequiredWeaponTypes = new Weapon.WeaponType[] {
                    Weapon.WeaponType.FistW_2H,
                    Weapon.WeaponType.Axe_1H,
                    Weapon.WeaponType.Axe_2H,
                    Weapon.WeaponType.Sword_1H,
                    Weapon.WeaponType.Sword_2H,
                    Weapon.WeaponType.Mace_1H,
                    Weapon.WeaponType.Mace_2H,
                    Weapon.WeaponType.Halberd_2H,
                    Weapon.WeaponType.Spear_2H
                },

                EffectTransforms = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] {
                        }
                    },
                },

                Cooldown = 60,
                StaminaCost = 16,
                ManaCost = 0,
                HealthCost = 0,
            };

            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            GameObject.Destroy(skill.transform.Find("HitEffects_Rage").gameObject);
            GameObject.Destroy(skill.transform.Find("HitEffects_Discipline").gameObject);

            //Set the correct animation
            WeaponSkillAnimationSelector.SetCustomAttackAnimation(skill, Weapon.WeaponType.Halberd_2H);

            //Define special on-hit effects
            var hitEffects = skill.transform.Find("HitEffects");
            hitEffects.gameObject.AddComponent<HordeBreakerEffect>();

            //Sett correct damage
            var damageEffect = skill.gameObject.GetComponentInChildren<WeaponDamage>();
            damageEffect.WeaponDamageMult = 1.0f;
            //damageEffect.WeaponDamageMultKDown = 1.0f;
            damageEffect.WeaponDurabilityLossPercent = 0;
            damageEffect.WeaponDurabilityLoss = 1;
            //damageEffect.OverrideDType = DamageType.Types.Count;
            damageEffect.Damages = new DamageType[] {};
            damageEffect.WeaponKnockbackMult = 1f;
            
            //Remove Discipline Condition
            GameObject.Destroy(skill.gameObject.GetComponentInChildren<HasStatusEffectEffectCondition>());
            GameObject.Destroy(skill.gameObject.GetComponentInChildren<AddStatusEffect>());
            return skill;
        }
    }
}
