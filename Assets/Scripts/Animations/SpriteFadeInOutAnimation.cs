using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFadeInOutAnimation : MonoBehaviour
{
    [SerializeField]
    private byte _lowerOpacity;
    [SerializeField]
    private byte _upperOpacity;
    [SerializeField]
    private float _duration;

    private SpriteRenderer _sprite;
    private float _timeElapsed = 0f;
    private bool _fadingIn = true;

    void OnEnable()
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
        ChangeOpacity(_lowerOpacity);
    }

    void Update()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= _duration)
        {
            _fadingIn = !_fadingIn;
            _timeElapsed -= _duration;
        }

        if (_fadingIn)
        {
            ChangeOpacity((byte)(_lowerOpacity + (_upperOpacity - _lowerOpacity) * (_timeElapsed / _duration)));
        }

        else
        {
            ChangeOpacity((byte)(_upperOpacity - (_upperOpacity - _lowerOpacity) * (_timeElapsed / _duration)));
        }
    }

    private void ChangeOpacity(byte newValue)
    {
        Color32 color = _sprite.color;
        color.a = newValue;
        _sprite.color = color;
    }
}
