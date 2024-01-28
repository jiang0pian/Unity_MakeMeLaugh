using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoltfrieScript : Prop
{


    private float distance = 0.0f;              // Boltfrie���þ�����ҵľ���
    private Rigidbody2D rb;                     // Boltfrie�ĸ���
    private Collider2D coll;                    // Boltfrie����ײ��
    private float maxDragDistance = 10.0f;      // ������ʱ��
    private float coefficient = 10.0f;          // Boltfrie�ķ�������С
    private float boltfireDamage = 2.0f;        // Boltfrie���˺���С
    public LayerMask layerMask = 8;             // ��Unity�༭���������������Layer


    // ��дProp���е�UseProp����
    public override void UseProp()
    {
        Debug.Log("ʹ��Boltfrie����\n");

        // ȷ��BoltfrieԤ����ǿ�
        if (itemPrefab != null)
        {
            Debug.Log("itemPrefab��ȡ�ɹ�\n");
            // ��ȡ��ҵ�ǰλ��
            Vector2 position = PlayerController.Instance.transform.position;

            // ������ƫ������λ��
            Debug.Log("��ǰ���λ�ã�" + position.x + "," + position.y + ")\n");
            position.x += distance * PlayerController.Instance.lookDirection.x;
            //position.y -= 1.0f;

            // ��ƫ��λ��ʵ����Boltfrie����
            Instantiate(itemPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.Log("itemPrefab == null\n");
        }
    }

    private void Start()
    {
        if(this.GetComponent<Rigidbody2D>()!=null)
        {
            rb = GetComponent<Rigidbody2D>();
            // ȷ����������Ӱ��
            rb.gravityScale = 0;
            // ʹ������ײ�����ã�����Ϊ������
            coll = this.GetComponent<Collider2D>();
            coll.isTrigger = true;
            // ������һ��˲ʱ�ĳ������ﳯ�������������������Ӱ�죬��������������ײ����ײ
            rb.AddForce(transform.right * coefficient* PlayerController.Instance.lookDirection.x, ForceMode2D.Impulse);
            // ����һ��Э�̵��ã���������ɳ�һ��ʱ���δ�����٣�����������
            StartCoroutine(DestroyAfterTime(maxDragDistance));
        }
    }
    private void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PropEffect(other);
    }

    // ������Ч���Ч��ʵ��
    private void PropEffect(Collider2D other)
    {
        // ����������ǹ����Թ�������˺�����������������������С����ﱾ��������˺��ĺ�����ֱ�ӵ��á�
        // ���������tilemap�����������١�
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // ���ù�������˺�����
            // ���磺other.GetComponent<Enemy>().TakeDamage(boltfireDamage);
        }
        else if (other.gameObject.layer == 6)
        {
            // ��������������������
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
