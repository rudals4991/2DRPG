using UnityEngine;

public enum NodeStatus { Success, Fail, Running}
public abstract class NodeBase
{
    public string NodeName { get; protected set; }
    //bool�� �ƴ� NodeStatus�� ����� ����: ���ӻ��¸� ǥ���ϱ� ���ؼ�
    public abstract NodeStatus Execute();
    protected NodeStatus LogAndReturn(NodeStatus status)
    {
        //Debug.Log($"[BT] {NodeName} �� {status}");
        return status;
    }
}
