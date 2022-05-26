using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juggernaut
{
    public class Stoicism
    {
        public const int Threshold = 15;
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Stoicism",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.steadyArmID,
                New_ItemID = IDs.stoicismSkillID,
                SLPackName = "Juggernaut",
                SubfolderName = "Stoicism",
                Description = "When you take more than " + Threshold + " direct damage, half of the damage exceeding " + Threshold + " is delayed over " + DelayedDamage.DelayedDamageEffect.LifeSpan + " seconds.",
                IsUsable = false,
                CastType = Character.SpellCastType.NONE,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1f,
            };
            myitem.ApplyTemplate();
            return ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;
        }
    }
}
