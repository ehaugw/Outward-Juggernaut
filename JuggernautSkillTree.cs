using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Juggernaut
{
    public class JuggernautSkillTree
    {
        public static void SetupSkillTree(ref SkillSchool juggernautTreeInstance)
        {
            var myskilltree = new SL_SkillTree()
            {
                Name = "Juggernaut",
                SkillRows = new List<SL_SkillRow>()
                {
                    new SL_SkillRow()
                    {

                        
                        RowIndex = 1,
                        
                        Slots = new List<SL_BaseSkillSlot>()
                        {

                            new SL_SkillSlotFork()
                            {
                                ColumnIndex = 2,
                                RequiredSkillSlot = Vector2.zero,
                                Choice1 = new SL_SkillSlot()
                                {
                                    ColumnIndex = 2,
                                    SilverCost = 50,
                                    SkillID = IDs.vengefulID,
                                    RequiredSkillSlot = Vector2.zero,
                                    Breakthrough = false
                                },
                                Choice2 = new SL_SkillSlot()
                                {
                                    ColumnIndex = 2,
                                    SilverCost = 50,
                                    SkillID = IDs.unyieldingID,
                                    RequiredSkillSlot = Vector2.zero,
                                    Breakthrough = false
                                }
                            },
                        }
                    },

                    new SL_SkillRow()
                    {
                        RowIndex = 2, Slots = new List<SL_BaseSkillSlot>()
                        {
                            new SL_SkillSlot() { ColumnIndex = 1, SilverCost = 100, SkillID = IDs.tackleID,    Breakthrough = false, RequiredSkillSlot = Vector2.zero},
                        }
                    },


                    new SL_SkillRow()
                    {
                        RowIndex = 3,
                        Slots = new List<SL_BaseSkillSlot>()
                        {
                            new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 500, SkillID = IDs.ruthlessID,  Breakthrough = true, RequiredSkillSlot = new Vector2(1,2)},
                        }
                    },

                    new SL_SkillRow()
                    {
                        RowIndex = 4, Slots = new List<SL_BaseSkillSlot>()
                        {
                            new SL_SkillSlot() { ColumnIndex = 1, SilverCost = 600, SkillID = IDs.fortifiedID, Breakthrough = false, RequiredSkillSlot = new Vector2(3, 2)},
                            new SL_SkillSlot() { ColumnIndex = 3, SilverCost = 600, SkillID = IDs.stoicismSkillID, Breakthrough = false, RequiredSkillSlot = new Vector2(3,2)},
                        }
                    },

                    new SL_SkillRow()
                    {


                        RowIndex = 5,

                        Slots = new List<SL_BaseSkillSlot>()
                        {

                            new SL_SkillSlotFork()
                            {
                                ColumnIndex = 2,
                                RequiredSkillSlot = new Vector2(3,2),
                                Choice1 = new SL_SkillSlot()
                                {
                                    ColumnIndex = 2,
                                    SilverCost = 600,
                                    SkillID = IDs.warCryID,
                                    RequiredSkillSlot = new Vector2(3,2),
                                    Breakthrough = false
                                },
                                Choice2 = new SL_SkillSlot()
                                {
                                    ColumnIndex = 2,
                                    SilverCost = 600,
                                    SkillID = IDs.hordeBreakerID,
                                    RequiredSkillSlot = new Vector2(3,2),
                                    Breakthrough = false
                                }
                            },
                        }
                    }
                    //,

                    //new SL_SkillRow()
                    //{
                    //    RowIndex = 5, Slots = new List<SL_BaseSkillSlot>()
                    //    {
                    //        new SL_SkillSlot() { ColumnIndex = 1, SilverCost = 600, SkillID = IDs.warCryID, Breakthrough = false, RequiredSkillSlot = new Vector2(4, 1)},
                    //        new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 600, SkillID = IDs.hordeBreakerID, Breakthrough = false, RequiredSkillSlot = new Vector2(4, 1)},
                    //    }
                    //},
                }
            };

            juggernautTreeInstance = myskilltree.CreateBaseSchool();
            myskilltree.ApplyRows();
        }

    }
}
