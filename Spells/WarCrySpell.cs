using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juggernaut
{
    public class WarCrySpell
    {
        public static Skill Init()
        {
            TinyHelper.TinyHelper.OnDescriptionModified += delegate (Item item, ref string description) {
                if (item.ItemID == IDs.warCryID)
                {
                    if (TinyHelper.SkillRequirements.SafeHasSkillKnowledge(CharacterManager.Instance.GetFirstLocalCharacter(), IDs.unyieldingID))
                    {
                        description = "Unleash a terrifying roar that staggers nearby enemies. Applies confusion to affected enemies.";
                    }
                    else if (TinyHelper.SkillRequirements.SafeHasSkillKnowledge(CharacterManager.Instance.GetFirstLocalCharacter(), IDs.vengefulID))
                    {
                        description = "Unleash a terrifying roar that staggers nearby enemies. Applies pain to affected enemies.";
                    }
                }
            };


            var myitem = new SL_Skill()
            {
                Name = "War Cry",
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.enrageID,
                New_ItemID = IDs.warCryID,
                SLPackName = Juggernaut.ModFolderName,
                SubfolderName = "WarCry",
                Description = String.Format("Unleash a terrifying roar that staggers and gains the attention of nearby enemies.\n\n{0}: Applies confusion to affected enemies.\n\n{1}: Applies pain to affected enemies.", UnyieldingSpell.NAME, VengefulSpell.NAME),
                CastType = Character.SpellCastType.Rage,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = 0f,

                EffectTransforms = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] {/*slow down, cripple, ele vulnerability and dizzy*/}
                    }
                },

                Cooldown = 100,
                StaminaCost = 4,
                ManaCost = 0,
                HealthCost = 0,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;
            skill.transform.Find("Effects").gameObject.AddComponent<WarCryEffect>();
            return skill;
        }
    }
}
