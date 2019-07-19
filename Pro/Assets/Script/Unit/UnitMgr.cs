/**  
* 标    题：   UnitMgr.cs 
* 描    述：    
* 创建时间：   2018年03月06日 01:35 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
    public partial class UnitMgr
    {
        public static readonly UnitMgr Instance = new UnitMgr();

        public Dictionary<UInt32, UnitBase> UnitDic = new Dictionary<UInt32, UnitBase>();

        private UnitPlayer mPlayer;
        public UnitPlayer Player { get { return mPlayer; } }

#if UNITY_EDITOR
        public UInt32 TempID;
        public string ModPath;
#endif

        public UnitPlayer CreatePlayer(UInt32 careerId, String Name, Vector3 Pos)
        {
            CareerTemp temp = CareerTempMgr.Instance.Find(careerId);
            if (temp == null)
            {
                MessageBox.Error(string.Format("职业配置表ID[{0}]为空！！", careerId));
                return null;
            }

            mPlayer = (UnitPlayer)CreateUnit(temp.modId, UnitType.Player);
            if (mPlayer == null) return null;
            mPlayer.UpdatTemp(temp);
            mPlayer.UpdatePos(Pos);
            if (CameraMgr.Main != null) CameraMgr.UpdatePlay(mPlayer.Trans);
            EventMgr.Instance.Trigger(EventKey.CreateUnit, mPlayer);
            return mPlayer;
        }

        public Unit CreateUnit(UInt32 modId, UnitType type)
        {
            ModelTemp temp = ModelTempMgr.Instance.Find(modId);
            if (temp == null)
            {
                MessageBox.Error(string.Format("模型配置表ID[{0}]为空！！", modId));
                return null;
            }
            string mod = temp.model;
            if (string.IsNullOrEmpty(mod))
            {
                MessageBox.Error(string.Format("模型配置表ID[{0}]中模型字段为空！！", modId));
                return null;
            }
#if UNITY_EDITOR
            string rPath = "Unit/" + temp.model;
            UnityEngine.Object prefab = Resources.Load(rPath);
            if(prefab == null)
            {
                MessageBox.Error(string.Format("error:{0} 未读取到", rPath));
                return null;
            }
            GameObject go = GameObject.Instantiate(prefab as GameObject);
            string path = UnityEditor.AssetDatabase.GetAssetPath(prefab);
            ModPath = System.IO.Path.GetDirectoryName(path);
#else
            GameObject go = AssetsMgr.Instance.LoadPrefab(mod);
#endif

            if (go == null) return null;
            go.transform.localEulerAngles = Vector3.zero;
            go.transform.localScale = Vector3.one;
            Unit unit = CreateClass(type);
            unit.UpdateModel(go);
            unit.UpdateModelTemp(temp);
            unit.UpdateAnims(temp);
            return unit;
        }

        public Unit CreateClass(UnitType type)
        {
            switch (type)
            {
                case UnitType.Player:
                    return new UnitPlayer();
                    break;
            }
            return new Unit();
        }
    }
}