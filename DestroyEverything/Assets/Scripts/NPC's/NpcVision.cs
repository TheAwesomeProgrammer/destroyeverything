using UnityEngine;
using System.Collections.Generic;

public class NpcVision : MonoBehaviour
{
    

    public float SeeDistance;

    public bool cCanSeePlayer { get; set; }
    public bool cMovingToComfort { get; set; }

    private GameObject[] mPlayers;

    private GameObject mPlayerICanSee;

    private NpcThinking mNpcThinking;

    private GameObject mNpcToMoveTo;

    private Camera mNpcCamera;

	// Use this for initialization
	void Start ()
	{
	    mNpcCamera = transform.FindChild("Camera").GetComponent<Camera>();
	    mNpcThinking = GetComponent<NpcThinking>();
	    mPlayers = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	    CheckIfCanSeePlayer();
  
      
        
	}



    void CheckIfCanSeePlayer()
    {

        foreach (GameObject tPlayer in mPlayers)
        {
            if(mNpcCamera != null)
            {
                if (tPlayer.renderer.IsVisibleFrom(mNpcCamera))
                {
                    RaycastHit tHitInfo;
                    if(Physics.Raycast(transform.position,(tPlayer.transform.position-transform.position),out tHitInfo,SeeDistance))
                    {
                        if(tHitInfo.collider.tag == "Player")
                        {
                            cCanSeePlayer = true;
                        }
                        
                    }
                 }
                else
                {
                    cCanSeePlayer = false;
                }
            }
           
        }
       
    }

    void CheckIfCanSeeOtherNpc()
    {
        foreach (GameObject tTagNpc in GameObject.FindGameObjectsWithTag("Npc"))
        {
           
                RaycastHit tHitInfo;
                if (Physics.Raycast(transform.position, (tTagNpc.transform.position - transform.position), out tHitInfo, SeeDistance))
                {

                    if (tHitInfo.collider.tag == "Npc")
                    {

                    }

                }
            
            
        }
         
    }
}
