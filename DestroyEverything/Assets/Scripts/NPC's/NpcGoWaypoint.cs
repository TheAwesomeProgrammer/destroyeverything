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
    public float Speed;

    
    public bool cMoveToWaypoint { get; set; }
    public bool cReachedWaypoint { get; set; }

    private List<NpcWaypoint> mMyNpcWaypoints;

    private NpcWaypoint mWayPointToGoTo;

    private int mWaypointNumber;

    private Camera mNpcCamera;
    

	// Use this for initialization
	void Start ()
	{
        mNpcCamera = transform.FindChild("Camera").GetComponent<Camera>();
        mMyNpcWaypoints = new List<NpcWaypoint>();
	    mWayPointToGoTo = null;
        FindMyWaypoints();
	    mWaypointNumber = 0;
        StartCycle();
	}
    	// Update is called once per frame
	void Update () {
	    if(cMoveToWaypoint)
	    {
	        MoveToWaypoint();
	    }
        ReachedWaypoint();
	}

    void StartCycle()
    {
        GoToWaypoint(Waypoints[mWaypointNumber].Name);
    }

    public void StopCycle()
    {
        cMoveToWaypoint = false;
        cReachedWaypoint = false;
    }



    void DoAnimation()
    {
        
    }

    void DoActivity()
    {
        
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
            transform.Translate((mWayPointToGoTo.transform.position - transform.position).normalized * Speed * Time.deltaTime);
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

 

    void ReachedWaypoint()
    {
        if(mWayPointToGoTo != null && Vector3.Distance(transform.position,mWayPointToGoTo.transform.position) < WaypointReachDistance)
        {
            mWayPointToGoTo = null;
            cMoveToWaypoint = false;
            cReachedWaypoint = true;
            StartCoroutine(Wait());
        }

        
    }
	

}
