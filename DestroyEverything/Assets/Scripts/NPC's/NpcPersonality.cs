using UnityEngine;
using System.Collections;
[System.Serializable]
public class DescribePersonality
{
    public string Name;
    public int Leadership;
}

public enum State
{
    Running,
    Safe,
    DoJob
}

public class NpcPersonality : MonoBehaviour
{
    public DescribePersonality MyPersonality;

    private NpcThinking mNpcThinking;

    private NpcGoWaypoint mNpcGoWaypoint;

    private State mState;

    private Emotion mLastEmotion;

	// Use this for initialization
	void Start ()
	{
	    mNpcThinking = GetComponent<NpcThinking>();
        mNpcGoWaypoint = GetComponent<NpcGoWaypoint>();
	    mLastEmotion = mNpcThinking.Emotion;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
    if(mNpcThinking.Emotion != mLastEmotion && mNpcThinking.Emotion == Emotion.Scared)
    {
        mState = State.Running;
       RunAway();
    }
    if (mNpcThinking.Emotion == Emotion.Scared && mState == State.DoJob)
    {
        mNpcThinking.Emotion = Emotion.Happy;
        DoYourNormalJob();
    }

	    mLastEmotion = mNpcThinking.Emotion;
	}

    void RunAway()
    {
        mNpcGoWaypoint.RunAwayToNpcWithMostLeadership();
    }

    private void DoYourNormalJob()
    {
        mNpcGoWaypoint.StartCycle();
    }

    void Moved()
    {
             mState = State.DoJob;
         

    }
}
