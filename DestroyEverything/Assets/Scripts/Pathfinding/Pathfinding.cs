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

    private GameObject mTheOwner;

	// Use this for initialization
	void Start ()
	{
	    mCollisionDetector = GameObject.Find("CollisionDetector").GetComponent<CollisionDetector>();
        mCostCalculater = GameObject.Find("CostCalculator").GetComponent<CostCalculating>();
	}

    void AddSquaresToOpenListAroundYou(Node tNodeSquaresAround, Vector3 pPosToFindRoadTo,GameObject pTheOwner)
    {

    
        // Right position of player
        Node tRightPos = new Node()
        {
            GCost = 0,
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x + GridExpandNumber, tNodeSquaresAround.Position.y,tNodeSquaresAround.Position.z)
        };

        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(new Vector3((int)(tRightPos.Position.x * 10) + 1000,0, (int)(tRightPos.Position.z * 10) + 1000), pTheOwner) &&
            !UsedCoordinates[(int)(tRightPos.Position.x * 10) + 1000, (int)(tRightPos.Position.z * 10) + 1000])
        {
            UsedCoordinates[(int)(tRightPos.Position.x * 10) + 1000, (int)(tRightPos.Position.z * 10) + 1000] = true;
            tRightPos.GCost = mCostCalculater.CalculateGCost(tRightPos);
            tRightPos.HCost = mCostCalculater.CalculateHCost(tRightPos, pPosToFindRoadTo, GridExpandNumber);
            tRightPos.FCost = tRightPos.GCost + tRightPos.HCost;
            cOpenList.Add(tRightPos);
        }


        // Left position of player
        Node tLeftPos = new Node()
        {
            GCost = 0,
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x - GridExpandNumber, tNodeSquaresAround.Position.y, tNodeSquaresAround.Position.z)
        };
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(new Vector3((int)(tLeftPos.Position.x * 10) + 1000, 0, (int)(tLeftPos.Position.z * 10) + 1000), pTheOwner) &&
            !UsedCoordinates[(int)(tLeftPos.Position.x * 10) + 1000, (int)(tLeftPos.Position.z * 10) + 1000])
        {
            UsedCoordinates[(int)(tLeftPos.Position.x * 10) + 1000, (int)(tLeftPos.Position.z * 10) + 1000] = true;
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
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(new Vector3((int)(tUpPos.Position.x * 10) + 1000, 0, (int)(tUpPos.Position.z * 10) + 1000), pTheOwner) &&
            !UsedCoordinates[(int)(tUpPos.Position.x * 10) + 1000, (int)(tUpPos.Position.z * 10) + 1000])
        {
            UsedCoordinates[(int)(tUpPos.Position.x * 10) + 1000, (int)(tUpPos.Position.z * 10) + 1000] = true;
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
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(new Vector3((int)(tUpLeftPos.Position.x * 10) + 1000, 0, (int)(tUpLeftPos.Position.z * 10) + 1000), pTheOwner) &&
            !UsedCoordinates[(int)(tUpLeftPos.Position.x * 10) + 1000, (int)(tUpLeftPos.Position.z * 10) + 1000])
        {
            UsedCoordinates[(int)(tUpLeftPos.Position.x * 10) + 1000, (int)(tUpLeftPos.Position.z * 10) + 1000] = true;
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
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(new Vector3((int)(tUpRightPos.Position.x * 10) + 1000, 0, (int)(tUpRightPos.Position.z * 10) + 1000), pTheOwner) &&
            !UsedCoordinates[(int)(tUpRightPos.Position.x * 10) + 1000, (int)(tUpRightPos.Position.z * 10) + 1000])
        {
            UsedCoordinates[(int)(tUpRightPos.Position.x * 10) + 1000, (int)(tUpRightPos.Position.z * 10) + 1000] = true;
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
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(new Vector3((int)(tDownPos.Position.x * 10) + 1000, 0, (int)(tDownPos.Position.z * 10) + 1000), pTheOwner) &&
            !UsedCoordinates[(int)(tDownPos.Position.x * 10) + 1000, (int)(tDownPos.Position.z * 10) + 1000])
        {
            UsedCoordinates[(int)(tDownPos.Position.x * 10) + 1000, (int)(tDownPos.Position.z * 10) + 1000] = true;
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
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(new Vector3((int)(tDownLeftPos.Position.x * 10) + 1000, 0, (int)(tDownLeftPos.Position.z * 10) + 1000), pTheOwner) &&
            !UsedCoordinates[(int)(tDownLeftPos.Position.x * 10) + 1000, (int)(tDownLeftPos.Position.z * 10) + 1000])
        {
            UsedCoordinates[(int)(tDownLeftPos.Position.x * 10) + 1000, (int)(tDownLeftPos.Position.z * 10) + 1000] = true;
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

        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(new Vector3((int)(tDownRightPos.Position.x * 10) + 1000, 0, (int)(tDownRightPos.Position.z * 10) + 1000), pTheOwner) &&
            !UsedCoordinates[(int)(tDownRightPos.Position.x * 10) + 1000, (int)(tDownRightPos.Position.z * 10) + 1000])
        {
            UsedCoordinates[(int)(tDownRightPos.Position.x * 10) + 1000, (int)(tDownRightPos.Position.z * 10) + 1000] = true;
            tDownRightPos.GCost = mCostCalculater.CalculateGCost(tDownRightPos);
            tDownRightPos.HCost = mCostCalculater.CalculateHCost(tDownRightPos, pPosToFindRoadTo, GridExpandNumber);
            tDownRightPos.FCost = tDownRightPos.GCost + tDownRightPos.HCost;
            cOpenList.Add(tDownRightPos);
        }

       

    }

   bool BetterPathGCost(Node pNode)
   {
       bool tBetterPath = false;
        Node tNode = pNode;
        int tCost = mCurrentNode.GCost;
        if (tNode.Position.x < mCurrentNode.Position.x || tNode.Position.x > mCurrentNode.Position.x ||
             tNode.Position.z < mCurrentNode.Position.z || tNode.Position.z > mCurrentNode.Position.z)
        {
            tCost += 10;
        }
        if (tNode.Position.x > tNode.Parent.Position.x && tNode.Position.z > tNode.Parent.Position.z ||
            tNode.Position.z < tNode.Parent.Position.z && tNode.Position.x < tNode.Parent.Position.x ||
            tNode.Position.x > tNode.Parent.Position.x && tNode.Position.z < tNode.Parent.Position.z ||
            tNode.Position.z < tNode.Parent.Position.z && tNode.Position.x > tNode.Parent.Position.x)
        {
            tCost += 14;
        }

       if(tNode.GCost > tCost)
       {
           tBetterPath = true;
       }

       return tBetterPath;
    }

    /*bool IsNodeInClosedOrOpenList(Node pNodeToCheck)
    {
        bool tIsNodeInClosedOrOpenList = false;

        if (cClosedList.Exists(item => item.Position == pNodeToCheck.Position))
        {
             tIsNodeInClosedOrOpenList = true;
        }

        Node tOpenNode = cOpenList.Find(item => item.Position == pNodeToCheck.Position);
        if (tOpenNode != null) 
        {
                if(BetterPathGCost(tOpenNode))
                {
                    tOpenNode.Parent = mCurrentNode;
                }
            tIsNodeInClosedOrOpenList = true;
        }

   

        return tIsNodeInClosedOrOpenList;
    }*/

    public void InitFastestRoad(Vector3 pStartPos, Vector3 pPosToFindRoadTo, GameObject pTheOwner)
    {
        cOpenList = new List<Node>();
        cClosedList = new List<Node>();
        UsedCoordinates = new bool[2000,2000];
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


    void FindFastestRoadToPoint()
    {

        float tTimeOut = Time.realtimeSinceStartup;
        while (cRun)
        {
            if(mTheOwner.tag == "Npc")
            {
                
            }

            UnWalkable tUnWalkableCollidingWith =
                mCollisionDetector.IsPointCollidingWithUnWalkableAndGetUnwalkable(new Vector3((int)(mEndPos.x * 10) + 1000, 0, (int)(mEndPos.z * 10) + 1000), mTheOwner);

            if (tUnWalkableCollidingWith != null)
            {
                if (tUnWalkableCollidingWith.Postion.x >= mEndPos.x)
                {
                    mEndPos.x -= tUnWalkableCollidingWith.LossyScale.x;
                }

                else if (tUnWalkableCollidingWith.Postion.x < mEndPos.x)
                {
                    mEndPos.x += tUnWalkableCollidingWith.LossyScale.x;
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
            mTheOwner.SendMessage("SetListToFollow",MakeListToFollow());

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
