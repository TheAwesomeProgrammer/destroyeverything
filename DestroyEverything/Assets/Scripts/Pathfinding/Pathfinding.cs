using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class Node
{
    public int GCost = 0;
    public int HCost = 0;
    public int FCost = 0;
    public Vector3 Position = Vector3.zero;
    public Node Parent = null;
}

public enum Direction
{
    Right,
    left,
    Up,
    Down,
    All
}

public class Pathfinding : MonoBehaviour
{

 

    public float GridExpandNumber = 0.1f;
    public float TimeOut;
   

    public List<Node> cClosedList { get; set; }
    public List<Node> cOpenList { get; set; }
    public List<Node> cLastList { get; set; }

    public bool cRun { get; set; }

    public bool[,] UsedCoordinates;

    private Node mCurrentNode; 

    private CollisionDetector mCollisionDetector;
    private CostCalculating mCostCalculater;

    private Vector3 mStartPos;
    private Vector3 mEndPos;
    public Vector3[] PosNextToNode;

    private GameObject mTheOwner;
    private GameObject mBorders;

	// Use this for initialization
	void Start ()
	{
	    mCollisionDetector = GameObject.Find("CollisionDetector").GetComponent<CollisionDetector>();
        mCostCalculater = GameObject.Find("CostCalculator").GetComponent<CostCalculating>();
	    mBorders = GameObject.Find("Borders");
       PosNextToNode =  new Vector3[8]{
                                             new Vector3(GridExpandNumber,0,0), 
                                             new Vector3(-GridExpandNumber,0,0),
                                             new Vector3(0,0,GridExpandNumber),
                                             new Vector3(-GridExpandNumber,0,GridExpandNumber),
                                             new Vector3(GridExpandNumber,0,GridExpandNumber),
                                             new Vector3(0,0-GridExpandNumber),
                                             new Vector3(-GridExpandNumber,0-GridExpandNumber),
                                             new Vector3(GridExpandNumber,0,-GridExpandNumber),
                                             };
	}

    void AddSquaresToOpenListAroundYou(Node tNodeSquaresAround, Vector3 pPosToFindRoadTo,GameObject pTheOwner)
    {

    for(int i = 0; i < 8;i++)
    {
        // Position of player
        Node tNode = new Node()
        {
            GCost = 0,
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x + PosNextToNode[i].x, tNodeSquaresAround.Position.y + PosNextToNode[i].y,
                tNodeSquaresAround.Position.z + PosNextToNode[i].z)
        };

        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tNode.Position, pTheOwner) &&
            !UsedCoordinates[(int)((tNode.Position.x * 10) + ((mBorders.transform.lossyScale.x / 2) * 10)),
            (int)((tNode.Position.z * 10) + ((mBorders.transform.lossyScale.z / 2) * 10))])
        {
            UsedCoordinates[(int)((tNode.Position.x * 10) + ((mBorders.transform.lossyScale.x / 2) * 10)),
            (int)((tNode.Position.z * 10) + ((mBorders.transform.lossyScale.z / 2) * 10))] = true;
            tNode.GCost = mCostCalculater.CalculateGCost(tNode);
            tNode.HCost = mCostCalculater.CalculateHCost(tNode, pPosToFindRoadTo, GridExpandNumber);
            tNode.FCost = tNode.GCost + tNode.HCost;
            cOpenList.Add(tNode);
        }
    }
}



    public void InitFastestRoad(Vector3 pStartPos, Vector3 pPosToFindRoadTo, GameObject pTheOwner)
    {
        cOpenList = new List<Node>();
        cClosedList = new List<Node>();
        UsedCoordinates = new bool[(int)mBorders.transform.lossyScale.x * 10, (int)mBorders.transform.lossyScale.z * 10];
        mStartPos = pStartPos;
        mEndPos = pPosToFindRoadTo;
        mTheOwner = pTheOwner;

        mCurrentNode = new Node()
        {
            Parent = null,
            Position = mStartPos
        };
        cOpenList.Add(mCurrentNode);
        cRun = true;

        FindFastestRoadToPoint();
        
        
    }

   Vector3 FindEmptySpot(Vector3 pCurrentSpot,Direction pDirection)
    {
        Vector3 tEmptySpot = pCurrentSpot;

        while(mCollisionDetector.IsPointCollidingWithUnWalkable(tEmptySpot,mTheOwner))
        {
            switch (pDirection)
            {
                case Direction.Right:
                  
                        for(int i = 0; i < 1;i++)
                        {
                            tEmptySpot =  new Vector3(tEmptySpot.x + PosNextToNode[i].x,tEmptySpot.y,tEmptySpot.z);
                        }

                    break;

                case Direction.left:
                  
                        for (int i = 1; i < 2; i++)
                        {
                            tEmptySpot = new Vector3(tEmptySpot.x + PosNextToNode[i].x, tEmptySpot.y, tEmptySpot.z);
                        }
                    
                   break;

                case Direction.Up :
                   
                        for(int i = 3; i < 4;i++)
                        {
                            tEmptySpot =  new Vector3(tEmptySpot.x + PosNextToNode[i].x,tEmptySpot.y,tEmptySpot.z);
                        }
                    break;

                case Direction.Down:

                        for (int i = 6; i < 7; i++)
                        {
                            tEmptySpot = new Vector3(tEmptySpot.x + PosNextToNode[i].x, tEmptySpot.y, tEmptySpot.z);
                        }
                   break;
                case Direction.All:

                   for (int i = 0; i < 8; i++)
                   {
                       tEmptySpot = new Vector3(tEmptySpot.x + PosNextToNode[i].x, tEmptySpot.y + PosNextToNode[i].y, tEmptySpot.z + PosNextToNode[i].z);
                   }
                   break;
                    
            }
        }

        return tEmptySpot;
    }


    void FindFastestRoadToPoint()
    {

        float tTimeOut = Time.realtimeSinceStartup;
        while (cRun)
        {

            UnWalkable tUnWalkableCollidingWith =
                mCollisionDetector.IsPointCollidingWithUnWalkableAndGetUnwalkable(mEndPos, mTheOwner);
            float tMakeSmallerByAmout = 1f;
            if(tUnWalkableCollidingWith != null)
            {
                    if(mTheOwner.tag == "Npc")
                    {
                        mEndPos = FindEmptySpot(mEndPos, Direction.All);
                    }
                    else if (tUnWalkableCollidingWith.Postion.x < mEndPos.x)
                    {
                        mEndPos = FindEmptySpot(mEndPos, Direction.left);
                    }
                    else if (tUnWalkableCollidingWith.Postion.x > mEndPos.x)
                    {
                        mEndPos = FindEmptySpot(mEndPos, Direction.Right);
                    }
            }

            AddSquaresToOpenListAroundYou(mCurrentNode, mEndPos, mTheOwner);

            // Find lowest Fvalue in openlist
            Node tLowestFNode = null;
            float LowestF = float.MaxValue;
            foreach (Node tNode in cOpenList)
            {
                if (tNode.FCost <= LowestF)
                {
                    LowestF = tNode.FCost;
                    tLowestFNode = tNode;
                }
            }


            // Add the lowest fValue to closedlist and remove it from the openlist.
            cOpenList.Remove(tLowestFNode);
            cClosedList.Add(tLowestFNode);
            mCurrentNode = tLowestFNode;





            if (mCurrentNode == null || Mathf.Abs(mCurrentNode.Position.x - mEndPos.x) < GridExpandNumber &&
                Mathf.Abs(mCurrentNode.Position.z - mEndPos.z) < GridExpandNumber)
            {
                cRun = false;
                break;
            }
            if (cOpenList.Count <= 0)
            {
                cRun = false;
                break;
            }
            if (tTimeOut + Time.realtimeSinceStartup < TimeOut)
            {
                cRun = false;
                break;
            }
        }



        if(!cRun)
        {
            CancelInvoke("FindFastestRoadToPoint");
            mTheOwner.transform.FindChild("MoveViaList").SendMessage("SetListToFollow",MakeListToFollow());
            mStartPos = Vector3.zero;
            mEndPos = Vector3.zero;
            mTheOwner = null;
        }
       


    }

    List<Node> MakeListToFollow()
    {
        List<Node> tListToFollow = new List<Node>();
        Node tNode = cClosedList.ToArray()[cClosedList.Count - 1];
       
        while (tNode != null && tNode.Parent != null)
        {
            tListToFollow.Add(tNode);
            tNode = tNode.Parent;
        }

        return tListToFollow;
    }

    void OnGUI()
    {
        
    }
    

}



















/*     // Left position of player
        Node tLeftPos = new Node()
        {
            GCost = 0,
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x - GridExpandNumber, tNodeSquaresAround.Position.y, tNodeSquaresAround.Position.z)
        };
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(new Vector3((int)(tLeftPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) ), 0, (int)(tLeftPos.Position.z ) + ((mBorders.transform.lossyScale.x / 2) )), pTheOwner) &&
            !UsedCoordinates[(int)((tLeftPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) )), (int)((tLeftPos.Position.z ) + ((mBorders.transform.lossyScale.z / 2) ))])
        {
            UsedCoordinates[(int)((tLeftPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) )), (int)((tLeftPos.Position.z ) + ((mBorders.transform.lossyScale.z / 2) ))] = true;
            tLeftPos.GCost = mCostCalculater.CalculateGCost(tLeftPos);
            tLeftPos.HCost = mCostCalculater.CalculateHCost(tLeftPos, pPosToFindRoadTo, GridExpandNumber);
            tLeftPos.FCost = tLeftPos.GCost + tLeftPos.HCost;
            cOpenList.Add(tLeftPos);
        }



        // Up position of player
        Node tUpPos = new Node()
        {
            GCost = 0,
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x, tNodeSquaresAround.Position.y, tNodeSquaresAround.Position.z + GridExpandNumber)
        };
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(new Vector3((int)(tUpPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) ), 0, (int)(tUpPos.Position.z ) + ((mBorders.transform.lossyScale.x / 2) )), pTheOwner) &&
            !UsedCoordinates[(int)((tUpPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) )), (int)((tUpPos.Position.z ) + ((mBorders.transform.lossyScale.z / 2) ))])
        {
            UsedCoordinates[(int)((tUpPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) )), (int)((tUpPos.Position.z ) + ((mBorders.transform.lossyScale.z / 2) ))] = true;
            tUpPos.GCost = mCostCalculater.CalculateGCost(tUpPos);
            tUpPos.HCost = mCostCalculater.CalculateHCost(tUpPos, pPosToFindRoadTo, GridExpandNumber);
            tUpPos.FCost = tUpPos.GCost + tUpPos.HCost;
            cOpenList.Add(tUpPos);
        }

        // UpLeft position of player
        Node tUpLeftPos = new Node()
        {
            GCost = 0,
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x - GridExpandNumber, tNodeSquaresAround.Position.y, tNodeSquaresAround.Position.z + GridExpandNumber)
        };
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(new Vector3((int)(tUpLeftPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) ), 0, (int)(tUpLeftPos.Position.z ) + ((mBorders.transform.lossyScale.x / 2) )), pTheOwner) &&
            !UsedCoordinates[(int)((tUpLeftPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) )), (int)((tUpLeftPos.Position.z ) + ((mBorders.transform.lossyScale.z / 2) ))])
        {
            UsedCoordinates[(int)((tUpLeftPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) )), (int)((tUpLeftPos.Position.z ) + ((mBorders.transform.lossyScale.z / 2) ))] = true;
            tUpLeftPos.GCost = mCostCalculater.CalculateGCost(tUpLeftPos);
            tUpLeftPos.HCost = mCostCalculater.CalculateHCost(tUpLeftPos, pPosToFindRoadTo, GridExpandNumber);
            tUpLeftPos.FCost = tUpLeftPos.GCost + tUpLeftPos.HCost;
            cOpenList.Add(tUpLeftPos);
        }

        // UpRight position of player
        Node tUpRightPos = new Node()
        {
            GCost = 0,
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x + GridExpandNumber, tNodeSquaresAround.Position.y, tNodeSquaresAround.Position.z + GridExpandNumber)
        };
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(new Vector3((int)(tUpRightPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) ), 0, (int)(tUpRightPos.Position.z ) + ((mBorders.transform.lossyScale.x / 2) )), pTheOwner) &&
            !UsedCoordinates[(int)((tUpRightPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) )), (int)((tUpRightPos.Position.z ) + ((mBorders.transform.lossyScale.z / 2) ))])
        {
            UsedCoordinates[(int)((tUpRightPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) )), (int)((tUpRightPos.Position.z ) + ((mBorders.transform.lossyScale.z / 2) ))] = true;
            tUpRightPos.GCost = mCostCalculater.CalculateGCost(tUpRightPos);
            tUpRightPos.HCost = mCostCalculater.CalculateHCost(tUpRightPos, pPosToFindRoadTo, GridExpandNumber);
            tUpRightPos.FCost = tUpRightPos.GCost + tUpRightPos.HCost;
            cOpenList.Add(tUpRightPos);
        }

        // Down position of player
        Node tDownPos = new Node()
        {
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x, tNodeSquaresAround.Position.y, tNodeSquaresAround.Position.z - GridExpandNumber)
        };
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(new Vector3((int)(tDownPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) ), 0, (int)(tDownPos.Position.z ) + ((mBorders.transform.lossyScale.x / 2) )), pTheOwner) &&
            !UsedCoordinates[(int)((tDownPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) )), (int)((tDownPos.Position.z ) + ((mBorders.transform.lossyScale.z / 2) ))])
        {
            UsedCoordinates[(int)((tDownPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) )), (int)((tDownPos.Position.z ) + ((mBorders.transform.lossyScale.z / 2) ))] = true;
            tDownPos.GCost = mCostCalculater.CalculateGCost(tDownPos);
            tDownPos.HCost = mCostCalculater.CalculateHCost(tDownPos, pPosToFindRoadTo, GridExpandNumber);
            tDownPos.FCost = tDownPos.GCost + tDownPos.HCost;
            cOpenList.Add(tDownPos);
        }

        // DownLeft position of player
        Node tDownLeftPos = new Node()
        {
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x - GridExpandNumber, tNodeSquaresAround.Position.y, tNodeSquaresAround.Position.z - GridExpandNumber)
        };
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(new Vector3((int)(tDownLeftPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) ), 0, (int)(tDownLeftPos.Position.z ) + ((mBorders.transform.lossyScale.x / 2) )), pTheOwner) &&
            !UsedCoordinates[(int)((tDownLeftPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) )), (int)((tDownLeftPos.Position.z ) + ((mBorders.transform.lossyScale.z / 2) ))])
        {
            UsedCoordinates[(int)((tDownLeftPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) )), (int)((tDownLeftPos.Position.z ) + ((mBorders.transform.lossyScale.z / 2) ))] = true;
            tDownLeftPos.GCost = mCostCalculater.CalculateGCost(tDownLeftPos);
            tDownLeftPos.HCost = mCostCalculater.CalculateHCost(tDownLeftPos, pPosToFindRoadTo, GridExpandNumber);
            tDownLeftPos.FCost = tDownLeftPos.GCost + tDownLeftPos.HCost;
            cOpenList.Add(tDownLeftPos);
        }

        // DownRight position of player
        Node tDownRightPos = new Node()
        {
            GCost = 0,
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x + GridExpandNumber, tNodeSquaresAround.Position.y, tNodeSquaresAround.Position.z - GridExpandNumber)
        };

        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(new Vector3((int)(tDownRightPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) ), 0, (int)(tDownRightPos.Position.z ) + ((mBorders.transform.lossyScale.x / 2) )), pTheOwner) &&
            !UsedCoordinates[(int)((tDownRightPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) )), (int)((tDownRightPos.Position.z ) + ((mBorders.transform.lossyScale.z / 2) ))])
        {
            UsedCoordinates[(int)((tDownRightPos.Position.x ) + ((mBorders.transform.lossyScale.x / 2) )), (int)((tDownRightPos.Position.z ) + ((mBorders.transform.lossyScale.z / 2) ))] = true;
            tDownRightPos.GCost = mCostCalculater.CalculateGCost(tDownRightPos);
            tDownRightPos.HCost = mCostCalculater.CalculateHCost(tDownRightPos, pPosToFindRoadTo, GridExpandNumber);
            tDownRightPos.FCost = tDownRightPos.GCost + tDownRightPos.HCost;
            cOpenList.Add(tDownRightPos);
        } */
