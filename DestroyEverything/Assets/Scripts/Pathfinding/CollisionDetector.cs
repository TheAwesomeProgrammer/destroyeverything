using System;
using UnityEngine;
using System.Collections.Generic;


public class UnWalkable
{
    public Vector3 Postion;
    public Vector3 LossyScale;
    public GameObject TheOwner;
}

public class CollisionDetector : MonoBehaviour
{

    public List<UnWalkable> cUnwalkAbles { get; set; }
    public List<UnWalkable> cMovingUnwalkAbles { get; set; }

    public float MoveBeforeAdded;

    public Vector3 cLossyScale { get; set; }

    private UnWalkable[,] mUnwalkables;
    private UnWalkable[,] mMovingUnwalkables;

    // Use this for initialization
    void Start()
    {
        cMovingUnwalkAbles = new List<UnWalkable>();
        cUnwalkAbles = new List<UnWalkable>();
        mUnwalkables = new UnWalkable[2000, 2000];
        mMovingUnwalkables = new UnWalkable[2000, 2000];
        cLossyScale = transform.parent.lossyScale / 2;

        foreach (GameObject tUnWalkable in GameObject.FindGameObjectsWithTag("Unwalkable"))
        {
           
           
            for (int x = (int) (1000 + (tUnWalkable.transform.position.x - (tUnWalkable.transform.lossyScale.x/2+cLossyScale.x))*10);
                x < (int)(1000 + (tUnWalkable.transform.position.x + (tUnWalkable.transform.lossyScale.x / 2 + cLossyScale.x)) * 10); x++)
            {

                for (int z = (int)(1000 + (tUnWalkable.transform.position.z - (tUnWalkable.transform.lossyScale.z / 2 + cLossyScale.z)) * 10);
                    z < (int)(1000 + (tUnWalkable.transform.position.z + (tUnWalkable.transform.lossyScale.z / 2 + cLossyScale.z)) * 10);
                    z++)
                {
                 mUnwalkables[x, z] = new UnWalkable()
                                             {
                                                 LossyScale = tUnWalkable.transform.lossyScale,
                                                 Postion = new Vector3((int)((1000 + tUnWalkable.transform.position.x) * 10), 0, (int)((1000 + tUnWalkable.transform.position.z) * 10)),
                                                 TheOwner = tUnWalkable
                                
                                             };
                }
            }
        }
       AddMovingUnwalkables();
        InvokeRepeating("AddMovingUnwalkables", 0.0000001f, 0.1f);
    }




    // Update is called once per frame
    void Update()
    {
        
    }

    void AddMovingUnwalkables()
    {
        Array.Clear(mMovingUnwalkables,0,mMovingUnwalkables.Length);
        foreach (GameObject tNpc in GameObject.FindGameObjectsWithTag("Npc"))
        {

            for (int x = (int)(1000 + (tNpc.transform.position.x - (tNpc.transform.lossyScale.x / 2 + cLossyScale.x)) * 10);
                  x < (int)(1000 + (tNpc.transform.position.x + (tNpc.transform.lossyScale.x / 2 + cLossyScale.x)) * 10);
                  x++)
            {
                for (
                    int z = (int)(1000 + (tNpc.transform.position.z - (tNpc.transform.lossyScale.z / 2 + cLossyScale.z)) * 10);
                    z < (int)(1000 + (tNpc.transform.position.z + (tNpc.transform.lossyScale.z / 2 + cLossyScale.z)) * 10);
                    z++)
                {
                    mMovingUnwalkables[x, z] = new UnWalkable()
                                             {
                                                 LossyScale = tNpc.transform.lossyScale,
                                                 Postion = new Vector3((int)((1000 + tNpc.transform.position.x) * 10), 0, (int)((1000 + tNpc.transform.position.z) * 10)),
                                                 TheOwner = tNpc
                                
                                             };
                }
            }
        }

        foreach (GameObject tPlayer in GameObject.FindGameObjectsWithTag("Player"))
        {
            for (int x = (int)(1000 + (tPlayer.transform.position.x - (tPlayer.transform.lossyScale.x / 2 + cLossyScale.x)) * 10);
             x < (int)(1000 + (tPlayer.transform.position.x + (tPlayer.transform.lossyScale.x / 2 + cLossyScale.x)) * 10);
             x++)
            {
                for (int z = (int)(1000 + (tPlayer.transform.position.z - (tPlayer.transform.lossyScale.z / 2 + cLossyScale.z)) * 10);
                    z < (int)(1000 + (tPlayer.transform.position.z + (tPlayer.transform.lossyScale.z / 2 + cLossyScale.z)) * 10);
                    z++)
                {
                    
                    mMovingUnwalkables[x, z] = new UnWalkable()
                                             {
                                                 LossyScale = tPlayer.transform.lossyScale,
                                                 Postion = new Vector3((int)((1000 + tPlayer.transform.position.x) * 10), 0, (int)((1000 + tPlayer.transform.position.z) * 10)),
                                                 TheOwner = tPlayer
                                
                                             };
                }
            }
        }

    }





    public bool IsPointCollidingWithUnWalkable(Vector3 pPosToCheck, GameObject pTheOwner)
    {
        bool tIsCollingWithUnWalkable = false;

        UnWalkable tMovingUnWalkable = mMovingUnwalkables[(int) pPosToCheck.x, (int) pPosToCheck.z];

        if (mUnwalkables[(int)pPosToCheck.x, (int)pPosToCheck.z] != null)
        {
            tIsCollingWithUnWalkable = true;
        }
        if (tMovingUnWalkable != null && tMovingUnWalkable.TheOwner.GetInstanceID() != pTheOwner.GetInstanceID())
        {
            tIsCollingWithUnWalkable = true;
        }




        return tIsCollingWithUnWalkable;
    }

    public UnWalkable IsPointCollidingWithUnWalkableAndGetUnwalkable(Vector3 pPosToCheck, GameObject pTheOwner)
    {
        UnWalkable tIsCollingWithUnWalkable = null;

        
        UnWalkable tMovingUnWalkable = mMovingUnwalkables[(int)pPosToCheck.x, (int)pPosToCheck.z];

        if (mUnwalkables[(int)pPosToCheck.x, (int)pPosToCheck.z] != null)
        {
            tIsCollingWithUnWalkable = mUnwalkables[(int) pPosToCheck.x, (int) pPosToCheck.z];
        }

        if (tMovingUnWalkable != null && tMovingUnWalkable.TheOwner != null &&  tMovingUnWalkable.TheOwner.GetInstanceID() != pTheOwner.GetInstanceID())
        {
            tIsCollingWithUnWalkable = tMovingUnWalkable;
        }


        return tIsCollingWithUnWalkable;
    }
}




       /* foreach (UnWalkable tMovingUnwalkable in cMovingUnwalkAbles)
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

        if(cUnwalkAbles.Exists(item => Mathf.Abs(item.Postion.x - pPosToCheck.x) < (item.LossyScale.x / 2 + cLossyScale.x) &&
                            Mathf.Abs(item.Postion.z - pPosToCheck.z) < (item.LossyScale.z / 2 + cLossyScale.z)))
        {
            tIsCollingWithUnWalkable = true;
        }

        foreach (UnWalkable tUnWalkable in cUnwalkAbles)
        {

            if (Mathf.Abs(tUnWalkable.Postion.x - pPosToCheck.x) < (tUnWalkable.LossyScale.x / 2 + cLossyScale.x) &&
                Mathf.Abs(tUnWalkable.Postion.z - pPosToCheck.z) < (tUnWalkable.LossyScale.z / 2 + cLossyScale.z))
            {

              
            }
        }


        if (cUnwalkAbles.Exists(item => Mathf.Abs(item.Postion.x - pPosToCheck.x) < (item.LossyScale.x / 2 + cLossyScale.x) &&
                       Mathf.Abs(item.Postion.z - pPosToCheck.z) < (item.LossyScale.z / 2 + cLossyScale.z)))
        {
            tIsCollingWithUnWalkable =
                cUnwalkAbles.Find(
                    item => Mathf.Abs(item.Postion.x - pPosToCheck.x) < (item.LossyScale.x/2 + cLossyScale.x) &&
                            Mathf.Abs(item.Postion.z - pPosToCheck.z) < (item.LossyScale.z/2 + cLossyScale.z));
        }
        
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
                        if (Input.GetMouseButtonDown(1))
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
        }*/