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
                EffectBehaviour = EditBehaviours.Destroy,
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
            MeleeSkill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as MeleeSkill;
            skill.Blockable = false;

            var meleeHitDetectorTransform = TinyGameObjectManager.GetOrMake(skill.transform, "MeleeHitDetector", true, true).gameObject;
            skill.MeleeHitDetector = meleeHitDetectorTransform.gameObject.AddComponent<MeleeHitDetector>();
            skill.MeleeHitDetector.Radius = 0.3f;
            skill.MeleeHitDetector.LinecastCount = 3;
            skill.MeleeHitDetector.Damage = -1;
            EmptyOffHandCondition.AddToSkill(skill, false, false);
            
            var activationEffects = TinyGameObjectManager.GetOrMake(skill.transform, "ActivationEffects", true, true).gameObject;
            activationEffects.AddComponent<InitTackle>();

            //-------- ACTIVATION ----------//
            for (int i = 0; i < 4; i++)
            {
                var hitenabler = activationEffects.AddComponent<EnableHitDetection>();
                hitenabler.Delay = 0.2f + i * 0.08f;
            }
            
            //var shieldCharge = ResourcesPrefabManager.Instance.GetItemPrefab(IDs.shieldChargeID).gameObject;
            //foreach (var sound in shieldCharge.gameObject.GetComponentsInChildren<PlaySoundEffect>())
            //{
            //    if (sound.Delay <= 0.2)
            //    {
            //        var newSound = activationEffects.AddComponent<PlaySoundEffect>();
            //        newSound.Sounds = sound.Sounds;
            //        newSound.Delay = sound.Delay;
            //        newSound.Follow = sound.Follow;
            //        newSound.MinPitch = sound.MinPitch;
            //        newSound.MaxPitch = sound.MaxPitch;
            //        newSound.SyncType = sound.SyncType;
            //    }
            //}

            //-------- HIT ---------//
            var hitEffects = TinyGameObjectManager.GetOrMake(skill.transform, "HitEffects", true, true).gameObject;
            var tackleEffect = hitEffects.AddComponent<TackleEffect>();
            tackleEffect.Knockback = 10;
            tackleEffect.Damages = new DamageType[] { new DamageType(DamageType.Types.Physical, 3) };

            return skill;
        }
    }
}
