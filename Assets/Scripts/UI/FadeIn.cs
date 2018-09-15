using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeIn: MonoBehaviour {

    public float fromAlpha = 0.0f;
    public float toAlpha = 1.0f;
    public float durationSecond = 1.0f;
    public float delaySecond = 0.0f;

	void Start () {
        var image = GetComponent<Image>();
        var color = image.material.color;
        image.material.color = new Color(color.r, color.g, color.b, toAlpha);
        image.DOFade(fromAlpha, durationSecond).SetDelay(delaySecond).From();
	}
}
