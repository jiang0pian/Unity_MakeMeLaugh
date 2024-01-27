using UnityEngine;

public class JellyProp : Prop
{
    private GameObject jellyPrefab;              // ����Ԥ����
    private float distance = 3.0f;              // �������þ�����ҵľ���

    // ��дProp���е�UseProp����
    public override void UseProp()
    {
        Debug.Log("ʹ�ù�������\n");

        // ʹ��Ԥ�����·���������Resources�ļ��У�������Ԥ����
        jellyPrefab = Resources.Load<GameObject>("Jelly");

        // ȷ������Ԥ����
        if (jellyPrefab != null)
        {
            Debug.Log("jellyPrefab��ȡ�ɹ�\n");
            // ��ȡ��ҵ�ǰλ��
            Vector2 position = PlayerController.Instance.transform.position;

            // ������ƫ������λ��
            Debug.Log("��ǰ���λ�ã�" + position.x + "," + position.y + ")\n");
            position.x += distance * PlayerController.Instance.lookDirection.x;
            position.y -= 1.0f;

            // ��ƫ��λ��ʵ������������
            Instantiate(jellyPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.Log("jellyPrefab == null\n");
        }
    }

    void Start()
    {
        // ��Start�����п���ִ�г�ʼ������
    }
}
