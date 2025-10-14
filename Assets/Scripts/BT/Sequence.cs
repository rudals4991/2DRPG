using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class Sequence : NodeBase
{
    private readonly List<NodeBase> child;
    private int currentIndex = 0;
    public Sequence(List<NodeBase> child) => this.child = child;
    public override NodeStatus Execute()
    {
        while (currentIndex < child.Count)
        {
            var status = child[currentIndex].Execute();

            switch (status)
            {
                case NodeStatus.Success:
                    currentIndex++;                 // 다음 자식으로
                    continue;

                case NodeStatus.Running:
                    return NodeStatus.Running;      // 현재 자식 진행 중

                case NodeStatus.Fail:
                    currentIndex = 0;               // 실패 → 리셋
                    return NodeStatus.Fail;
            }
        }

        // 모든 자식 성공
        currentIndex = 0;
        return NodeStatus.Success;
    }
}
