/**  
* 标    题：   DrawTool.cs 
* 描    述：    
* 创建时间：   2019年05月23日 03:13 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game
{
    public class DrawTool : MonoBehaviour
    {
        public static GameObject go;
        public static MeshFilter mf;
        public static MeshRenderer mr;
        public static Shader shader;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ToDrawCircleSolid(transform, transform.localPosition, 3);
            }
        }

        private static GameObject CreateMesh(List<Vector3> vertices)
        {
            int[] triangles;
            Mesh mesh = new Mesh();
            int triangleAmount = vertices.Count - 2;
            triangles = new int[3 * triangleAmount];
            //根据三角形的个数，来计算绘制三角形的顶点顺序（索引）  
            //顺序必须为顺时针或者逆时针    
            for (int i = 0; i < triangleAmount; i++)
            {
                triangles[3 * i] = 0;//固定第一个点      
                triangles[3 * i + 1] = i + 1;
                triangles[3 * i + 2] = i + 2;
            }
            if (go == null)
            {
                go = new GameObject("mesh");
                go.transform.position = new Vector3(0, 0.1f, 0);//让绘制的图形上升一点，防止被地面遮挡  
                mf = go.AddComponent<MeshFilter>();
                mr = go.AddComponent<MeshRenderer>();
                shader = Shader.Find("Unlit/Color");
            }
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles;
            mf.mesh = mesh;
            mr.material.shader = shader;
            mr.material.color = Color.red;
            return go;
        }

        //绘制实心圆    
        public static void ToDrawCircleSolid(Transform t, Vector3 center, float radius)
        {
            int pointAmount = 100;//点的数目，值越大曲线越平滑   
            float eachAngle = 360f / pointAmount;
            Vector3 forward = t.forward;
            List<Vector3> vertices = new List<Vector3>();
            for (int i = 0; i <= pointAmount; i++)
            {
                Vector3 pos = Quaternion.Euler(0f, eachAngle * i, 0f) * forward * radius + center;
                vertices.Add(pos);
            }
            CreateMesh(vertices);
        }
    }
}