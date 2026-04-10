using UnityEngine;
using UnityEngine.UI;

public class ActiveUi : MonoBehaviour
{
    public Transform Cube;

    private Camera cam;

    private Image ui;

    private float margin = 50f;
    private void Start()
    {
        cam = Camera.main;
        ui = GetComponent<Image>();
    }

    private void Update()
    {
        Vector3 screenPoint = cam.WorldToScreenPoint(Cube.position);

        if (screenPoint.z < 0)
        {
            screenPoint.x = Screen.width - screenPoint.x;
            screenPoint.y = Screen.height - screenPoint.y;
        }


        bool isOut = screenPoint.x < 0 || screenPoint.x > Screen.width ||
                     screenPoint.y < 0 || screenPoint.y > Screen.height ||
                     screenPoint.z < 0;


        if (isOut)
        {
            ui.enabled = true;

            // x와 y가 동시에 나갔을 때 둘 다 고정해야 오류 안남
            // else if로 했을 때 카메라 돌리니까 이상하게 나옴
            // 근데 아직 x값에 따른 위치가 애매함
            float finalX = Mathf.Clamp(screenPoint.x, margin, Screen.width - margin);
            float finalY = Mathf.Clamp(screenPoint.y, margin, Screen.height - margin);

            if (screenPoint.z < 0)
            {
                finalY = margin;
                
            }


            ui.rectTransform.position = new Vector3(finalX, finalY, 0);


            // z가 0~1 사이면 값이 배수가 돼서 이상하게 나옴.
            
            Vector3 local = cam.transform.InverseTransformPoint(screenPoint);
            Vector2 dir = new Vector2(local.x, local.y).normalized;
            Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

            // 중앙으로부터 더 가까운 점 찾기
            float scale = Mathf.Min(center.x / Mathf.Abs(dir.x), center.y / Mathf.Abs(dir.y)); 


            Vector2 pos = center + dir * scale;
            
            ui.transform.position = pos;
            
            
        }
        else
        {
            ui.enabled = false;
        }


        
    }
}
