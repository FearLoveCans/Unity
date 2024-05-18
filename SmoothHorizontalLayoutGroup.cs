using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SmoothHorizontalLayoutGroup : MonoBehaviour
{
    [Header("Layout Settings")]
    public float spacing = 10f; // 子物体之间的间距
    public HorizontalAlignmentOptions alignment = HorizontalAlignmentOptions.Middle; // 对齐方式

    [Header("Animation Settings")]
    public float smoothSpeed = 5f; // 平滑移动速度

    public List<RectTransform> children;
    private RectTransform parentRectTransform;



    private void Start()
    {
        parentRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        GetChildRects();
        SmoothLayout();
    }

  
    private void GetChildRects()
    {
        children.Clear();
        foreach (RectTransform child in transform)
        {
            if (child != null)
            {
                children.Add(child);
            }
        }
    }

    private void SmoothLayout()
    {
        if (children == null || children.Count == 0)
            return;

        float totalWidth = 0f;

        // 计算所有子物体的总宽度
        foreach (RectTransform child in children)
        {
            totalWidth += child.rect.width;
        }

        // 添加间距
        totalWidth += (children.Count - 1) * spacing;

        // 计算起始位置
        float startX = 0f;

        if (alignment == HorizontalAlignmentOptions.Left)
        {
            startX = -parentRectTransform.rect.width / 2 + children[0].rect.width / 2;
        }
        else if (alignment == HorizontalAlignmentOptions.Middle)
        {
            startX = -totalWidth / 2;
        }
        else if (alignment == HorizontalAlignmentOptions.Right)
        {
            startX = parentRectTransform.rect.width / 2 - totalWidth;
        }

        // 调整每个子物体的位置
        for (int i = 0; i < children.Count; i++)
        {
            RectTransform child = children[i];
            float targetX = startX + (child.rect.width / 2) + i * (child.rect.width + spacing);
            Vector2 targetPosition = new Vector2(targetX, child.anchoredPosition.y);

            // 使用Lerp进行平滑移动
            child.anchoredPosition = Vector2.Lerp(child.anchoredPosition, targetPosition, Time.deltaTime * smoothSpeed);
        }
    }
}

public enum HorizontalAlignmentOptions
{
    Left,
    Middle,
    Right
}
