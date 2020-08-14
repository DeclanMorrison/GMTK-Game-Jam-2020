using UnityEngine;

public class Hose : MonoBehaviour
{
    public Rigidbody2D startPoint;
    public GameObject endPoint;
    public GameObject linkPrefab;
    public int links = 7;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRope();
    }

    void GenerateRope()
    {
        Rigidbody2D previousRB = startPoint;
        for (int i = 0; i < links; i++)
        {
            GameObject link = Instantiate(linkPrefab, transform);
            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            joint.connectedBody = previousRB;

            previousRB = link.GetComponent<Rigidbody2D>();

            if (i == links -1 )
            {
                endPoint.GetComponent<HingeJoint2D>().connectedBody = previousRB;
            }
        }
    }

}
