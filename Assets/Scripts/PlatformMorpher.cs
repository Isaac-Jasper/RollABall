using System.Collections;
using UnityEngine;

public class PlatformMorpher : MonoBehaviour
{
    [SerializeField]
    GameObject floor1, floor2, wall1, wall2, wall3, wall4;
    [SerializeField]
    Vector3[] dims = new Vector3[1];
    [SerializeField]
    float speed;
    

    public void Start() {
        StartCoroutine(RemoveWall(wall1, speed, dims[4]));
        StartCoroutine(RemoveWall(wall2, speed, dims[4]));
        StartCoroutine(RemoveWall(wall3, speed, dims[4]));
        StartCoroutine(RemoveWall(wall4, speed, dims[4]));
    }
    private IEnumerator TransformObject(GameObject floor, float speed, Vector3 dimensions) {
        Vector3 initialDimensions = floor.transform.localScale;
        float time = 0;
        while (floor.transform.localScale.magnitude >= dimensions.magnitude) {
            time = time + speed*Time.deltaTime;
            floor.transform.localScale = Vector3.Lerp(initialDimensions, dimensions, time);
            yield return null;
        }
        floor.transform.localScale = dimensions;
    }
    private IEnumerator RemoveWall(GameObject wall, float speed, Vector3 dimensions) {
        Vector3 initialDimensions = wall.transform.localScale;
        float time = 0;
        while (wall.transform.localScale.magnitude > dimensions.magnitude) {
            time = time + speed*Time.deltaTime;
            wall.transform.localScale = Vector3.Lerp(initialDimensions, dimensions, time);
            yield return null;
        }
        wall.SetActive(false);
    }
}
