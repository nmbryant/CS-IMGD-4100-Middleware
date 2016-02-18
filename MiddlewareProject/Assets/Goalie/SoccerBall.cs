using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Serialization;

public class SoccerBall : MonoBehaviour {

    private AIRig m_soccerBallRig;

    private Vector3 m_targetPosition;

    void Start()
    {
        m_soccerBallRig = GetComponent<AIRig>();
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            _GetMovementLoc();
        }
    }

    private void _GetMovementLoc()
    {
        // Get the position of the click in the game world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.black, 10000);
        Plane xy = new Plane(Vector3.up, new Vector3(0, 0, 0));
        float point;
        xy.Raycast(ray, out point);
        m_targetPosition = ray.GetPoint(point);
        m_targetPosition.y = transform.position.y;

        if (m_targetPosition.z < -10.07f)
        {
            m_targetPosition.z = -10.07f;
        }
        else if (m_targetPosition.z > 10.07f)
        {
            m_targetPosition.z = 10.07f;
        }

        if (m_targetPosition.x > 4.57f)
        {
            m_targetPosition.x = 4.57f;
        }
        m_soccerBallRig.AI.WorkingMemory.SetItem<Vector3>("MovePos", m_targetPosition);
    }
}
