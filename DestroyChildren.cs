using UnityEngine;

public class DestroyChildren : MonoBehaviour
{
    // 可以在Unity编辑器中设置这个时间间隔
    public float interval = 1.0f; // 每隔1秒销毁所有子物体

    private void Start()
    {
        // 开始重复调用DestroyAllChildren方法
        InvokeRepeating("DestroyAllChildren", 0, interval);
    }

    // 销毁所有子物体的方法
    void DestroyAllChildren()
    {
        // 遍历所有子物体
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject); // 销毁子物体
        }
    }
}

