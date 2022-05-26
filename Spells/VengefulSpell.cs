using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyHelper;
using UnityEngine;

namespace Juggernaut
{
    public class VengefulSpell
    {
        public const string NAME = "Vengeful";
        public static Skill Init()
        {
            TinyHelper.TinyHelper.OnDescriptionModified += delegate (Item item, ref string description) {
                if (item.ItemID == IDs.vengefulID)
                {
                    if (TinyHelper.SkillRequirements.SafeHasSkillKnowledge(CharacterManager.Instance.GetFirstLocalCharacter(), IDs.vengefulID))
                    {
                        description = "Being damaged causes rage to build up.";
                    }
                }
            };

            var myitem = new SL_Skill()
            {
                Name = NAME,
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.steadyArmID,
                New_ItemID = IDs.vengefulID,
                SLPackName = "Juggernaut",
                SubfolderName = "Vengeful",
                Description = "Being damaged causes rage to build up.\n\nBe aware that learning this skill has impact on most other Juggernaut skills!",
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

    [HarmonyPatch(typeof(Character), "ReceiveHit", new Type[] { typeof(UnityEngine.Object), typeof(DamageList), typeof(Vector3), typeof(Vector3), typeof(float), typeof(float), typeof(Character), typeof(float), typeof(bool) })]
    public class Character_ReceiveHit
    {
        [HarmonyPostfix]
        public static void Postfix(Character __instance, ref DamageList __result, UnityEngine.Object _damageSource, DamageList _damage, Vector3 _hitDir, Vector3 _hitPoint, float _angle, float _angleDir, Character _dealerChar, float _knockBack, bool _hitInventory)
        {
            if (SkillRequirements.CanEnrageFromDamage(__instance) && __result.TotalDamage > 0)
            {
                __instance.StatusEffectMngr.AddStatusEffectBuildUp(ResourcesPrefabManager.Instance.GetStatusEffectPrefab("Rage"), _damage.TotalDamage, __instance);
            }
            Character ownerCharacter =
                (_damageSource as Item)?.OwnerCharacter ??
                (_damageSource as Effect)?.OwnerCharacter ??
                (_damageSource as EffectSynchronizer)?.OwnerCharacter;

            if (SkillRequirements.CanTerrify(ownerCharacter) && __result.TotalDamage > 0)
            {
                List<Character> charsInRange = new List<Character>();
                CharacterManager.Instance.FindCharactersInRange(__instance.CenterPosition, RuthlessSpell.Range, ref charsInRange);
                charsInRange = charsInRange.Where(c => !c.IsAlly(ownerCharacter)).ToList();
                foreach (Character character in charsInRange)
                {
                    if (character.StatusEffectMngr is StatusEffectManager manager)
                    {
                        bool wasConfused = manager.HasStatusEffect("Confusion");
                        manager.AddStatusEffectBuildUp(ResourcesPrefabManager.Instance.GetStatusEffectPrefab("Confusion"), Mathf.Clamp(__result.TotalDamage, 0, 40), character);
                        if (!wasConfused && manager.HasStatusEffect("Confusion"))
                        {
                            CasualStagger.Stagger(character);
                        }
                    }

                }
            }
        }
    }
}
