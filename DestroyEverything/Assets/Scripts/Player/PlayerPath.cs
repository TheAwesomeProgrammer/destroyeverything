using UnityEngine;
using System.Collections.Generic;

public class PlayerPath : MonoBehaviour
{
    public float Speed = 5;
    public float IntervalToFindNewPath = 0.25f;



    public bool cSelected { get; set; }


    private GameObject mWayPoint;

    private MoveViaList mMoveViaList;

    private List<Node> mListToFollow;

    private Vector3 mPostionToGetTo;

    private int mWaypointNumber;

    private float mPathTime = 0;

    

	// Use this for initialization
	void Start ()
	{
        mMoveViaList = transform.FindChild("MoveViaList").GetComponent<MoveViaList>();
	    cSelected = false;
        mListToFollow = new List<Node>();
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
        if(cSelected)
        {
            GameObject.Find("Main Camera").SendMessage("SetAnyPlayerSelected",cSelected);
        }
        if ( mPostionToGetTo != Vector3.zero && mPathTime < Time.time)
        {
            mPathTime = Time.time + IntervalToFindNewPath;
            mMoveViaList.MoveGameobjectViaList(gameObject,transform.position,mPostionToGetTo,Speed);
        }
   

	if(Input.GetMouseButtonDown(1) && cSelected)
	{
        mPostionToGetTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
 

	  

	}

   

    void SetListToFollow(List<Node> pListToFollow)
    {
        
       
    }

   
    void Moved()
    {

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
