using UnityEngine;

namespace IUtils.Render.MeshInstancedRenderer
{
    public interface IInstancedRenderPropertie
    {
        public int Size();
        public void SetMaterialPropertyBlock(ref MaterialPropertyBlock mpb);
    }
}