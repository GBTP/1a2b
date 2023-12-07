using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IUtils.Pool
{
    public static class PoolHelper
    {
        public static PoolSimple<T> Pool<T>(this T mono, Transform parent = null) where T : MonoBehaviour
        {
            return new PoolSimple<T>(mono, parent);
        }
    }

    //每帧调用一次渲染一次的简易对象池，特点是不用考虑回收（好耶！
    public class PoolSimple<T> where T : MonoBehaviour
    {
        private T TargetPrefab;
        public List<T> CurrentPool = new();
        private int GetIndex;
        private Transform Parent;

        private int LastGetIndex;

        public PoolSimple(T target, Transform parent)
        {
            TargetPrefab = target;
            Parent = parent;
        }

        public T TakeTarget()
        {
            GetIndex++;

            if (GetIndex > CurrentPool.Count)
            {
                CurrentPool.Add(Object.Instantiate(TargetPrefab, Parent));
            }

            var obj = CurrentPool[GetIndex - 1];
            obj.gameObject.SetActive(true);

            return obj;
        }

        //每帧调用一次就行
        public void OnUpdate()
        {
            for (var i = GetIndex; i < LastGetIndex; i++)
            {
                CurrentPool[i].gameObject.SetActive(false);
            }

            LastGetIndex = GetIndex;

            GetIndex = 0;
        }
    }
}