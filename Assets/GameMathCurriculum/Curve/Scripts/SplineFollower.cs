using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;
public class SplineFollower : MonoBehaviour
{
    public Transform mover;
    public float duration = 5f;
    private SplineContainer spline;
    private float t;

    private void Awake()
    {
        spline = GetComponent<SplineContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / duration;
        t = Mathf.Repeat(t, 1f);

        // .Spline 가장 위에 값
        // float3 <-> Vector3 간 형변환 가능
        if(!spline.Evaluate(spline.Spline, t,out float3 position, out float3 tangent, out float3 up))
        {
            return;
        }

        mover.position = position;
        if(math.length(tangent) > 0.001f) // 너무 작으면 반환. V
        {
            mover.rotation = Quaternion.LookRotation(tangent, up);

        }



    }
}
