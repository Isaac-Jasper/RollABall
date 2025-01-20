using System.Collections;
using UnityEngine;

public class PlatformMorpher : MonoBehaviour
{
    [SerializeField]
    private LayoutCollection layoutCollection;
    [SerializeField]
    int initLayout;
    [SerializeField]
    float speed, tolerance;
    

    public void Start() {
        MorphToLayout(initLayout);
    }

    private void MorphToLayout(int layout) {
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
