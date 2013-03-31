using UnityEngine;
using System.Collections.Generic;

public class PlayerPath : MonoBehaviour
{
    public float Speed = 5;
    public float WaypointDistance = 1;
    public float percentsPerSecond = 0.02f; 
    public float currentPathPercent = 0.0f; 

    public bool cSelected { get; set; }

    private bool mMoveToWayPoint;

    private Vector3 mWayPointPostion;

    private GameObject mWayPoint;

    private Pathfinding mPathfinding;

    private List<Node> mListToFollow;

    private List<Vector3> mVector3sToFollow; 

    private int mWaypointNumber;

    

	// Use this for initialization
	void Start ()
	{
        mVector3sToFollow = new List<Vector3>();
        mPathfinding = transform.FindChild("Pathfinding").GetComponent<Pathfinding>();
	    mWayPointPostion = Vector3.zero;
	    cSelected = false;
	}
	
    public GameObject GetSelectedPlayer()
    {
        GameObject tSelectedPlayer = null;
        foreach (GameObject tPlayer in GameObject.FindGameObjectsWithTag("Player"))
        {
            PlayerPath tPlayerPath = tPlayer.GetComponent<PlayerPath>();
            if(tPlayerPath.cSelected)
            {
                tSelectedPlayer = tPlayerPath.gameObject;
            }
        }

        return tSelectedPlayer;
    }

	// Update is called once per frame
	void Update () {
    ReachedWayPoint();
        if(cSelected)
        {
            GameObject.Find("Main Camera").SendMessage("SetAnyPlayerSelected",cSelected);
        }
   

	if(Input.GetMouseButtonDown(1) && cSelected)
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

        mListToFollow = mPathfinding.FindFastestRoadToPoint(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), GetComponent<NpcPersonality>());
        mVector3sToFollow.Clear();
        currentPathPercent = 1.01f;
    
        mWaypointNumber =  mListToFollow.Count-1;
        
        SetWaypoint();
       
    }

    void SetWaypoint()
    {
        if(mWaypointNumber > 0)
        {
            mWaypointNumber-= 1;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(mListToFollow != null)
        {
            //iTween.DrawPathGizmos(mVector3sToFollow.ToArray());
            foreach (var tNode in mListToFollow)
            {
               // Instantiate(Waypoint, mWayPointPostion, Quaternion.identity);
                Gizmos.DrawCube(tNode.Position, new Vector3(0.25f, 0.25f, 0.25f));
            }
        }
       
        
    }
}
