using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Juggernaut
{
    class InitTackle : Effect
    {
        protected override void ActivateLocally(Character _affectedCharacter, object[] _infos)
        {
            var skill = _affectedCharacter.LastUsedSkill as MeleeSkill; 
            //skill.MeleeHitDetector.LinecastTrans = _affectedCharacter.transform.FindAllInAllChildren("Clavicle_L").First();
            skill.MeleeHitDetector.LinecastTrans = _affectedCharacter.transform.FindAllInAllChildren("hand_left").First();
        }
    }
}
