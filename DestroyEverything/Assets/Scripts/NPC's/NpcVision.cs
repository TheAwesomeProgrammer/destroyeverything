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

	// Use this for initialization
	void Start ()
	{
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
            RaycastHit tHitInfo;
            if(Physics.Raycast(transform.position, (tPlayer.transform.position - transform.position),out tHitInfo,SeeDistance))
            {
                if(tHitInfo.collider.tag == "Player")
                {
                   
                    cCanSeePlayer = true;
                }
                else if (tHitInfo.collider.tag == "Corpse")
                {
                    mNpcThinking.SendMessage("SetEmotion",Emotion.Scared);
                    cCanSeePlayer = false;
                }
                else 
                {
                    cCanSeePlayer = false;
                }
              
                Debug.DrawLine(transform.position,tHitInfo.point,Color.red,.01f);
            }
            else
            {
                cCanSeePlayer = false;
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
