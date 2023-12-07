using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//池化自动扩充的Mesh实例化渲染器
namespace IUtils.Render.MeshInstancedRenderer
{
    public class MeshInstancedRendererPool<T> where T : struct, IInstancedRenderPropertie
    {
        private readonly int m_EachSize;
        private readonly bool m_UseInstanced;
        private int m_Count;
        private readonly Material m_Material;
        private readonly Mesh m_Mesh;

        private readonly Camera m_TargetCamera;
        private readonly int m_Layer;

        private readonly bool m_Reverse;

        private Vector3 m_CenterPoint;
        private Vector3 m_Range;

        private readonly List<MeshInstancedRenderer<T>> m_Pool;

        public MeshInstancedRendererPool(Material material, Mesh mesh, int layer, int eachSize = 1024, Vector3 centerPoint = default, Vector3 range = default, Camera target = null, bool useInstanced = true, bool reverse = false)
        {
            m_UseInstanced = useInstanced;

            m_Material = material;
            m_Mesh = mesh;
            m_TargetCamera = target;
            m_Layer = layer;
            m_EachSize = eachSize;
            m_Reverse = reverse;

            m_CenterPoint = centerPoint;
            m_Range = range;

            //new池子并塞一个renderer进去
            m_Pool = new();
            ExpandPool();
        }

        private void ExpandPool()
        {
            m_Pool.Add(new MeshInstancedRenderer<T>(m_Material, m_Mesh, m_Layer, m_EachSize, m_CenterPoint, m_Range, m_TargetCamera, m_UseInstanced, m_Reverse));
        }

        public void AddDrawCall(T p)
        {
            //加不进去，得扩充或者去下一个了
            if (!m_Pool[m_Count].AddDrawCall(p))
            {
                m_Count++;
                if (m_Count == m_Pool.Count) ExpandPool();

                m_Pool[m_Count].AddDrawCall(p);
            }
        }

        public void Draw()
        {
            //反转的话先提交的最后渲染
            if (m_Reverse)
            {
                for (var i = m_Count; i >= 0; i--)
                {
                    m_Pool[i].Draw();
                }
            }
            else
            {
                for (var i = 0; i <= m_Count; i++)
                {
                    m_Pool[i].Draw();
                }
            }

            m_Count = 0;
        }

        public void Dispose()
        {
            //Object.Destroy(m_Material);
            Object.Destroy(m_Mesh);

            foreach (var renderer in m_Pool)
            {
                renderer.Dispose();
            }
            m_Pool.Clear();
        }
    }
}