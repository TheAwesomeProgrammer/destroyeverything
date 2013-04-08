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

    public Vector3 cLossyScale { get; set; }

    private UnWalkable[,] mUnwalkables;
    private UnWalkable[,] mMovingUnwalkables;

    private GameObject mBorders;

    // Use this for initialization
    void Start()
    {
        mBorders = GameObject.Find("Borders");
        cMovingUnwalkAbles = new List<UnWalkable>();
        cUnwalkAbles = new List<UnWalkable>();
        mUnwalkables = new UnWalkable[(int)mBorders.transform.lossyScale.x * 10,(int) mBorders.transform.lossyScale.z * 10];
        mMovingUnwalkables = new UnWalkable[(int)mBorders.transform.lossyScale.x * 10, (int)mBorders.transform.lossyScale.z * 10];
        cLossyScale = transform.parent.lossyScale / 2;
        foreach (GameObject tUnWalkable in GameObject.FindGameObjectsWithTag("Unwalkable"))
        {
           
           
            for (int x = (int) (((mBorders.transform.lossyScale.x / 2) * 10) + (tUnWalkable.transform.position.x - (tUnWalkable.transform.lossyScale.x/2+cLossyScale.x))*10);
                x < (int)((((mBorders.transform.lossyScale.x / 2) * 10)) + (tUnWalkable.transform.position.x + (tUnWalkable.transform.lossyScale.x / 2 + cLossyScale.x)) * 10); x++)
            {

                for (int z = (int)(((mBorders.transform.lossyScale.z / 2) * 10) + (tUnWalkable.transform.position.z - (tUnWalkable.transform.lossyScale.z / 2 + cLossyScale.z)) * 10);
                    z < (int)(((mBorders.transform.lossyScale.z / 2) * 10) + (tUnWalkable.transform.position.z + (tUnWalkable.transform.lossyScale.z / 2 + cLossyScale.z)) * 10);
                    z++)
                {
                 mUnwalkables[x, z] = new UnWalkable()
                                             {
                                                 LossyScale = tUnWalkable.transform.lossyScale,
                                                 Postion = new Vector3(x, 0, z),
                                                 TheOwner = tUnWalkable
                                
                                             };
                }
            }
        }

        InvokeRepeating("AddMovingUnwalkables",0.0001f,0.03f);
             
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

            for (int x = (int)(((mBorders.transform.lossyScale.x / 2) * 10) + (tNpc.transform.position.x - (tNpc.transform.lossyScale.x / 2 + cLossyScale.x)) * 10);
                  x < (int)(((mBorders.transform.lossyScale.x / 2) * 10) + (tNpc.transform.position.x + (tNpc.transform.lossyScale.x / 2 + cLossyScale.x)) * 10);
                  x++)
            {
                for (
                    int z = (int)(((mBorders.transform.lossyScale.z / 2) * 10) + (tNpc.transform.position.z - (tNpc.transform.lossyScale.z / 2 + cLossyScale.z)) * 10);
                    z < (int)(((mBorders.transform.lossyScale.z / 2) * 10) + (tNpc.transform.position.z + (tNpc.transform.lossyScale.z / 2 + cLossyScale.z)) * 10);
                    z++)
                {
                    mMovingUnwalkables[x, z] = new UnWalkable()
                                             {
                                                 LossyScale = tNpc.transform.lossyScale,
                                                 Postion = new Vector3(x, 0, z),
                                                 TheOwner = tNpc                                
                                             };
                }
            }
        }

        foreach (GameObject tPlayer in GameObject.FindGameObjectsWithTag("Player"))
        {
            for (int x = (int)(((mBorders.transform.lossyScale.x / 2) * 10) + (tPlayer.transform.position.x - (tPlayer.transform.lossyScale.x / 2 + cLossyScale.x)) * 10);
             x < (int)(((mBorders.transform.lossyScale.x / 2) * 10) + (tPlayer.transform.position.x + (tPlayer.transform.lossyScale.x / 2 + cLossyScale.x)) * 10);
             x++)
            {
                for (int z = (int)(((mBorders.transform.lossyScale.z / 2) * 10) + (tPlayer.transform.position.z - (tPlayer.transform.lossyScale.z / 2 + cLossyScale.z)) * 10);
                    z < (int)(((mBorders.transform.lossyScale.z / 2) * 10) + (tPlayer.transform.position.z + (tPlayer.transform.lossyScale.z / 2 + cLossyScale.z)) * 10);
                    z++)
                {
                    mMovingUnwalkables[x, z] = new UnWalkable()
                                             {
                                                 LossyScale = tPlayer.transform.lossyScale,
                                                 Postion = new Vector3(x, 0,z),
                                                 TheOwner = tPlayer
                                
                                             };
                }
            }
        }
        

    }





    public bool IsPointCollidingWithUnWalkable(Vector3 pPosToCheck, GameObject pTheOwner)
    {
        bool tIsCollingWithUnWalkable = false;

        try
        {
            if (mUnwalkables[(int)((pPosToCheck.x * 10) + (mBorders.transform.lossyScale.x / 2 * 10)),
            (int)+((pPosToCheck.z * 10) + (mBorders.transform.lossyScale.z / 2 * 10))] != null)
            {
                tIsCollingWithUnWalkable = true;
            }
        }
        catch (Exception)
        {
            print("POS EXPECTED TO BE LEADER POS "+pPosToCheck);
           print("POS THAT FUCKS"+ (new Vector3((int)((pPosToCheck.x * 10) + (mBorders.transform.lossyScale.x / 2 * 10)),0,
            (int)+((pPosToCheck.z * 10) + (mBorders.transform.lossyScale.z / 2 * 10)))));
        }
      

       UnWalkable tMovingUnWalkable = mMovingUnwalkables[(int)((pPosToCheck.x * 10) + (mBorders.transform.lossyScale.x/2 * 10)),
            (int)((pPosToCheck.z * 10) + (mBorders.transform.lossyScale.z/2 * 10))];

     
       if (tMovingUnWalkable != null && tMovingUnWalkable.TheOwner.GetInstanceID() != pTheOwner.GetInstanceID())
        {
            tIsCollingWithUnWalkable = true;
        } 




        return tIsCollingWithUnWalkable;
    }

    public UnWalkable IsPointCollidingWithUnWalkableAndGetUnwalkable(Vector3 pPosToCheck, GameObject pTheOwner)
    {
        UnWalkable tIsCollingWithUnWalkable = null;


        UnWalkable tMovingUnWalkable = mMovingUnwalkables[(int)((pPosToCheck.x * 10) + (mBorders.transform.lossyScale.x / 2 * 10)),
            (int)((pPosToCheck.z * 10) + (mBorders.transform.lossyScale.z / 2 * 10))];


        if (mUnwalkables[(int)((pPosToCheck.x * 10) + (mBorders.transform.lossyScale.x / 2 * 10)),
            (int)+((pPosToCheck.z * 10) + (mBorders.transform.lossyScale.z / 2 * 10))] != null)
        {
            tIsCollingWithUnWalkable = mUnwalkables[(int)((pPosToCheck.x * 10) + (mBorders.transform.lossyScale.x/2 * 10)), (int)+((pPosToCheck.z * 10) + (mBorders.transform.lossyScale.z/2 * 10))];
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