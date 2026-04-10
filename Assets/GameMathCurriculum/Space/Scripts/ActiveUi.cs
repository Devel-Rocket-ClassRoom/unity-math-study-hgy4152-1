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
            float finalX = Mathf.Clamp(screenPoint.x, margin, Screen.width - margin);
            float finalY = Mathf.Clamp(screenPoint.y, margin, Screen.height - margin);

            if (screenPoint.z < 0)
            {
                finalY = margin;
                
            }


            ui.rectTransform.position = new Vector3(finalX, finalY, 0);
        }
        else
        {
            ui.enabled = false;
        }
    }
}
