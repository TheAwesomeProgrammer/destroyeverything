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

    private Pathfinding mPathfinding;

    private Vector3 mEndPos;
    private Vector3 mStartPos;

	// Use this for initialization
	void Start () {
        mWayPointPostion = Vector3.zero;
        mPathfinding = transform.parent.FindChild("Pathfinding").GetComponent<Pathfinding>();
	}
	
	// Update is called once per frame
	void Update () {

        if (mGameobjectToMove != null)
        {
            ReachedWayPoint();
        }


        if (mMoveToWayPoint)
        {
            FollowWayPoint();
        }
    
      
	}

    public void MoveGameobjectViaList(GameObject pGameObjectToMove,Vector3 pStartPos,Vector3 pEndPos,float pSpeed)
    {
        Speed = pSpeed;
        mMoveToWayPoint = true;
        mGameobjectToMove = pGameObjectToMove;
        mStartPos = pStartPos;
        mEndPos = pEndPos;
        print("ENDPOS " + mEndPos);
        FindList();
    }

    public void Stop()
    {
        //mGameobjectToMove.SendMessage("Stopped");
        mGameobjectToMove = null;
        mMoveToWayPoint = false;
    }



    void SetListToFollow(List<Node> pListToFollow)
    {
        mListToFollow = pListToFollow;
        mWaypointNumber = mListToFollow.Count - 1;
        SetWaypoint();
    }

    void FindList()
    {

            mPathfinding.InitFastestRoad(mStartPos, mEndPos, mGameobjectToMove);
     
    }

    void SetWaypoint()
    {
        if (mWaypointNumber > 0)
        {
            mWaypointNumber -= 1;
            mWayPointPostion = mListToFollow.ToArray()[mWaypointNumber].Position;
            mWayPointPostion.y = mGameobjectToMove.transform.position.y;
        }
 
    }

    void FollowWayPoint()
    {
        mGameobjectToMove.transform.Translate((mWayPointPostion - mGameobjectToMove.transform.position).normalized * Speed * Time.deltaTime);
    }

    void ReachedWayPoint()
    {
        if (Vector3.Distance(mGameobjectToMove.transform.position, mWayPointPostion) > WaypointDistance)
        {
           SetWaypoint();
        }
        else if (mWaypointNumber <= 0 && Vector3.Distance(mGameobjectToMove.transform.position, mWayPointPostion) < WaypointDistance)
        {
            mGameobjectToMove.SendMessage("Moved");
            mGameobjectToMove = null;
            mMoveToWayPoint = false;
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
