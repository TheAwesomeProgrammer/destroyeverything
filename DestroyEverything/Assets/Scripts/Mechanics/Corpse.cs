using UnityEngine;
using System.Collections;

public class Corpse : MonoBehaviour
{
    public float DistanceToInteractWithCorpse = 1;
    public Vector3 AmoutToMakePlayerGrow;

    public GameObject Options;

    private PlayerPath mPlayerPath;

    private bool mShowOptions;

    // Use this for initialization
	void Start ()
	{
	    mPlayerPath = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPath>();
	}
	
	// Update is called once per frame
	void Update () {
     
        if(mShowOptions)
        {
            ShowOptions();
        }

	}

    void OnMouseDown()
    {
        if(mPlayerPath.GetSelectedPlayer() != null && Vector3.Distance(transform.position,mPlayerPath.GetSelectedPlayer().transform.position) < DistanceToInteractWithCorpse)
        {
            mShowOptions = true;
        }
    }

    void ShowOptions()
    {
        Options.SetActive(true);

        if(Input.GetKey(KeyCode.E))
        {
            // FEAST
            Options.SetActive(false);
            mShowOptions = false;
        }
        if (Input.GetKey(KeyCode.O))
        {
            // OBTAIN
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPath>().GetSelectedPlayer().GetComponent<Player>().Grow(AmoutToMakePlayerGrow);
            Options.SetActive(false);
            mShowOptions = false;
            Destroy(gameObject);
        }
    }

}
