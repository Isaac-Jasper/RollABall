using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class PlatformMorpher : MonoBehaviour
{
    [SerializeField]
    public LayoutCollection layoutCollection;
    [SerializeField]
    private GameObject collectable, collectableAreaParent;
    private Transform[] collectableAreas;
    [SerializeField]
    private float speed, tolerance;
    
    public void Awake() {
        collectableAreas = collectableAreaParent.GetComponentsInChildren<Transform>(true);
    }

    public void MorphToRandomLayout() {
        int rand = Random.Range(0, layoutCollection.layouts.Count);
        MorphToLayout(rand);
    }

    private IEnumerator MoveCollectable() {
        float[] threshhold = new float[collectableAreas.Length-1]; //the 0 index of collectable areas is parent
        float TotalVolume = 0;
        for (int i = 1; i < collectableAreas.Length; i++) {
            Vector3 s = collectableAreas[i].localScale;
            TotalVolume += s.x*s.y*s.z;
            threshhold[i-1] = TotalVolume;
        }
        float rand = Random.Range(0f, TotalVolume);
        int randomArea = 0;
        for (int i = 1; i < threshhold.Length; i++) {
            if (rand > threshhold[i-1] && rand < threshhold[i])  {
                randomArea = i;
                break;
            }
        }


        Vector3 pos = collectableAreas[randomArea+1].position; //the 0 index of collectable areas is parent
        Vector3 scale = collectableAreas[randomArea+1].localScale;

        float randomX = Random.Range(pos.x - scale.x/2, pos.x + scale.x/2);
        float randomY = Random.Range(pos.y - scale.y/2, pos.y + scale.y/2);
        float randomZ = Random.Range(pos.z - scale.z/2, pos.z + scale.z/2);

        Vector3 initPos = collectable.transform.position;
        Vector3 newPos = new Vector3(randomX, randomY, randomZ);
        Vector3 initDim = collectable.transform.localScale;
        collectable.transform.localScale = Vector3.zero;

        collectable.transform.position = newPos;
        collectable.SetActive(true);

        float time = 0;

        while ( Vector3.SqrMagnitude(collectable.transform.position - newPos) > tolerance) {
            time += speed*Time.deltaTime;
            //collectable.transform.position = Vector3.Lerp(initPos, newPos, time);
            collectable.transform.localScale = Vector3.Lerp(Vector3.zero, initDim, time);
            yield return null;
        }
        collectable.transform.localScale = initDim;
    }

    public void MorphToLayout(int layout) {
        StopAllCoroutines();
        ObjectLayout l = layoutCollection.layouts[layout];

        for (int i = 0; i < layoutCollection.objects.Count; i++) {
            GameObject obj = layoutCollection.objects[i];
            if (obj == null)  continue;
            
            switch (obj.tag) {
                case "InstantMorph":
                    obj.transform.localScale = l.Scales[i];
                    obj.transform.position = l.Positions[i];
                break;
                case "InstantMove" :
                    StartCoroutine(TransformObjectInstantMove(obj, speed, l.Scales[i], l.Positions[i]));
                break;
                default:
                    StartCoroutine(TransformObject(obj, speed, l.Scales[i], l.Positions[i]));
                break;
            }
        }
        StartCoroutine(MoveCollectable());
    }
    private IEnumerator TransformObject(GameObject obj, float speed, Vector3 scaleDim, Vector3 pos) {
        Vector3 initDim = obj.transform.localScale;
        Vector3 initPos = obj.transform.position;
        float time = 0;
        if (initDim != Vector3.zero) {
            obj.SetActive(true);
        }
        
        while (Vector3.SqrMagnitude(obj.transform.localScale - scaleDim) > tolerance || Vector3.SqrMagnitude(obj.transform.position - pos) > tolerance) {
            time += speed*Time.deltaTime;
            obj.transform.localScale = Vector3.Lerp(initDim, scaleDim, time);
            obj.transform.position = Vector3.Lerp(initPos, pos, time);
            yield return null;
        }

        if (scaleDim == Vector3.zero)
            obj.SetActive(false);
        else {
            obj.transform.localScale = scaleDim;
            obj.transform.position = pos;
        }
    }

    private IEnumerator TransformObjectInstantMove(GameObject obj, float speed, Vector3 scaleDim, Vector3 pos) {
        Vector3 initDim = obj.transform.localScale;
        float time = 0;
        while (Vector3.SqrMagnitude(obj.transform.localScale) > tolerance) {
            time += 2*speed*Time.deltaTime;
            obj.transform.localScale = Vector3.Lerp(initDim, Vector3.zero, time);
            yield return null;
        }
        obj.SetActive(false);

        obj.transform.position = pos;
        if (scaleDim == Vector3.zero) yield break;

        obj.SetActive(true);
        time = 0;
        while (Vector3.SqrMagnitude(obj.transform.localScale - scaleDim) > tolerance) {
            time += 2*speed*Time.deltaTime;
            obj.transform.localScale = Vector3.Lerp(Vector3.zero, scaleDim, time);
            yield return null;
        }

        obj.transform.localScale = scaleDim;
    }
}
