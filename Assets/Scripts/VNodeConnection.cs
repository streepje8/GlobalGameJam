using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VNodeConnection : MonoBehaviour
{
    public IOComponent start;
    public IOComponent end;
    //public int segments = 20;
    //public float bezierboost = 0.2f;
    public float GUIDistance = 0.2f;
    private LineRenderer rend;
    private Connection connection;
    private void Awake()
    {
        rend = GetComponent<LineRenderer>();
    }

    public void Init(Connection c)
    {
        connection = c;
        start.SetConnection(this);
        end.SetConnection(this);
    }

    private void Update()
    {
        //Vector3[] positions = new Vector3[segments];
        //ThreeDBezier(ref positions, start.transform.localPosition, end.transform.localPosition, segments);
        //rend.positionCount = segments;
        rend.SetPositions(new Vector3[] { start.cirkel.transform.position - transform.rotation * Vector3.forward * GUIDistance,end.cirkel.transform.position - transform.rotation * Vector3.forward * GUIDistance});
    }

    /*
    private void ThreeDBezier(ref Vector3[] positions, Vector3 startpos, Vector3 endpos, int segmentCount)
    {
        Vector3 startToEndVector = -(endpos - startpos).normalized;
        startToEndVector.y = 0;
        for (int i = 0; i < segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            positions[i] = CalculateCubicBezierPoint(t,startpos,startpos + startToEndVector * bezierboost, endpos - startToEndVector * bezierboost,endpos);
            positions[i].z = endpos.z;
        }
    }
    
    // https://www.gamedeveloper.com/business/how-to-work-with-bezier-curve-in-games-with-unity
    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
        
        Vector3 p = uuu * p0; 
        p += 3 * uu * t * p1; 
        p += 3 * u * tt * p2; 
        p += ttt * p3; 
        
        return p;
    }
    */
    public void Disconnect(bool isOutput = false)
    {
        if (!isOutput || !end.node.isLocked)
        {
            connection.Disconnect(start.node);
            start.SetConnected(false);
            end.SetConnected(false);
            Destroy(gameObject);
        }
    }
}
