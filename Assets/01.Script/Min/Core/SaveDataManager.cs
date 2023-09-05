using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataManager : MonoSingleTon<SaveDataManager>
{
    //private PlayerData _playerData;
    //public PlayerData PlayerData => _playerData;

    //private MapData _mapData;
    //public MapData MapData => _mapData;

    //private void Awake()
    //{
    //    LoadPlayerData();
    //    LoadMapData();
    //}

    //public void LoadPlayerData()
    //{
    //    string path = Application.dataPath + "/Save/PlayerData.json";
    //    if (File.Exists(path))
    //    {
    //        string json = File.ReadAllText(path);
    //        _playerData = Newtonsoft.Json.JsonConvert.DeserializeObject<PlayerData>(json);
    //    }
    //    else
    //    {
    //        if (_playerData == null)
    //        {
    //            _playerData = new();
    //        }
    //        File.WriteAllText(path, Newtonsoft.Json.JsonConvert.SerializeObject(_playerData));
    //    }
    //}
    //public void SavePlayerData()
    //{
    //    string path = Application.dataPath + "/Save/PlayerData.json";
    //    string json = Newtonsoft.Json.JsonConvert.SerializeObject(_playerData);

    //    File.WriteAllText(path, json);
    //}


    //public void LoadMapData()
    //{
    //    string path = Application.dataPath + "/Save/MapData.json";
    //    if (File.Exists(path))
    //    {
    //        string json = File.ReadAllText(path);
    //        _mapData = Newtonsoft.Json.JsonConvert.DeserializeObject<MapData>(json);
    //    }
    //    else
    //    {
    //        if (_mapData == null)
    //        {
    //            _mapData = new();
    //        }

    //        if (_mapData.mapNodeList == null)
    //        {
    //            _mapData.mapNodeList = new();
    //        }
    //        File.WriteAllText(path, Newtonsoft.Json.JsonConvert.SerializeObject(_mapData));
    //    }
    //}
    //public void SaveMapData()
    //{
    //    string path = Application.dataPath + "/Save/MapData.json";
    //    string json = Newtonsoft.Json.JsonConvert.SerializeObject(_mapData);

    //    File.WriteAllText(path, json);
    //}

}
