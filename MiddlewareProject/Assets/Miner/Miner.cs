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
    [SerializeField]
    private int comfortLevel; // The Miner's comfort level. When his wealth surpasses this, he can go home.
    [SerializeField]
    private int thirstThreshold; // The Miner's thirst threshold, when it exceeds this he will go to the saloon.
    [SerializeField]
    private int goldThreshold; // The Miner's gold threshold, when the amount of gold in his pockets exceeds this he will go to the bank.

    private AIRig minerRig;

    public enum Location { Saloon, Bank, Mine, Home }
    public enum State { EnterMineAndDig, VisitBank, GoHomeAndSleep, QuenchThirst }

	// Use this for initialization
	void Awake () {
        minerRig = GetComponentInChildren<AIRig>();
	}

	// Update is called once per frame
	void Update () {
	    // Depending on the state, process it.
        switch(miner_State)
        {
            case State.EnterMineAndDig:
                {
                    Fatigue += 1;
                    Thirst += 1;
                    Gold += 1;
                    break;
                }
            case State.VisitBank:
                {
                    Wealth += Gold;
                    Gold = 0;
                    break;
                }
            case State.QuenchThirst:
                {
                    Thirst -= 1;
                    break;
                }
            case State.GoHomeAndSleep:
                {
                    Fatigue -= 1;
                    break;
                }
            default:
                break;
        }
        Debug.Log("Miner State: " + MinerState + ", Fatigue " + Fatigue + ", Gold " + Gold + ", Thirst " + Thirst + ", Wealth " + Wealth);
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