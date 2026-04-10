using UnityEngine;

public class DragObject : MonoBehaviour
{
    public bool isReturning;
    public Vector3 originalPosition;
    public float timeReturn = 2f;
    
    private Vector3 startPosition;

    private Terrain terrain;
    private float timer;

    private void Start()
    {
        terrain = Terrain.activeTerrain;
    }

    private void ResetDrag()
    {
        isReturning = false;
        timer = 0f;
        originalPosition = Vector3.zero;
        startPosition = Vector3.zero;
    }
    public void Return()
    {
        timer = 0f;
        isReturning = true;
        startPosition = transform.position;
    }

    public void DragStart()
    {
        ResetDrag();

        originalPosition = transform.position;
    }

    public void DragEnd()
    {
        ResetDrag();
    }

    private void Update()
    {
        if(isReturning)
        {
            timer += Time.deltaTime / timeReturn;

            Vector3 newPos = Vector3.Lerp(startPosition, originalPosition, timer);
            newPos.y = terrain.SampleHeight(newPos) + transform.localScale.y/2f;
            transform.position = newPos;

            if(timer > 1f)
            {
                isReturning = false;
                transform.position = originalPosition;
                timer = 0f;
            }
        }
    }
}
