using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("�⺻ ����")]
    public CharacterType CharacterType; // ����
    public string Name;                 // �̸�

    [Header("����")]
    public int MaxHp;                   // �ִ�ü��
    public int NowHp;                   // ����ü��
    public float MoveSpeed;             // �̵��ӵ�
    public float AttackSpeed;           // ���ݼӵ�
    public float AttackDamage;          // �⺻ ���� ������
    public float AttackRange;           // ���� ��Ÿ�
}
