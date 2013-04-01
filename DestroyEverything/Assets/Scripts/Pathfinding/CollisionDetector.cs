using UnityEngine;
using System.Collections.Generic;


public class UnWalkable
{
    public Vector3 Postion;
    public Vector3 LossyScale;
    public  GameObject TheOwner;
}

public class CollisionDetector : MonoBehaviour
{

    public List<UnWalkable> cUnwalkAbles { get; set; }
    public List<UnWalkable> cMovingUnwalkAbles { get; set; }

    public float MoveBeforeAdded;

    public Vector3 cLossyScale { get; set; }

	// Use this for initialization
	void Start () {
        cMovingUnwalkAbles = new List<UnWalkable>();
        cUnwalkAbles = new List<UnWalkable>();
	    foreach (GameObject tUnWalkable in GameObject.FindGameObjectsWithTag("Unwalkable"))
	    {
            print(tUnWalkable.transform.lossyScale.z / 2);
	        cUnwalkAbles.Add(new UnWalkable()
	                             {
	                                 LossyScale = tUnWalkable.transform.lossyScale,
                                     Postion = tUnWalkable.transform.position
	                             });
        }
        cLossyScale = transform.parent.lossyScale / 2;
        print("CLOSSSYSCALE "+cLossyScale);
        AddMovingUnwalkables();
	}

    
    
	
	// Update is called once per frame
	void Update () {
        AddMovingUnwalkables();
	}

    void AddMovingUnwalkables()
    {
        cMovingUnwalkAbles.Clear();
        foreach (GameObject tNpc in GameObject.FindGameObjectsWithTag("Npc"))
        {
            cMovingUnwalkAbles.Add(new UnWalkable()
            {
                Postion = tNpc.transform.position,
                LossyScale = tNpc.transform.lossyScale,
                TheOwner = tNpc
            });
        }

        foreach (GameObject tPlayer in GameObject.FindGameObjectsWithTag("Player"))
        {
            cMovingUnwalkAbles.Add(new UnWalkable()
            {
                Postion = tPlayer.transform.position,
                LossyScale = tPlayer.transform.lossyScale,
                TheOwner = tPlayer
            });
        }
        
    }

    

   

    public bool IsPointCollidingWithUnWalkable(Vector3 pPosToCheck,GameObject pTheOwner)
    {
        bool tIsCollingWithUnWalkable = false;

        

          foreach (UnWalkable tMovingUnwalkable in cMovingUnwalkAbles)
          {
         

              if (Mathf.Abs(tMovingUnwalkable.Postion.x - pPosToCheck.x) < (tMovingUnwalkable.LossyScale.x / 2 + cLossyScale.x) &&
                  Mathf.Abs(tMovingUnwalkable.Postion.z - pPosToCheck.z) < (tMovingUnwalkable.LossyScale.z / 2 + cLossyScale.z))
                  {
                      tIsCollingWithUnWalkable = true;
                  }


              if (pTheOwner != null)
              {
                  if (tMovingUnwalkable.TheOwner.GetInstanceID() == pTheOwner.GetInstanceID())
                  {
                      if (Mathf.Abs(tMovingUnwalkable.TheOwner.transform.position.x - pPosToCheck.x) < (tMovingUnwalkable.TheOwner.transform.lossyScale.x / 2 + cLossyScale.x) &&
                          Mathf.Abs(tMovingUnwalkable.TheOwner.transform.position.z - pPosToCheck.z) < (tMovingUnwalkable.TheOwner.transform.lossyScale.z / 2 + cLossyScale.z))
                      {
                          tIsCollingWithUnWalkable = false;
                      }
                  }
              }
              
          }
          foreach (UnWalkable tUnWalkable in cUnwalkAbles)
          {

              if (Mathf.Abs(tUnWalkable.Postion.x - pPosToCheck.x) < (tUnWalkable.LossyScale.x / 2 + cLossyScale.x) &&
                  Mathf.Abs(tUnWalkable.Postion.z - pPosToCheck.z) < (tUnWalkable.LossyScale.z / 2 + cLossyScale.z))
              {

                  tIsCollingWithUnWalkable = true;
              }
          }


        return tIsCollingWithUnWalkable;
    }

    public UnWalkable IsPointCollidingWithUnWalkableAndGetUnwalkable(Vector3 pPosToCheck,GameObject pTheOwner)
    {
        UnWalkable tIsCollingWithUnWalkable = null;

        foreach (UnWalkable tUnWalkable in cUnwalkAbles)
        {
            if (Mathf.Abs(tUnWalkable.Postion.x - pPosToCheck.x) < (tUnWalkable.LossyScale.x / 2 + cLossyScale.x) &&
                Mathf.Abs(tUnWalkable.Postion.z - pPosToCheck.z) < (tUnWalkable.LossyScale.z / 2 + cLossyScale.z))
            {
                tIsCollingWithUnWalkable = tUnWalkable;
            }
        }

        foreach (UnWalkable tMovingUnwalkable in cMovingUnwalkAbles)
        {
            if (pTheOwner != null)
            {
                if (tMovingUnwalkable.TheOwner.GetInstanceID() == pTheOwner.GetInstanceID())
                {
                    if (Mathf.Abs(tMovingUnwalkable.TheOwner.transform.position.x - pPosToCheck.x) < (tMovingUnwalkable.LossyScale.x / 2 + cLossyScale.x) &&
                        Mathf.Abs(tMovingUnwalkable.TheOwner.transform.position.z - pPosToCheck.z) < (tMovingUnwalkable.LossyScale.z / 2 + cLossyScale.z))
                    {
                        if(Input.GetMouseButtonDown(1))
                        {
                            tIsCollingWithUnWalkable = tMovingUnwalkable;
                        }
                        else
                        {
                            tIsCollingWithUnWalkable = null;
                        }
                   
                        return tIsCollingWithUnWalkable;
                    }
                   
                }
            }


            if (Mathf.Abs(tMovingUnwalkable.Postion.x - pPosToCheck.x) < (tMovingUnwalkable.LossyScale.x / 2 + cLossyScale.x) &&
               Mathf.Abs(tMovingUnwalkable.Postion.z - pPosToCheck.z) < (tMovingUnwalkable.LossyScale.z / 2 + cLossyScale.z))
            {
                tIsCollingWithUnWalkable = tMovingUnwalkable;
            }
        }

        return tIsCollingWithUnWalkable;
    }
}
