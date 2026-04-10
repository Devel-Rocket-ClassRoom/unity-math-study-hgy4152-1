using UnityEngine;

public class DropPoint : MonoBehaviour
{
    private void Start()
    {
        Vector3 pos = transform.position;
        float terrainHeight = Terrain.activeTerrain.SampleHeight(pos);
        float finalHeight = terrainHeight + Terrain.activeTerrain.transform.position.y;

        pos.y = finalHeight + 10f;

        transform.position = pos;
        
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Cube"))
        {
            Mouse mouse = other.GetComponent<Mouse>();

            other.transform.position = transform.position;
            mouse.isDrop = true;
            Debug.Log("접촉");
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Cube"))
        {
            Mouse mouse = other.GetComponent<Mouse>();

            mouse.isDrop = false;
        }
    }

}
