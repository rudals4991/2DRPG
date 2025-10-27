using UnityEngine;

public enum NodeStatus { Success, Fail, Running}
public abstract class NodeBase
{
    public string NodeName { get; protected set; }
    //bool이 아닌 NodeStatus를 사용한 이유: 지속상태를 표현하기 위해서
    public abstract NodeStatus Execute();
    protected NodeStatus LogAndReturn(NodeStatus status)
    {
        //Debug.Log($"[BT] {NodeName} → {status}");
        return status;
    }
}
