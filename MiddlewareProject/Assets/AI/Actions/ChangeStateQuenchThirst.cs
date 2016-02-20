using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class ChangeStateQuenchThirst : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        Miner theMiner = ai.Body.GetComponent<Miner>();
        theMiner.MinerLocation = Miner.Location.Saloon;
        theMiner.MinerState = Miner.State.QuenchThirst;
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}