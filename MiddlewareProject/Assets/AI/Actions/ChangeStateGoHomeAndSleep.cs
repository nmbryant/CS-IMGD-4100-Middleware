using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class ChangeStateGoHomeAndSleep : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        Miner theMiner = ai.Body.GetComponent<Miner>();
        theMiner.MinerLocation = Miner.Location.Home;
        theMiner.MinerState = Miner.State.GoHomeAndSleep;
        AIRig minerRig = theMiner.GetComponentInChildren<AIRig>();
        minerRig.AI.WorkingMemory.SetItem<int>("MinerState", 2);
        //minerRig.AI.WorkingMemory.SetItem<Miner.Location>("MinerLocation", Miner.Location.Home);
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}