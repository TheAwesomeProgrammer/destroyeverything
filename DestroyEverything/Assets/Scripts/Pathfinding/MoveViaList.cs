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
        if (mMoveToWayPoint)
        {
            FollowWayPoint();
        }
	}

    public void MoveGameobjectViaList(GameObject pGameObjectToMove,List<Node> pListToFollow)
    {
        mGameobjectToMove = pGameObjectToMove;
        mListToFollow = pListToFollow;
        mWaypointNumber = mListToFollow.Count - 1;
        mMoveToWayPoint = true;
    }

    

    void SetWaypoint()
    {
        if (mWaypointNumber > 0)
        {
            mWaypointNumber -= 1;
            mWayPointPostion = mListToFollow.ToArray()[mWaypointNumber].Position;
            mWayPointPostion.y = transform.position.y;
        }
        else
        {
            mMoveToWayPoint = false;
        }
    }

    void FollowWayPoint()
    {
        //currentPathPercent -= percentsPerSecond * Time.deltaTime;
        //  iTween.PutOnPath(gameObject, mVector3sToFollow.ToArray(), currentPathPercent);
        transform.Translate((mWayPointPostion - transform.position).normalized * Speed * Time.deltaTime);
    }

    void ReachedWayPoint()
    {
        if (Vector3.Distance(transform.position, mWayPointPostion) < WaypointDistance)
        {
            Destroy(mWayPoint);
            SetWaypoint();
        }
    }
}
