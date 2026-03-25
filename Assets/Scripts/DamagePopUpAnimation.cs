using UnityEngine;
using TMPro;

public class DamagePopUpAnimation : MonoBehaviour
{
    TextMeshProUGUI tmp;
    public AnimationCurve opacityCurve;
    public AnimationCurve scaleCurve; 
    public AnimationCurve heightCurve; //Bổ sung

    float time = 0;
    Vector3 origin; //Bổ sung

    void Awake()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        origin = transform.position;
    }

    void Update()
    {
        time += Time.deltaTime;

        //Opacity
        Color color = tmp.color;
        color.a = opacityCurve.Evaluate(time);
        tmp.color = color;

        //Scale
        transform.localScale = Vector3.one * scaleCurve.Evaluate(time);

        //Move
        transform.position = origin + new Vector3(0, heightCurve.Evaluate(time), 0);
    }
}
