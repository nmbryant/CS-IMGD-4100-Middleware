using UnityEngine;
using System.Collections;
using RAIN.Core;

public class Miner : MonoBehaviour {

    private Location miner_Loc = Location.Mine; // The Miner's location.
    private State miner_State = State.EnterMineAndDig; // THe Miner's current state.
    private int thirst = 0;
    private int fatigue = 0;
    private int gold = 0;
    private int wealth = 0;
    private int comfortLevel = 6; // The Miner's comfort level. When his wealth surpasses this, he can go home.
    private int thirstThreshold = 6; // The Miner's thirst threshold, when it exceeds this he will go to the saloon.
    private int goldThreshold = 5; // The Miner's gold threshold, when the amount of gold in his pockets exceeds this he will go to the bank.

    private AIRig minerRig;

    public enum Location { Saloon, Bank, Mine, Home }
    public enum State { EnterMineAndDig, VisitBank, GoHomeAndSleep, QuenchThirst }

	// Use this for initialization
	void Awake () {
        minerRig = GetComponentInChildren<AIRig>();
        // Set the miner's state and behavior to be accessible in the behavior tree
        minerRig.AI.WorkingMemory.SetItem<int>("MinerState", 0);
        //minerRig.AI.WorkingMemory.SetItem<Miner.Location>("MinerLocation", Location.Mine);
        // Set the miner's thresholds to be visible in the behavior tree
        minerRig.AI.WorkingMemory.SetItem<int>("ComfortLevel", comfortLevel);
        minerRig.AI.WorkingMemory.SetItem<int>("ThirstThreshold", thirstThreshold);
        minerRig.AI.WorkingMemory.SetItem<int>("GoldThreshold", goldThreshold);
        // Set the initial values of the miner's variables to be seen in the behavior tree
        minerRig.AI.WorkingMemory.SetItem<int>("Fatigue", fatigue);
        minerRig.AI.WorkingMemory.SetItem<int>("Gold", gold);
        minerRig.AI.WorkingMemory.SetItem<int>("Thirst", thirst);
        minerRig.AI.WorkingMemory.SetItem<int>("Wealth", wealth);


        Debug.Log(
          ", GoldMAX " + minerRig.AI.WorkingMemory.GetItem<int>("GoldThreshold") +
          ", ThirstMAX " + minerRig.AI.WorkingMemory.GetItem<int>("ThirstThreshold") +
          ", WealthMAX " + minerRig.AI.WorkingMemory.GetItem<int>("ComfortLevel"));

	    StartCoroutine(Thinker());
	}

    IEnumerator Thinker()
    {
        while (true)
        {
            Think();
            yield return new WaitForSeconds(0.5f);
        }
    }

	// Update is called once per frame
	void Think () {
	    // Depending on the state, process it.
        //switch(miner_State)
        switch(minerRig.AI.WorkingMemory.GetItem<int>("MinerState"))
        {
            case 0:
                {
                    Fatigue += 1;
                    Thirst += 1;
                    Gold += 1;
                    minerRig.AI.WorkingMemory.SetItem<int>("Fatigue", Fatigue);
                    minerRig.AI.WorkingMemory.SetItem<int>("Gold", Gold);
                    minerRig.AI.WorkingMemory.SetItem<int>("Thirst", Thirst);
                    break;
                }
            case 1:
                {
                    // Bank logic moved to ChangeStateVisitBank, as its one-shot execution works better there.
                    break;
                }
            case 3:
                {
                    Thirst -= 1;
                    minerRig.AI.WorkingMemory.SetItem<int>("Thirst", Thirst);
                    break;
                }
            case 2:
                {
                    Fatigue -= 1;
                    minerRig.AI.WorkingMemory.SetItem<int>("Fatigue", Fatigue);
                    break;
                }
            default:
                break;
        }
        //Debug.Log("Miner State: " + MinerState + ", Fatigue " + Fatigue + ", Gold " + Gold + ", Thirst " + Thirst + ", Wealth " + Wealth);
        Debug.Log("Miner State: " + minerRig.AI.WorkingMemory.GetItem<int>("MinerState") + 
                  ", Fatigue " + minerRig.AI.WorkingMemory.GetItem<int>("Fatigue") + 
                  ", Gold " + minerRig.AI.WorkingMemory.GetItem<int>("Gold") + 
                  ", Thirst " + minerRig.AI.WorkingMemory.GetItem<int>("Thirst") + 
                  ", Wealth " + minerRig.AI.WorkingMemory.GetItem<int>("Wealth"));
    }

    // PROPERTIES
    public State MinerState
    {
        get { return miner_State; }
        set { miner_State = value; }
    }

    public Location MinerLocation
    {
        get { return miner_Loc; }
        set { miner_Loc = value; }
    }

    public int Thirst
    {
        get { return thirst; }
        set { thirst = value; }
    }

    public int Fatigue
    {
        get { return fatigue; }
        set { fatigue = value; }
    }

    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }

    public int Wealth
    {
        get { return wealth; }
        set { wealth = value; }
    }
}