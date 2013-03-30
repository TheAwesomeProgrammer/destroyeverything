using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MoveViaList : MonoBehaviour {

    public float Speed = 5;
    public float WaypointDistance = 1;
    

   private bool mMoveToWayPoint;

    private Vector3 mWayPointPostion;

    private GameObject mWayPoint;

    private List<Node> mListToFollow;

   private int mWaypointNumber;

    private GameObject mGameobjectToMove;

	// Use this for initialization
	void Start () {
        mWayPointPostion = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {

        if(mGameobjectToMove != null)
        {
            ReachedWayPoint();
        }
        

        if (mMoveToWayPoint)
        {
            FollowWayPoint();
        }
	}

    public void MoveGameobjectViaList(GameObject pGameObjectToMove,List<Node> pListToFollow,float pSpeed)
    {
        Speed = pSpeed;
        mMoveToWayPoint = true;
        mGameobjectToMove = pGameObjectToMove;
        mListToFollow = pListToFollow;
        mWaypointNumber = mListToFollow.Count - 1;
        SetWaypoint();
        
    }

    public void Stop()
    {
        //mGameobjectToMove.SendMessage("Stopped");
        mGameobjectToMove = null;
        mMoveToWayPoint = false;
    }
    

    void SetWaypoint()
    {
        if (mWaypointNumber > 0)
        {
            mWaypointNumber -= 1;
            mWayPointPostion = mListToFollow.ToArray()[mWaypointNumber].Position;
            mWayPointPostion.y = mGameobjectToMove.transform.position.y;
        }
        else
        {
            mGameobjectToMove.SendMessage("Moved");
            mGameobjectToMove = null;
            mMoveToWayPoint = false;
        }
    }

    void FollowWayPoint()
    {
        mGameobjectToMove.transform.Translate((mWayPointPostion - mGameobjectToMove.transform.position).normalized * Speed * Time.deltaTime);
    }

    void ReachedWayPoint()
    {
        if (Vector3.Distance(mGameobjectToMove.transform.position, mWayPointPostion) < WaypointDistance)
        {
           SetWaypoint();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (mListToFollow != null)
        {

            foreach (var tNode in mListToFollow)
            {
                Gizmos.DrawCube(tNode.Position, new Vector3(0.25f, 0.25f, 0.25f));
            }
        }
    }
}
