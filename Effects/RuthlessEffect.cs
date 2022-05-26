//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Juggernaut
//{
//    using HarmonyLib;
//    using InstanceIDs;
//    using SideLoader;
//    using UnityEngine;

//    class RuthlessEffect : Effect
//    {
//        protected override void ActivateLocally(Character character, object[] _infos)
//        {
//            if (!SkillRequirements.Enraged(character)) {
//                character.StatusEffectMngr.CleanseStatusEffect(Juggernaut.RUTHLESS_EFFECT_NAME);
//                return;
//            }

//            List<String> toRemove = new List<String>();

//            foreach (var effect in character.StatusEffectMngr.Statuses)
//            {
//                if (effect.HasMatch(TagSourceManager.Boon) && !effect.HasMatch(TagSourceManager.Instance.GetTag(IDs.rageTagUID.ToString())))
//                {
//                    toRemove.Add(effect.name);
//                }
//            }

//            foreach (var effectName in toRemove)
//            {
//                character.StatusEffectMngr.CleanseStatusEffect(effectName);
//            }
//        }
//    }

//    //[HarmonyPatch(typeof(CharacterStats), "HasStatusImmunity", new Type[] { typeof(Tag)})]
//    //public class CharacterStats_HasStatusImmunity
//    //{
//    //    [HarmonyPostfix]
//    //    public static void Postfix(CharacterStats __instance, ref Tag _tag, ref bool __result)
//    //    {
//    //        if (At.GetValue(typeof(CharacterStats), __instance, "m_character") is Character character)
//    //        {
//    //            if (SkillRequirements.Enraged(character))
//    //            {
//    //                if (_tag.UID != IDs.rageTagUID.ToString() && _tag.ParentUID != null && _tag.ParentUID == IDs.boonTagUID.ToString())
//    //                    __result = true;
//    //            }
//    //        }
//    //    }
//    //}

//    //[HarmonyPatch(typeof(StatusEffect), "StatusName", MethodType.Getter)]
//    //public class StatusEffect_StatusName
//    //{
//    //    [HarmonyPrefix]
//    //    public static bool Prefix(StatusEffect __instance, ref string __result)
//    //    {
//    //        if (__instance == null)
//    //        {
//    //            __result = "NULL_RATHER_THAN_CRASH";
//    //            return false;
//    //        }
//    //        return true;

//    //    }
//    //}
//}