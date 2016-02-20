using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class ChangeStateEnterMineAndDig : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        Miner theMiner = ai.Body.GetComponent<Miner>();
        theMiner.MinerLocation = Miner.Location.Mine;
        theMiner.MinerState = Miner.State.EnterMineAndDig;
        AIRig minerRig = theMiner.GetComponentInChildren<AIRig>();
        minerRig.AI.WorkingMemory.SetItem<int>("MinerState", 0);
        //minerRig.AI.WorkingMemory.SetItem<Miner.Location>("MinerLocation", Miner.Location.Mine);
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}