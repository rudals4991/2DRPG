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
                    runningIndex = -1;          // �Ϸ� �� ���� ƽ�� ó������
                    return NodeStatus.Success;

                case NodeStatus.Running:
                    runningIndex = i;           // ���� ���� �ڽ� ���
                    return NodeStatus.Running;

                case NodeStatus.Fail:
                    // ���� �ڽ� ��
                    continue;
            }
        }

        runningIndex = -1;                      // ���� ����
        return NodeStatus.Fail;
    }
}
