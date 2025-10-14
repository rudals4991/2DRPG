using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class Selector : NodeBase
{
    private readonly List<NodeBase> child;
    private int runningIndex = -1;
    public Selector(List<NodeBase> child) => this.child = child;
    public override NodeStatus Execute()
    {
        int start = runningIndex >= 0 ? runningIndex : 0;

        for (int i = start; i < child.Count; i++)
        {
            var status = child[i].Execute();
            switch (status)
            {
                case NodeStatus.Success:
                    runningIndex = -1;          // 완료 → 다음 틱엔 처음부터
                    return NodeStatus.Success;

                case NodeStatus.Running:
                    runningIndex = i;           // 진행 중인 자식 기억
                    return NodeStatus.Running;

                case NodeStatus.Fail:
                    // 다음 자식 평가
                    continue;
            }
        }

        runningIndex = -1;                      // 전부 실패
        return NodeStatus.Fail;
    }
}
