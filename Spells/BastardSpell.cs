using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juggernaut
{
    public class BastardSpell
    {
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Bastard",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.steadyArmID,
                New_ItemID = IDs.bastardWeaponTrainingID,
                SLPackName = Juggernaut.ModFolderName,
                SubfolderName = "Bastard",
                Description = "Increases the speed and damage bonuses from two-handing a bastard sword.",
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
