﻿using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Serialization;
using System;

public class Goalie : MonoBehaviour {

    AIRig goalieRig;

    [SerializeField]
    Transform leftPost;

    [SerializeField]
    Transform rightPost;

    [SerializeField]
    Transform pitch;

    [SerializeField]
    Transform leftWall;

    [SerializeField]
    Transform rightWall;

    [SerializeField]
    Transform topWall;

    [SerializeField]
    Transform botWall;

    [SerializeField]
    Transform goalLine;

    [SerializeField]
    float goalieMaxX = 6;

    [SerializeField]
    Transform goalMax;

    SoccerBall soccerBall;
    Vector3 goalCenter;
    bool isTouchingBall;

    void Start()
    {
        goalCenter = (leftPost.position - rightPost.position)/2 + rightPost.position;
        Debug.Log("Goal center z = " + goalCenter.z);
        goalieRig = GetComponentInChildren<AIRig>();
        goalieRig.AI.WorkingMemory.SetItem<Vector3>("GoalCenter", goalCenter);

        soccerBall = FindObjectOfType<SoccerBall>();
        goalieRig.AI.WorkingMemory.SetItem<Vector3>("SoccerBallPos", soccerBall.transform.position);
    }

	// Update is called once per frame
	void Update () {
        goalieRig.AI.WorkingMemory.SetItem<Vector3>("SoccerBallPos", soccerBall.transform.position);
        bool isBlockingCenter = goalieRig.AI.WorkingMemory.GetItem<bool>("IsBlockingCenter");
        _CheckIfTouchingBall();
        _UpdateDistanceToBall();
        _FindInterceptPoint();
        if (isTouchingBall)
        {
            _CheckIfAtHome();
        }
    }

    private void _FindInterceptPoint()
    {
        float pitchHeight = Math.Abs(botWall.position.x - topWall.position.x);
        float pitchWidth = Math.Abs(leftWall.position.z - rightWall.position.z);
        float goalHeight = Math.Abs(leftPost.position.x - goalLine.position.x);
        float goalWidth = Math.Abs(leftPost.position.z - rightPost.position.z);
        Vector3 soccerBallPos = soccerBall.transform.position;
        float relativeBallZPos = soccerBallPos.z / pitchWidth;
        float newGoalieZ = relativeBallZPos * goalWidth;
        float relativeBallXPos = soccerBallPos.x / pitchHeight;
        float newGoalieX = relativeBallXPos * goalHeight;
        Vector3 moveVector = transform.position;
        moveVector.x = newGoalieX;
        moveVector.z = newGoalieZ;
        if (moveVector.x >= leftPost.position.x)
        {
            moveVector.x = leftPost.position.x;
        }
        else if (Math.Abs(moveVector.x - leftPost.position.x) > Math.Abs(goalMax.position.x - leftPost.position.x))
        {
            moveVector.x = goalMax.position.x;
        }
        goalieRig.AI.WorkingMemory.SetItem<Vector3>("MovePos", moveVector);
    }

    private void _UpdateDistanceToBall()
    {
        float distance = Vector3.Distance(transform.position, soccerBall.transform.position);
        if (distance < 3)
        {
            goalieRig.AI.WorkingMemory.SetItem<bool>("IsBlockingCenter", true);
        }
        else
        {
            goalieRig.AI.WorkingMemory.SetItem<bool>("IsBlockingCenter", false);
        }
        goalieRig.AI.WorkingMemory.SetItem<float>("DistanceToBall", distance);
    }

    
    void _CheckIfTouchingBall()
    {
        if (Vector3.Distance(transform.position, soccerBall.transform.position) < 1)
        {
            goalieRig.AI.WorkingMemory.SetItem<bool>("IsTouchingBall", true);
            isTouchingBall = true;
            soccerBall._ReturnToStart();
            goalieRig.AI.WorkingMemory.SetItem<Vector3>("GoalieHome", goalMax.transform.position);
        }
    }

    void _CheckIfAtHome()
    {
        if (Vector3.Distance(transform.position, goalMax.transform.position) <= 2.5)
        {
            isTouchingBall = false;
            goalieRig.AI.WorkingMemory.SetItem<bool>("IsTouchingBall", false);
        }
    }
}
