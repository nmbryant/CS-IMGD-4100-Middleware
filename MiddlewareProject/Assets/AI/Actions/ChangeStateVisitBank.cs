using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class ChangeStateVisitBank : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        Miner theMiner = ai.Body.GetComponent<Miner>();
        theMiner.MinerLocation = Miner.Location.Bank;
        theMiner.MinerState = Miner.State.VisitBank;
        AIRig minerRig = theMiner.GetComponentInChildren<AIRig>();
        minerRig.AI.WorkingMemory.SetItem<int>("MinerState", 1);
        theMiner.Wealth += theMiner.Gold;
        theMiner.Gold = 0;
        minerRig.AI.WorkingMemory.SetItem<int>("Wealth", theMiner.Wealth);
        minerRig.AI.WorkingMemory.SetItem<int>("Gold", theMiner.Gold);
        //minerRig.AI.WorkingMemory.SetItem<Miner.Location>("MinerLocation", Miner.Location.Bank);
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}