using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GumdropProp : Prop
{

    private GameObject GumdropPrefab;               // ��ƤȦԤ����
    private float distance = 0.0f;                  // ��ƤȦ���ɾ�����ҵľ���
    // ��дProp���е�UseProp����
    public override void UseProp()
    {
        Debug.Log("ʹ����ƤȦ����");

        // ʹ��Ԥ�����·���������Resources�ļ��У�������Ԥ����
        GumdropPrefab = Resources.Load<GameObject>("Gamdrop");

        // ȷ������Ԥ����
        if (GumdropPrefab != null)
        {
            Debug.Log("GumdropPrefab��ȡ�ɹ�\n");
            // ��ȡ��ҵ�ǰλ��
            Vector2 position = PlayerController.Instance.transform.position;

            // ������ƫ������λ��
            position.x += distance * PlayerController.Instance.lookDirection.x;
            //position.y -= 1.0f;

            // ��ƫ��λ��ʵ������ƤȦ����
            Instantiate(GumdropPrefab, position, Quaternion.identity);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
