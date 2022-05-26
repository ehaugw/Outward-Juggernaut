using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juggernaut
{
    public class JuggernaughtySpell
    {
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Juggernaughty",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.pushKickID, //Push Kick
                New_ItemID = IDs.juggernaughtyID,
                Description = "Trainer Hax",
                CastType = Character.SpellCastType.Sit,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = true,
                MobileCastMovementMult = -1f,

                EffectTransforms = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] {
                        }
                    }
                },
                Cooldown = 1,
                StaminaCost = 0,
                ManaCost = 0,
                HealthCost = 0,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            var myEffects = skill.transform.Find("Effects");
            myEffects.gameObject.AddComponent<JuggernaughtyEffect>();

            return skill;
        }
    }
}
