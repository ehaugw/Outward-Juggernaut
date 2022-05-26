using InstanceIDs;
using SideLoader;
using System.Collections.Generic;
using CustomWeaponBehaviour;
using TinyHelper;

namespace Juggernaut
{
    public class TackleSpell
    {
        public static Skill Init()
        {
            var myitem = new SL_AttackSkill()
            {
                Name = "Tackle",
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.daggerSlashID,
                New_ItemID = IDs.tackleID,
                SLPackName = "Juggernaut",
                SubfolderName = "Tackle",
                Description = "Ram into your opponent! Either of you will fall. The most stable at foot will triump!",
                CastType = Character.SpellCastType.ShieldCharge,
                CastModifier = Character.SpellCastModifier.Attack,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1,
                CastSheatheRequired = 0,
                //RequiredWeaponTypes = new List<Weapon.WeaponType>() { Weapon.WeaponType.Axe_1H, Weapon.WeaponType.Axe_2H, Weapon.WeaponType.Sword_1H, Weapon.WeaponType.Sword_2H, Weapon.WeaponType.Mace_1H, Weapon.WeaponType.Mace_2H, Weapon.WeaponType.Halberd_2H, Weapon.WeaponType.Spear_2H },
                
                RequiredOffHandTypes = new Weapon.WeaponType[] { },
                Cooldown = 30,
                StaminaCost = 8,
                ManaCost = 0,
                HealthCost = 0,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            EmptyOffHandCondition.AddToSkill(skill, false, false);




            //-------- ACTIVATION ----------//

            var activationEffects = skill.transform.Find("ActivationEffects").gameObject;
            for (int i = 0; i < 4; i++)
            {
                var hitenabler = activationEffects.AddComponent<EnableHitDetection>();
                hitenabler.Delay = 0.2f + i * 0.08f;
            }
            //var detector = skill.gameObject.GetComponentInChildren<MeleeHitDetector>();
            //detector.Radius = 1f;

            ////Sounds
            //var activationEffects = skill.gameObject.transform.FindInAllChildren("ActivationEffects").gameObject;
            foreach (var sound in activationEffects.GetComponentsInChildren<PlaySoundEffect>())
            {
                UnityEngine.Object.Destroy(sound);
            }
            var shieldCharge = ResourcesPrefabManager.Instance.GetItemPrefab(IDs.shieldChargeID).gameObject;
            foreach (var sound in shieldCharge.gameObject.GetComponentsInChildren<PlaySoundEffect>())
            {
                if (sound.Delay <= 0.2)
                {
                    var newSound = activationEffects.AddComponent<PlaySoundEffect>();
                    newSound.Sounds = sound.Sounds;
                    newSound.Delay = sound.Delay;
                    newSound.Follow = sound.Follow;
                    newSound.MinPitch = sound.MinPitch;
                    newSound.MaxPitch = sound.MaxPitch;
                    newSound.SyncType = sound.SyncType;
                }
            }

            //-------- HIT ---------//
            var hitEffects = skill.gameObject.transform.FindInAllChildren("HitEffects").gameObject;
            var tackleEffect = hitEffects.AddComponent<TackleEffect>();
            tackleEffect.Knockback = 10;
            tackleEffect.Damages = new DamageType[] { new DamageType(DamageType.Types.Physical, 3) };

            var pickPunctualDamage = hitEffects.GetComponent<PunctualDamage>();
            UnityEngine.Object.Destroy(pickPunctualDamage);


            return skill;
        }
    }
}
