using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StoryMain : SceneMain
{
    public StoryUI storyUI;
    public RectTransform _closeUpCamera;

    // 임시 대사
    public List<string> _msgs;
    // 대사 인덱스
    private int _msgIdx;

    private bool isCloseUp = false;

    private void Start()
    {
        if(App.instance == null)
        {
            SpecDataManager.instance.onDataLoadFinished.AddListener(() =>
            {
                Init();
            });
            SpecDataManager.instance.Init(this);
        }

        DataManager.instance.Init();
        DataManager.instance.LoadUserData("0");
        DataManager.instance.UserData.gold++;
        DataManager.instance.SaveGame();
        Debug.Log(DataManager.instance.UserData.gold);
    }

    public override void Init(SceneParams param = null)
    {
        _msgIdx = 0;
        storyUI.Init();

        SpecDataManager.instance.DialogueDBDatas[0].id = 1;

    }

    private void Update()
    {
        if (isCloseUp)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (_msgIdx >= _msgs.Count)
            {
                isCloseUp = true;
                StartCoroutine(CloseUpImpl());
                return;
            }
            storyUI.SetMsg(_msgs[_msgIdx]);
            _msgIdx++;

        }
    }

    public Vector3 targetPosition;
    public float duration;

    private IEnumerator CloseUpImpl()
    {
        float delaTime = 0;
        Vector3 startPosition = _closeUpCamera.position;

        while (true)
        {
            delaTime += Time.deltaTime;
            if (delaTime > duration)
                break;

            delaTime += Time.deltaTime;
            float t = Mathf.Clamp01(delaTime / duration);
            _closeUpCamera.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        //_closeUpCamera.position = targetPosition;
        storyUI.ShowWorkBtn();
        yield return null;
    }

    public void OnClickWorkBtn()
    {
        Dispatch("onClickWorkBtn");
    }


}
