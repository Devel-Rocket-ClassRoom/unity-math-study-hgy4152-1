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

        p1 = new Vector3(Random.value / 2, Random.value / 2, Random.value / 2) * range;
        p2 = new Vector3(0.5f + Random.value / 2, 0.5f + Random.value / 2, 0.5f + Random.value / 2) * range;

        duration = Random.Range(3, 10);

        startTime = Time.time;

    }

    void Update()
    {

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
}
