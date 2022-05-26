using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juggernaut
{
    public class ParrySpell
    {
        public static Skill Init()
        {
            var myitem = new SL_AttackSkill()
            {
                Name = "Parry",
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.braceID,
                New_ItemID = IDs.parryID,
                //SLPackName = Crusader.ModFolderName,
                //SubfolderName = "Retributive Smite",
                Description = "Completely block a physical attack.",
                CastType = Character.SpellCastType.Counter,
                CastModifier = Character.SpellCastModifier.Attack,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1,
                CastSheatheRequired = 2,
                RequiredWeaponTypes = new Weapon.WeaponType[] {
                    Weapon.WeaponType.Axe_1H,
                    Weapon.WeaponType.Axe_2H,
                    Weapon.WeaponType.Sword_1H,
                    Weapon.WeaponType.Sword_2H,
                    Weapon.WeaponType.Mace_1H,
                    Weapon.WeaponType.Mace_2H,
                    Weapon.WeaponType.Halberd_2H,
                    Weapon.WeaponType.Spear_2H
                },

                Cooldown = 3,
                StaminaCost = 4,
                ManaCost = 0,
                HealthCost = 0,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            var hitEffects = skill.gameObject.transform.FindInAllChildren("HitEffects");
            UnityEngine.Object.Destroy(hitEffects.gameObject);

            return skill;
        }
    }
}
