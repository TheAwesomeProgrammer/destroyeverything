using UnityEngine;
using System.Collections;

public enum Emotion
{
    Scared,
    Angry,
    Happy,
    Confused,
    Inlove,
    Sad
}

public class NpcThinking : MonoBehaviour
{

    public Emotion Emotion;

    public float ComfortDistance;

    private GameObject[] mNpcs;

	// Use this for initialization
	void Start ()
	{
	    mNpcs = GameObject.FindGameObjectsWithTag("Npc");
	}
	
	// Update is called once per frame
	void Update () {
        Comfort();
	}

    void Comfort()
    {
        foreach (GameObject tNpc in mNpcs)
        {
            if(tNpc.gameObject != gameObject)
            {
                if(Vector3.Distance(tNpc.transform.position,transform.position) < ComfortDistance)
                {
                    if(tNpc.GetComponent<NpcThinking>().Emotion == Emotion.Scared)
                    {
                        tNpc.GetComponent<NpcThinking>().Emotion = Emotion.Happy;
                    }
                }
            }
            
        }
        
    }
    
    

    void SetEmotion(Emotion pEmotion)
    {
        Emotion = pEmotion;
    }
}
