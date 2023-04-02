using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DataManager
{
    public static readonly DataManager instance = new DataManager();

    public UnityEvent<string, float> onDataLoadComplete = new UnityEvent<string, float>();
    public UnityEvent onDataLoadFinished = new UnityEvent();
    
    //private Dictionary<int, RawData> dicDatas = new Dictionary<int, RawData>();
    //private List<DatapathData> dataPathList;

    private DataManager() 
    {
    }

    public void Init()
    {
        //var datapath = "Datas/datapath_data";
        //var asset = Resources.Load<TextAsset>(datapath);
        //var json = asset.text;
        //var datas = JsonConvert.DeserializeObject<DatapathData[]>(json);

        //this.dataPathList = datas.ToList();
    }
    //public void LoadData<T>(string json) where T : RawData
    //{
    //    var datas = JsonConvert.DeserializeObject<T[]>(json);
    //    datas.ToDictionary(x => x.id).ToList().ForEach(x => dicDatas.Add(x.Key, x.Value));
    //}

    public void LoadAllData()
    {
        App.instance.StartCoroutine(LoadAllDataRoutine());
    }
    public void LoadAllData(MonoBehaviour mono)
    {
        mono.StartCoroutine(LoadAllDataRoutine());
    }
    private IEnumerator LoadAllDataRoutine()
    {
        int idx = 0;
        // 데이터 없을때 테스트 용도
        for (int i = 0; i < 10; i++)
        {
            this.onDataLoadComplete.Invoke("호출할 데이터 없음", (float)(i + 1) / 10);
            yield return YieldInstructionCache.WaitForSeconds(0.1f);
        }

        //foreach (var data in this.dataPathList)
        //{
        //    var path = string.Format("Datas/{0}", data.res_name);
        //    ResourceRequest req = Resources.LoadAsync<TextAsset>(path);
        //    yield return req;
        //    float progress = (float)(idx + 1) / this.dataPathList.Count;
        //    this.onDataLoadComplete.Invoke(data.res_name, progress);
        //    TextAsset asset = (TextAsset)req.asset;
        //    var typeDef = Type.GetType(data.type);
        //    this.GetType().GetMethod(nameof(LoadData))
        //        .MakeGenericMethod(typeDef).Invoke(this, new string[] { asset.text });

        //    yield return YieldInstructionCache.WaitForSeconds(0.3f);
        //    idx++;
        //}
        yield return null;
        this.onDataLoadFinished.Invoke();
    }

}