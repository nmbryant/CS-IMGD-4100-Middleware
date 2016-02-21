using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class MinerDisplay : MonoBehaviour
{
    [SerializeField]
    private Miner m_target;

    [SerializeField]
    private Text m_fatigueDisplay;

    [SerializeField]
    private Text m_goldDisplay;

    [SerializeField]
    private Text m_thirstDisplay;

    [SerializeField]
    private Text m_wealthDisplay;

    [SerializeField]
    private Text m_locationDisplay;

    private Animator m_animator;

    void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update ()
	{
	    m_fatigueDisplay.text = string.Format("Fatigue: {0:D}", m_target.Fatigue);
        m_goldDisplay.text = string.Format("Gold: {0:D}", m_target.Gold);
        m_thirstDisplay.text = string.Format("Thirst: {0:D}", m_target.Thirst);
        m_wealthDisplay.text = string.Format("Wealth: {0:D}", m_target.Wealth);
	    m_locationDisplay.text = string.Format("Miner is at {0}", m_target.MinerLocation);

        m_animator.SetFloat("Blend", (float)m_target.MinerLocation);
	}
}
