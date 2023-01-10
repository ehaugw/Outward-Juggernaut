using UnityEngine;
using SideLoader;
using InstanceIDs;

namespace Juggernaut
{
    using SynchronizedWorldObjects;
    using TinyHelper;

    public class JuggernautNPC : SynchronizedNPC
    {
        public static void Init()
        {
            var syncedNPC = new JuggernautNPC(
                identifierName: "The Juggernaut",
                rpcListenerID: IDs.NPCID_Juggernaut,
                defaultEquipment: new int[] { IDs.prayerClaymoreID, IDs.brigandArmorID, IDs.palladiumHelmetID, IDs.blackPlateBootsID },
                scale: new Vector3(1.15f, 1.15f, 1.15f)
            );

            syncedNPC.AddToScene(new SynchronizedNPCScene(
                scene: "Berg",
                position: new Vector3(1207.5f, -13.72215f, 1378.747f),
                rotation: new Vector3(0, 220.5518f, 0),
                sheathed: false
            )) ;
        }

        public JuggernautNPC(string identifierName, int rpcListenerID, int[] defaultEquipment = null, int[] moddedEquipment = null, Vector3? scale = null, Character.Factions? faction = null) :
            base(identifierName, rpcListenerID, defaultEquipment: defaultEquipment, moddedEquipment: moddedEquipment, scale: scale, faction: faction)
        { }

        override public object SetupClientSide(int rpcListenerID, string instanceUID, int sceneViewID, int recursionCount, string rpcMeta)
        {
            Character instanceCharacter = base.SetupClientSide(rpcListenerID, instanceUID, sceneViewID, recursionCount, rpcMeta) as Character;
            if (instanceCharacter == null) return null;

            GameObject instanceGameObject = instanceCharacter.gameObject;
            var trainerTemplate = TinyDialogueManager.AssignTrainerTemplate(instanceGameObject.transform);
            var actor = TinyDialogueManager.SetDialogueActorName(trainerTemplate, IdentifierName);
            var trainerComp = TinyDialogueManager.SetTrainerSkillTree(trainerTemplate, Juggernaut.juggernautTreeInstance.UID);
            var graph = TinyDialogueManager.GetDialogueGraph(trainerTemplate);
            TinyDialogueManager.SetActorReference(graph, actor);

            var openTrainer = TinyDialogueManager.MakeTrainDialogueAction(graph, trainerComp);
            var rootStatement = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "What do you want, peasant?");
            var wantToLeavePrisonStatement = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Hah! Like you don't know... Everyone knows me, I'm a living legend known as \"The Juggernaut\"!");

            var introMultipleChoice = TinyDialogueManager.MakeMultipleChoiceNode(graph, new string[] {
                "I wish to become a legend like you!",
                "Who are you?"
            });

            graph.allNodes.Clear();
            graph.allNodes.Add(rootStatement);
            graph.allNodes.Add(introMultipleChoice);
            graph.allNodes.Add(wantToLeavePrisonStatement);
            graph.allNodes.Add(openTrainer);

            graph.primeNode = rootStatement;
            graph.ConnectNodes(rootStatement, introMultipleChoice);
            graph.ConnectNodes(introMultipleChoice, openTrainer, 0);
            graph.ConnectNodes(introMultipleChoice, wantToLeavePrisonStatement, 1);
            graph.ConnectNodes(wantToLeavePrisonStatement, rootStatement);

            var obj = instanceGameObject.transform.parent.gameObject;
            obj.SetActive(true);

            return instanceCharacter;
        }
    }
}