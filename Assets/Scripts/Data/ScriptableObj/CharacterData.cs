using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("�⺻ ����")]
    public CharacterType CharacterType; // ����
    public string Name;                 // �̸�
    public int Cost;                    // ����

    [Header("����")]
    public int MaxHp;                   // �ִ�ü��
    public int MaxMp;                   // �ִ븶��
    public float MoveSpeed;             // �̵��ӵ�
    public float AttackSpeed;           // ���ݼӵ�
    public float AttackDamage;          // �⺻ ���� ������
    public float CriticalRate;          // ġ��Ÿ Ȯ��
    public float Range;                 // ���� ��Ÿ�
}
