using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juggernaut
{
    public class RelentlessSkill
    {
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Relentless",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.steadyArmID,
                New_ItemID = IDs.relentlessID,
                SLPackName = Juggernaut.ModFolderName,
                SubfolderName = "Relentless",
                Description = "Reduces armor movement penalties and gives bonus physical resistance equal to your armor protection.",
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
