using TinyHelper;

namespace Juggernaut
{
    using UnityEngine;
    using SideLoader;
    using HarmonyLib;
    using BepInEx;
    using CustomWeaponBehaviour;
    using DelayedDamage;
    using System.IO;

    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency("com.sinai.SideLoader", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(TinyHelper.TinyHelper.GUID, TinyHelper.TinyHelper.VERSION)]
    [BepInDependency(DelayedDamage.GUID, DelayedDamage.VERSION)]
    [BepInDependency(CustomWeaponBehaviour.GUID, CustomWeaponBehaviour.VERSION)]
    [BepInDependency(SynchronizedWorldObjects.SynchronizedWorldObjects.GUID, SynchronizedWorldObjects.SynchronizedWorldObjects.VERSION)]

    public class Juggernaut : BaseUnityPlugin
    {
        public const string GUID = "com.ehaugw.juggernautclass";
        public const string VERSION = "4.1.6";
        public const string NAME = "Juggernaut Class";
        public static string ModFolderName = Directory.GetParent(typeof(Juggernaut).Assembly.Location).Name.ToString();


        public Skill bastardInstance;
        public Skill parryInstance;
        public Skill tackleInstance;
        public Skill relentlessInstance;
        public Skill unyieldingInstance;
        public Skill vengefulInstance;
        public Skill fortifiedInstance;
        public Skill ruthlessInstance;
        public Skill juggernaughtyInstance;
        public Skill hordeBreakerInstance;
        public Skill warCryInstance;
        public Skill stoicismInstance;
        public Skill cullInstance;

        public static SkillSchool juggernautTreeInstance;

        //private SideLoader.SLPack package;

        internal void Awake()
        {
            var rpcGameObject = new GameObject("JuggernautRPC");
            DontDestroyOnLoad(rpcGameObject);
            rpcGameObject.AddComponent<RPCManager>();

            SL.OnPacksLoaded += OnPackLoaded;

            CustomWeaponBehaviour.IBastardModifiers.Add(new BastardSkillModifier());

            JuggernautNPC.Init();

            var harmony = new Harmony(GUID);
            harmony.PatchAll();

            DelayedDamage.GetDamageToDelayList.Add(StoicismDelayedDamageSourceGetDelayedDamage);
            DelayedDamage.GetDamageToDelayList.Add(UnyieldingDelayedDamageSource);
        }

        public Trainer juggernautTrainer;

        private float StoicismDelayedDamageSourceGetDelayedDamage(Character character, Character dealer, DamageList damageList, float knockBack)
        {
            return TinyHelper.SkillRequirements.SafeHasSkillKnowledge(character, InstanceIDs.IDs.stoicismSkillID) ? Mathf.Max(0, ((damageList?.TotalDamage ?? 0) - 20) / 2) : 0;
        }

        private float UnyieldingDelayedDamageSource(Character character, Character dealer, DamageList damageList, float knockBack)
        {
            if (damageList?.Contains(DamageType.Types.Physical) ?? false) {
                DamageType physicalDamage;
                damageList.TryGet(DamageType.Types.Physical, out physicalDamage);
                return SkillRequirements.CanDelayDamageEqualToProtection(character) ? Mathf.Min(character?.Stats?.GetDamageProtection(DamageType.Types.Physical) ?? 0, physicalDamage?.Damage ?? 0) : 0;
            }
            return 0;
        }

        private void OnPackLoaded()
        {
            
            parryInstance = ParrySpell.Init();
            tackleInstance = TackleSpell.Init();
            bastardInstance = BastardSpell.Init();
            relentlessInstance = RelentlessSkill.Init();
            unyieldingInstance = UnyieldingSpell.Init();
            vengefulInstance = VengefulSpell.Init();
            fortifiedInstance = FortifiedSpell.Init();
            ruthlessInstance = RuthlessSpell.Init();
            juggernaughtyInstance = JuggernaughtySpell.Init();
            hordeBreakerInstance = HordeBreakerSpell.Init();
            warCryInstance = WarCrySpell.Init();
            stoicismInstance = Stoicism.Init();
            cullInstance = Cull.Init();

            JuggernautSkillTree.SetupSkillTree(ref juggernautTreeInstance);
        }
    }
}
