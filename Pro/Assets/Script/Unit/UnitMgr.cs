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

        private List<UnitBase> mArray = new List<UnitBase>();

#if UNITY_EDITOR
        public UInt32 TempID;
        public string ModPath;
#endif

        public void CreateUnit(UInt32 careerId, Action<Unit> callback = null)
        {
            CareerTemp temp = CareerTempMgr.Instance.Find(careerId);
            if (temp == null)
            {
                MessageBox.Error(string.Format("职业配置表ID[{0}]为空！！", careerId));
                return;
            }
            CreateUnit(temp, temp.modId, callback);
        }

        public void CreateUnit(CareerTemp temp, UInt32 modId, Action<Unit> callback = null)
        {
            ModelTemp modTemp = ModelTempMgr.Instance.Find(modId);
            if (modTemp == null)
            {
                MessageBox.Error(string.Format("模型配置表ID[{0}]为空！！", modId));
                return;
            }
            Unit unit = new Unit();
            unit.UpdateCfgs(temp, modTemp);
            callback(unit);
        }
        private void OnLoadFinish(object obj)
        {
            if (obj == null)
            {
                MessageBox.Error(string.Format("error:{0} 未读取到", obj.ToString()));
                return;
            }
            GameObject go = obj as GameObject;
            string path = UnityEditor.AssetDatabase.GetAssetPath(go);
            ModPath = System.IO.Path.GetDirectoryName(path);

            if (go == null) return;
            go.transform.localEulerAngles = Vector3.zero;
            go.transform.localScale = Vector3.one;
        }

        //         public UnitPlayer CreatePlayer(UInt32 careerId, String Name, Vector3 Pos)
        //         {
        //             CareerTemp temp = CareerTempMgr.Instance.Find(careerId);
        //             if (temp == null)
        //             {
        //                 MessageBox.Error(string.Format("职业配置表ID[{0}]为空！！", careerId));
        //                 return null;
        //             }
        // 
        //             mPlayer = (UnitPlayer)CreateUnit(temp.modId, UnitType.Player);
        //             if (mPlayer == null) return null;
        //             mPlayer.UpdatTemp(temp);
        //             mPlayer.UpdatePos(Pos);
        //             RemoteControl.Instance.AddCommand(temp.id, new MoveCommand(mPlayer), new AttackCommand(mPlayer));
        //             if (CameraMgr.Main != null) CameraMgr.UpdatePlay(mPlayer.Trans);
        //             EventMgr.Instance.Trigger(EventKey.CreateUnit, mPlayer);
        //             return mPlayer;
        //         }
        // 
        //         public void CreateUnit(UInt32 modId, UnitType type)
        //         {
        //             
        //
        //         }
        // 
        //         
        // 
        //         public Unit CreateClass(UnitType type)
        //         {
        //             switch (type)
        //             {
        //                 case UnitType.Player:
        //                     return new UnitPlayer();
        //                     break;
        //             }
        //             return new Unit();
        //         }
        // 
        // #if UNITY_EDITOR
        //         public Unit FindToTag(string tag)
        //         {
        //             foreach(Unit unit in mUnitDic.Values)
        //             {
        //                 if (unit.Tag.Contains(tag)) return unit;
        //             }
        //             return null;
        //         }
        // #endif
    }
}