using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeOut: MonoBehaviour {

    public float fromAlpha = 1.0f;
    public float toAlpha = 0.0f;
    public float durationSecond = 1.0f;
    public float delaySecond = 0.0f;

    public bool destroyOnComplete = false;

    private Image _image;

	void Start () {
        _image = GetComponent<Image>();
        var color = _image.material.color;
        _image.material.color = new Color(color.r, color.g, color.b, fromAlpha);
        _image.DOFade(toAlpha, durationSecond).SetDelay(delaySecond).OnComplete(() =>
        {
            if (destroyOnComplete)
            {
                Destroy(_image);
            }
        });
	}
}
