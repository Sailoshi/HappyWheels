namespace Assets.Scripts
{
    using Assets.DataStructure;
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// The parallel coroutine instance information.
    /// </summary>
    public class ParallelCoroutineSupport : MonoBehaviour
    {
        public static ParallelCoroutineSupport instance;
        void Awake()
        {
            instance = this;
        }
    }

    /// <summary>
    /// Provides the functionality to execute coroutines in a parallel process.
    /// </summary>
    public static class ParallelCoroutineExt
    {
        public static RunInfo ParallelCoroutine(this IEnumerator coroutine, string group = "default")
        {
            if (!RunInfo.runners.ContainsKey(group))
            {
                RunInfo.runners[group] = new RunInfo();
            }
            var ri = RunInfo.runners[group];
            ri.count++;
            ParallelCoroutineSupport.instance.StartCoroutine(DoParallel(coroutine, ri));
            return ri;
        }
        static IEnumerator DoParallel(IEnumerator coroutine, RunInfo ri)
        {
            yield return ParallelCoroutineSupport.instance.StartCoroutine(coroutine);
            ri.count--;
        }
    }
}
