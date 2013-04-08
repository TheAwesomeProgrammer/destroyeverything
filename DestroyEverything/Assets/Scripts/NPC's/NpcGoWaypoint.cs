using System.Collections;
using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class Waypoint
{
    public string Name;
    public float Worktime;
}

public class NpcGoWaypoint: MonoBehaviour
{

    public Waypoint[] Waypoints;
    
    public float WaypointReachDistance;
    public float WalkSpeed;
    public float RunSpeed;
    public float IntervalToFindNewPath = 0.25f;

    
    public bool cMoveToWaypoint { get; set; }
    public bool cReachedWaypoint { get; set; }
    
    private List<NpcWaypoint> mMyNpcWaypoints;

    private NpcWaypoint mWayPointToGoTo;

    private int mWaypointNumber;

    private Camera mNpcCamera;

    private MoveViaList mMoveViaList;

    private bool mStopped;
    private bool mFollowLeader;

    private float mPathTime = 0;

    private GameObject mLeaderToFollow;
    

	// Use this for initialization
	void Start ()
	{
        mMoveViaList = transform.FindChild("MoveViaList").GetComponent<MoveViaList>();
        mNpcCamera = transform.FindChild("Camera").GetComponent<Camera>();
        StartCycle();
      }
    	// Update is called once per frame
	void Update () {
        if(mFollowLeader)
        {
            //mFollowLeader = FindNpcWithMostLeadership();
        }

	    if(cMoveToWaypoint)
	    {
	        MoveToWaypoint();
	    }
	}

    public void StartCycle()
    {
        mStopped = false;
        mMyNpcWaypoints = new List<NpcWaypoint>();
        FindMyWaypoints();
        GoToWaypoint(Waypoints[mWaypointNumber].Name);
     }

    public void StopCycle()
    {
        mMoveViaList.Stop();
        mStopped = true;
        cMoveToWaypoint = false;
        cReachedWaypoint = false;
    }



    void DoAnimation()
    {
        
    }

    void DoActivity()
    {
        
    }

    public void RunAwayToNpcWithMostLeadership()
    {
        mLeaderToFollow = FindNpcWithMostLeadership();
        print("LEADERPOS "+mLeaderToFollow.transform.position);
        mMoveViaList.MoveGameobjectViaList(gameObject, transform.position, mLeaderToFollow.transform.position, RunSpeed);
        
        mFollowLeader = true;
    }



    GameObject FindNpcWithMostLeadership()
    {
        GameObject tNpcWithMostLeadership = null;
        int tLeadership = int.MinValue;
        foreach (GameObject tNpc in GameObject.FindGameObjectsWithTag("Npc"))
        {
            if(tNpc != gameObject)
            {
                NpcPersonality tNpcPersonality = tNpc.GetComponent<NpcPersonality>();

                if (tNpcPersonality.MyPersonality.Leadership > tLeadership)
                {
                    tLeadership = tNpcPersonality.MyPersonality.Leadership;
                    tNpcWithMostLeadership = tNpc;
                }
            }
           
        }
        return tNpcWithMostLeadership;
    }

    

    IEnumerator Wait()
    {
        DoAnimation();
        yield return new WaitForSeconds(Waypoints[mWaypointNumber].Worktime);
        if(mWaypointNumber >= Waypoints.Length-1)
        {
            mWaypointNumber = 0;
        }
        else
        {
            mWaypointNumber++;
        }
        GoToWaypoint(Waypoints[mWaypointNumber].Name);
    }

   
    void MoveToWaypoint()
    {
        if(mWayPointToGoTo != null)
        {
            mNpcCamera.transform.LookAt(mWayPointToGoTo.transform.position);
            if(mPathTime < Time.time)
            {
                mPathTime = Time.time + IntervalToFindNewPath;
                if(mLeaderToFollow != null)
                {
                    mMoveViaList.MoveGameobjectViaList(gameObject, transform.position, mLeaderToFollow.transform.position, WalkSpeed);
                }
                else
                {
                    mMoveViaList.MoveGameobjectViaList(gameObject, transform.position, mWayPointToGoTo.transform.position, WalkSpeed);
                }
                
            }
           
        }
    }

    void FindMyWaypoints()
    {
        foreach (GameObject tTagWaypoint in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            NpcWaypoint tNpcWaypoint = tTagWaypoint.GetComponent<NpcWaypoint>();

            foreach (Waypoint tWaypoint in Waypoints)
            {
                if(tNpcWaypoint.WaypointName == tWaypoint.Name)
                {
                    mMyNpcWaypoints.Add(tNpcWaypoint);
                }
            }
        }
    }

    void GoToWaypoint(string pName)
    {
        foreach (NpcWaypoint tNpcWaypoint in mMyNpcWaypoints)
        {
            if(tNpcWaypoint.WaypointName == pName)
            {
                cMoveToWaypoint = true;
                mWayPointToGoTo = tNpcWaypoint;
            }
        }
    }

 

    void Moved()
    {
           if(!mStopped)
           {
               mWayPointToGoTo = null;
               cMoveToWaypoint = false;
               cReachedWaypoint = true;
               StartCoroutine(Wait());
           }
    
        
    }
	

}
