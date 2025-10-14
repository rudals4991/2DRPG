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
                    currentIndex++;                 // ���� �ڽ�����
                    continue;

                case NodeStatus.Running:
                    return NodeStatus.Running;      // ���� �ڽ� ���� ��

                case NodeStatus.Fail:
                    currentIndex = 0;               // ���� �� ����
                    return NodeStatus.Fail;
            }
        }

        // ��� �ڽ� ����
        currentIndex = 0;
        return NodeStatus.Success;
    }
}
