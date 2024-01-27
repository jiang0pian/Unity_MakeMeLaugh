using UnityEngine;

public class JellyProp : Prop
{
    public GameObject jellyPrefab;              // ����Ԥ����



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
            position.x += 2;

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
