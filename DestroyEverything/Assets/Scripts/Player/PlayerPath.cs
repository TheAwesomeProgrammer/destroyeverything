using UnityEngine;
using System.Collections.Generic;

public class PlayerPath : MonoBehaviour
{
    public GameObject Waypoint;

    public float Speed = 5;
    public float WaypointDistance = 1;

    public bool cSelected { get; set; }

    private bool mMoveToWayPoint;

    private Vector3 mWayPointPostion;

    private GameObject mWayPoint;

    private Pathfinding mPathfinding;

    private List<Node> mListToFollow;

    private int mWaypointNumber;

    

	// Use this for initialization
	void Start ()
	{
	    mPathfinding = GameObject.Find("Pathfinding").GetComponent<Pathfinding>();
	    mWayPointPostion = Vector3.zero;
	    cSelected = false;
	}
	
	// Update is called once per frame
	void Update () {
    ReachedWayPoint();

   

	if(Input.GetMouseButtonDown(0) && cSelected)
	{
	    MakeWayPoint();
	}

    if(mMoveToWayPoint)
    {
       FollowWayPoint();
    }

	}

    void ReachedWayPoint()
    {
        if(Vector3.Distance(transform.position,mWayPointPostion) < WaypointDistance)
        {
            Destroy(mWayPoint);
            SetWaypoint();
        }
    }

    void MakeWayPoint()
    {
        if(mMoveToWayPoint)
        {
            Destroy(mWayPoint);
        }
        mMoveToWayPoint = true;
        
        mListToFollow = mPathfinding.FindFastestRoadToPoint(transform.position,Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mWaypointNumber =  mListToFollow.Count-1;
        
        SetWaypoint();
        foreach (var tNode in mListToFollow)
        {
           // mWayPoint = Instantiate(Waypoint, tNode.Position, Quaternion.identity) as GameObject;
        }
    }

    void SetWaypoint()
    {
        if(mWaypointNumber - 1 > 0)
        {
            mWaypointNumber-= 1;
            mWayPointPostion = mListToFollow.ToArray()[mWaypointNumber].Position;
           // mWayPoint = Instantiate(Waypoint, mWayPointPostion, Quaternion.identity) as GameObject;
        }
        else 
        {
            mMoveToWayPoint = false;
        }
    }

    void FollowWayPoint()
    {
        transform.LookAt(mWayPointPostion);
        transform.Translate(Vector3.forward  * Speed * Time.deltaTime);
    }

    public void SetSelected(bool pIsSelected)
    {
        cSelected = pIsSelected;
    }

    void OnMouseDown()
    {
        cSelected = true;
        foreach (var tPlayer in GameObject.FindGameObjectsWithTag("Player"))
        {
           if(tPlayer.gameObject != transform.gameObject)
           {
               tPlayer.SendMessage("SetSelected",false);
           }
        }
        
    }
}
