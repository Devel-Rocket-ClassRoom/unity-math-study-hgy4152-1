using UnityEngine;

public class SpawnSphere : MonoBehaviour
{
    public GameObject Sphere;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject go = Instantiate(Sphere, transform.position, transform.rotation);

            SetColor(go);
            
        }
    }

    private void SetColor(GameObject go)
    {
        // Trail 설정
        TrailRenderer tr = go.GetComponent<TrailRenderer>();

        // 1. 그라데이션 객체 생성
        Gradient gradient = new Gradient();

        // 2. 색상 키 설정 (색상 값)
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        // 표면 색 설정
        go.GetComponent<MeshRenderer>().material.color = randomColor;

        // GradientColorKey(색상, 위치0~1)
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0] = new GradientColorKey(randomColor, 0.0f); 
        colorKeys[1] = new GradientColorKey(randomColor, 1.0f);  

        // 3. 알파 키 설정 (투명도)
        // GradientAlphaKey(알파값0~1, 위치0~1)
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = new GradientAlphaKey(1.0f, 0.0f); // 시작은 불투명
        alphaKeys[1] = new GradientAlphaKey(0.0f, 1.0f); // 끝은 투명

        // 4. 그라데이션 객체에 키값들 적용
        gradient.SetKeys(colorKeys, alphaKeys);
        tr.colorGradient = gradient;
    }
}
