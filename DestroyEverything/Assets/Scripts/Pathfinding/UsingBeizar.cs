using UnityEngine;



public class UsingBeizar : MonoBehaviour
{
    public BeizarCurves myBezier;
    public GameObject[] mPositions;

    private float t = 0f;



    void Start()
    {

        myBezier = GetComponent<BeizarCurves>();
        myBezier.Init(mPositions[0].transform.position, mPositions[1].transform.position, mPositions[2].transform.position, mPositions[3].transform.position);

    }



    void Update()
    {

        Vector3 vec = myBezier.GetPointAtTime(t);

        transform.position = vec;



        t += 0.01f;

        if (t > 1f)

            t = 0f;

    }

}