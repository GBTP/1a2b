using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace IUtils.Render.MeshInstancedRenderer
{
    public class MeshInstancedRenderer<T> where T : struct, IInstancedRenderPropertie
    {
        private readonly bool m_UseInstanced;

        private readonly int m_MaxCount;
        private int m_Count;
        private readonly Material m_Material;
        private readonly Mesh m_Mesh;
        private readonly Camera m_TargetCamera;
        private readonly int m_Layer;

        private readonly bool m_Reverse;

        private readonly static int m_PropertiesShaderId = Shader.PropertyToID("_Properties");
        private readonly Bounds m_Bounds;

        private readonly uint[] m_Args;
        private readonly ComputeBuffer m_ArgsBuffer;
        private readonly T[] m_Properties;
        private readonly ComputeBuffer m_MeshPropertiesBuffer;
        private MaterialPropertyBlock m_MaterialPropertyBlock;

        public MeshInstancedRenderer(Material material, Mesh mesh, int layer, int maxCount = 1024, Vector3 centerPoint = default, Vector3 range = default, Camera target = null, bool useInstanced = true, bool reverse = false)
        {
            m_MaxCount = maxCount;
            m_Material = material;
            m_Mesh = mesh;
            m_TargetCamera = target;
            m_Layer = layer;
            m_Reverse = reverse;
            m_MaterialPropertyBlock = new MaterialPropertyBlock();
            m_Properties = new T[m_MaxCount];

            m_UseInstanced = useInstanced;

            if (m_UseInstanced)
            {
                m_Bounds = new Bounds(centerPoint, range);
                //每个实例的索引数、实例数、起始索引位置、基顶点位置、起始实例位置
                m_Args = new uint[5];
                m_Args[0] = m_Mesh.GetIndexCount(0);
                m_Args[2] = m_Mesh.GetIndexStart(0);
                m_Args[3] = m_Mesh.GetBaseVertex(0);
                m_ArgsBuffer = new ComputeBuffer(1, m_Args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
                m_MeshPropertiesBuffer = new ComputeBuffer(m_MaxCount, m_Properties[0].Size());
            }
        }

        public bool AddDrawCall(T p)
        {
            if (m_Count == m_MaxCount) return false;

            m_Properties[m_Count] = p;
            m_Count++;

            return true;
        }

        public void Draw()
        {
            //没东西为何不润？
            if (m_Count == 0) return;

            //反转的话先提交的最后渲染
            if (m_Reverse) Array.Reverse(m_Properties, 0, m_Count);

            if (m_UseInstanced)
            {
                //首先用实际数量更新渲染数量
                m_Args[1] = (uint)m_Count;
                m_ArgsBuffer.SetData(m_Args);

                //用数据更新数据（？）
                m_MeshPropertiesBuffer.SetData(m_Properties);
                m_MaterialPropertyBlock.SetBuffer(m_PropertiesShaderId, m_MeshPropertiesBuffer);

                //Graphics.DrawMeshInstancedProcedural(m_Mesh, 0, m_Material, m_Bounds, m_Count, m_MaterialPropertyBlock, UnityEngine.Rendering.ShadowCastingMode.Off, false, 0, null, UnityEngine.Rendering.LightProbeUsage.Off, null);
                Graphics.DrawMeshInstancedIndirect(m_Mesh, 0, m_Material, m_Bounds, m_ArgsBuffer, 0, m_MaterialPropertyBlock, UnityEngine.Rendering.ShadowCastingMode.Off, false, m_Layer, m_TargetCamera, UnityEngine.Rendering.LightProbeUsage.Off, null);
            }
            else
            {
                //这里就是普通的渲染路径了
                for (var i = 0; i < m_Count; i++)
                {
                    m_Properties[i].SetMaterialPropertyBlock(ref m_MaterialPropertyBlock);
                    //乱用api.jpg，但是确实有用（确保下一个能盖在上一个上面就行
                    Graphics.DrawMesh(m_Mesh, new Vector3(0f, 0f, 100f - 0.001f * i), Quaternion.identity, m_Material, m_Layer, m_TargetCamera, 0, m_MaterialPropertyBlock, false, false, false);
                }
            }

            //渲染数目清零！
            m_Count = 0;
        }

        public void Dispose()
        {
            m_MaterialPropertyBlock.Clear();

            if (m_UseInstanced)
            {
                m_ArgsBuffer.Dispose();
                m_MeshPropertiesBuffer.Dispose();
            }
        }
    }
}