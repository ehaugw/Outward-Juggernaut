using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juggernaut
{
    public class Cull
    {
        public const int Threshold = 15;
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Cull",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.steadyArmID,
                New_ItemID = IDs.cullID,
                SLPackName = Juggernaut.ModFolderName,
                SubfolderName = "Cull",
                Description = "Weapon attacks that would reduce a creature to less health than the weapon's base damage cause the creatures to die instead.",
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

    [HarmonyLib.HarmonyPatch(typeof(Character), "OnReceiveHit")]
    public class Character_OnReceiveHit
    {
        [HarmonyPostfix]
        public static void Postfix(Character __instance, Weapon _weapon, DamageList _damageList)
        {
            if (TinyHelper.SkillRequirements.SafeHasSkillKnowledge(_weapon?.OwnerCharacter, IDs.cullID))
            {
                var damageList = _weapon.Damage.Clone();
                damageList.IgnoreHalfResistances = _damageList.IgnoreHalfResistances;
                //_weapon.OwnerCharacter.Stats.GetAmplifiedDamage(null, ref damageList);
                __instance.Stats.GetMitigatedDamage(null, ref damageList, false);

                var cullDamage = damageList.TotalDamage;
                if (__instance.Health < cullDamage) {
                    __instance.Stats.ReceiveDamage(cullDamage);
                }
            }
        }
    }
}
