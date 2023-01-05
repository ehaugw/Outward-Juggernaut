using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Juggernaut
{
    public class UnyieldingSpell
    {
        public const string NAME = "Unyielding";
        public static Skill Init()
        {
            TinyHelper.TinyHelper.OnDescriptionModified += delegate (Item item, ref string description) {
                if (item.ItemID == IDs.unyieldingID)
                {
                    if (TinyHelper.SkillRequirements.SafeHasSkillKnowledge(CharacterManager.Instance.GetFirstLocalCharacter(), IDs.unyieldingID))
                    {
                        description = "A portion of physical damage taken proportional to your Protection is delayed over " + DelayedDamage.DelayedDamageEffect.LifeSpan + " seconds.";
                    }
                }
            };

            var myitem = new SL_Skill()
            {
                Name = NAME,
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.steadyArmID,
                New_ItemID = IDs.unyieldingID,
                SLPackName = Juggernaut.ModFolderName,
                SubfolderName = "Unyielding",
                Description = "A portion of physical damage taken proportional to your Protection is delayed over " + DelayedDamage.DelayedDamageEffect.LifeSpan + " seconds.\n\nBe aware that learning this skill has impact on most other Juggernaut skills!",
                IsUsable = false,
                CastType = Character.SpellCastType.NONE,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1f,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;
            return skill;
        }
    }
}
