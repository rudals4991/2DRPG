using UnityEngine;

public enum NodeStatus { Success, Fail, Running}
public abstract class NodeBase
{
    //bool�� �ƴ� NodeStatus�� ����� ����: ���ӻ��¸� ǥ���ϱ� ���ؼ�
    public abstract NodeStatus Execute();
}
