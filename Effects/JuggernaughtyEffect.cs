namespace Juggernaut
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using SideLoader;

    class JuggernaughtyEffect : Effect
    {

        public static void StaticActivate(Character character, object[] _infos, Effect instance)
        {
            var trainer = new Trainer();
            At.SetField<Trainer>(trainer, "m_uid", UID.Generate());
            At.SetField<Trainer>(trainer, "m_skillTreeUID", Juggernaut.juggernautTreeInstance.UID);
            trainer.StartTraining(character);
        }

        protected override void ActivateLocally(Character character, object[] _infos)
        {
            StaticActivate(character, _infos, this);
        }
    }
}
