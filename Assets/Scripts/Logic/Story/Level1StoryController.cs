using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace MiniGame
{
    public class Level1StoryController : MonoBehaviour
    {
        //场景yuansu
        [SerializeField]
        private Camera _mainCamera;
        [SerializeField]
        private Image _image1;
        [SerializeField]
        private Image _image2;
        [SerializeField]
        private Image _image3;
        [SerializeField]
        private Image _image4;

        [SerializeField]
        private Text _text1;
        [SerializeField]
        private Text _text2;
        [SerializeField]
        private Text _text3;
        [SerializeField]
        private Text _text4;
        [SerializeField]
        private Text _text5;
        [SerializeField]
        private Text _text6;
        [SerializeField]
        private Text _text7;
        [SerializeField]
        private Text _text8;

        private Color _showColor = new Color(1, 1, 1, 1);
        private Color _hideColor = new Color(0, 0, 0, 0);

        private int _shotIndex = 1;
        //控制播放完才可以点
        private bool _listenFlag;

        //镜头
        private void SceneReset()
        {
            _image1.color = _hideColor;
            _image2.color = _hideColor;
            _image3.color = _hideColor;
            _image4.color = _hideColor;
            _text1.color = _hideColor;
            _text2.color = _hideColor;
            _text3.color = _hideColor;
            _text4.color = _hideColor;
            _text5.color = _hideColor;
            _text6.color = _hideColor;
            _text7.color = _hideColor;
            _text8.color = _hideColor;
        }

        private void Shot1()
        {
            _listenFlag = false;
            SceneReset();
            _text1.DOColor(_showColor, 2.0f).OnComplete(_resetFlag);
        }

        private void Shot2()
        {
            _listenFlag = false;
            _text1.DOColor(_hideColor, 1.0f);
            _text2.DOColor(_showColor, 1.5f).OnComplete(_resetFlag);
        }

        private void Shot3()
        {
            _listenFlag = false;
            _text2.color = _hideColor;
            _mainCamera.transform.position = new Vector3(0, -2.0f, -10);
            _image1.DOColor(_showColor, 3.0f).OnComplete(_resetFlag);
        }

        private void Shot4()
        {
            _listenFlag = false;
            _text3.DOColor(_showColor, 1.5f).OnComplete(_resetFlag);
        }

        private void Shot5()
        {
            _listenFlag = false;
            _text3.DOColor(_hideColor, 1.0f);
            _mainCamera.transform.DOMove(new Vector3(0, -0, -10), 3.0f);
            _mainCamera.DOOrthoSize(7.0f, 3.0f).OnComplete(_resetFlag);
        }

        private void Shot6()
        {
            _listenFlag = false;
            SceneReset();
            _mainCamera.orthographicSize = 5.0f;
            _image2.DOColor(_showColor, 2.8f).OnComplete(_resetFlag);

        }

        private void Shot7()
        {
            _listenFlag = false;
            _text4.DOColor(_showColor, 1.0f).OnComplete(_resetFlag);
        }

        private void Shot8()
        {
            _listenFlag = false;
            SceneReset();
            _mainCamera.transform.position = new Vector3(-0.74f, 1f, -10);
            _image3.DOColor(_showColor, 2.8f).OnComplete(_resetFlag);
        }

        private void Shot9()
        {
            _listenFlag = false;
            _mainCamera.DOOrthoSize(7.0f, 3.0f);
            _mainCamera.transform.DOMove(new Vector3(0, 0, -10), 3.0f);
            _text5.DOColor(_showColor, 2.0f).OnComplete(_resetFlag);
        }

        private void Shot10()
        {
            _listenFlag = false;
            _text5.DOColor(_hideColor, 1.0f);
            _text6.DOColor(_showColor, 1.5f).OnComplete(_resetFlag);
        }

        private void Shot11()
        {
            _listenFlag = false;
            SceneReset();
            _mainCamera.orthographicSize = 3.0f;
            _mainCamera.transform.position = new Vector3(0.75f, -1.33f, -10);
            _image4.DOColor(_showColor, 1.5f).OnComplete(() =>
            {
                _text7.DOColor(_showColor, 2.0f);
                _mainCamera.transform.DOMove(new Vector3(0, -0, -10), 2.5f);
                _mainCamera.DOOrthoSize(5.0f, 2.5f).OnComplete(_resetFlag);
            });
        }

        private void Shot12()
        {
            _listenFlag = false;
            _text7.DOColor(_hideColor, 0.7f).OnComplete(() => _text8.DOColor(_showColor, 1.2f).OnComplete(_resetFlag));
        }

        //private void Shot13()
        //{
        //    _listenFlag = false;
        //    _text6.DOColor(_hideColor, 1.0f);
        //    _text7.DOColor(_showColor, 1.5f).OnComplete(_resetFlag);
        //}

        private void SceneChange()
        {
            SceneManager.UnloadScene("Level1Story");
            SceneManager.LoadSceneAsync("Level1-1");
        }

        private void _resetFlag()
        {
            _listenFlag = true;
        }
        // Use this for initialization
        void Start()
        {
            _listenFlag = true;
            SceneReset();
            Shot1();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && _listenFlag)
            {
                if (_shotIndex == 0)
                {
                    //Shot1();
                }
                else if (_shotIndex == 1)
                {
                    Shot2();
                }
                else if (_shotIndex == 2)
                {
                    Shot3();
                }
                else if (_shotIndex == 3)
                {
                    Shot4();
                }
                else if (_shotIndex == 4)
                {
                    Shot5();
                }
                else if (_shotIndex == 5)
                {
                    Shot6();
                }
                else if (_shotIndex == 6)
                {
                    Shot7();
                }
                else if (_shotIndex == 7)
                {
                    Shot8();
                }
                else if (_shotIndex == 8)
                {
                    Shot9();
                }
                else if (_shotIndex == 9)
                {
                    Shot10();
                }
                else if (_shotIndex == 10)
                {
                    Shot11();
                }
                else if (_shotIndex == 11)
                {
                    Shot12();
                }
                else if (_shotIndex == 12)
                {
                    SceneChange();
                }
                //else if (_shotIndex == 13)
                //{

                //}
                _shotIndex++;
            }
        }
    }

}
