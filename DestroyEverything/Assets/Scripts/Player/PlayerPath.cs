using UnityEngine;
using System.Collections;

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

   

	if(Input.GetMouseButton(0) && cSelected)
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
            mMoveToWayPoint = false;
            Destroy(mWayPoint.gameObject);
        }
    }

    void MakeWayPoint()
    {
        
        if(mMoveToWayPoint)
        {
            Destroy(mWayPoint.gameObject);
        }
        mMoveToWayPoint = true;
        mWayPointPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mWayPointPostion.y = transform.position.y;
        mWayPoint = Instantiate(Waypoint, mWayPointPostion, transform.rotation) as GameObject;
        mPathfinding.FindFastestRoadToPoint(transform.position,mWayPointPostion);
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
