using Unity.VisualScripting;
using UnityEngine;

public class RandomMover : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector3 p0;
    private Vector3 p1;
    private Vector3 p2;
    private Vector3 p3;

    public float range = 5f;


    private float duration;
    private float startTime;


    private void Start()
    {
        p0 = transform.position;
        p3 = range * Vector3.one;


        p1 = GetPoint(p3, true);
        p2 = GetPoint(p3, false);

        duration = Random.Range(5, 10);

        startTime = Time.time;

    }

    void Update()
    {

        // Time.time 은 인스턴스 간 공유되니까 생성 시점 빼서 맞춰주기
        float t = (Time.time - startTime) / duration;


        if(t >= 0.98f)
        {
            Destroy(gameObject);
        }

        transform.position = CubicBezier(p0, p1, p2, p3, t);

    }


    private Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        // TODO
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(d, e, t);
    }

    private Vector3 GetPoint(Vector3 endPoint, bool isLower)
    {

        float x = Random.Range(0, range);
        float y = Random.Range(0, range);
        float z = Random.Range(0, range);

        
        // 내가 자르고자 하는 평면의 법선벡터 => (1,1,1) 과 (0,0,0) 을 지나는 정육면체 기준으로 (0.5, 0.5, 0.5)
        // 평면 방정식 0.5 * (x-1) + 0.5 * (y-1) + 0.5 * (z-1) = 0 
        // 양 변에 range를 곱하면 거리 보정
        float threshold = 1.5f * range;
        float currentSum = x + y + z;



        if (isLower) // p1을 위한 영역 (합이 1.5L보다 작아야 함)
        {
            if (currentSum > threshold)
            {
                // 평면 반대쪽으로 대칭 이동
                x = range - x;
                y = range - y;
                z = range - z;
            }
        }
        else // p2를 위한 영역 (합이 1.5L보다 커야 함)
        {
            if (currentSum < threshold)
            {
                x = range - x;
                y = range - y;
                z = range - z;
            }
        }

        return new Vector3(x, y, z);

    }
}
