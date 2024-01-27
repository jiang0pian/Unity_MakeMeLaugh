using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumdropScript : Prop
{
    

    private float distance = 0.0f;              // ��Ƥ��Ȧ���þ�����ҵľ���
    public Rigidbody2D anchor;                  // ê��ĸ���
    private Rigidbody2D rb;                     // ��Ƥ��Ȧ�ĸ���
    private Collider2D coll;                    // ��Ƥ��Ȧ����ײ��
    private bool isPressed;                     // �������Ƿ���
    private bool isCatapult;                    // ��Ƥ��Ȧ�Ƿ񱻷���
    private float maxDragDistance = 5.0f;       // ����������
    //private TrailRenderer trailRenderer;        // �켣���
    private Vector2 mousePos;                   // �����Ϸ������
    private float coefficient = 15.0f;           // ��Ƥ��Ȧ�ĵ���ϵ��
    public LayerMask layerMask;                 // ��Unity�༭���������������Layer
    public float detectionRadius = 5.0f;        // ���뾶


    // ��дProp���е�UseProp����
    public override void UseProp()
    {
        Debug.Log("ʹ����Ƥ��Ȧ����\n");

        // ȷ����Ƥ��ȦԤ����ǿ�
        if (itemPrefab != null)
        {
            Debug.Log("itemPrefab��ȡ�ɹ�\n");
            // ��ȡ��ҵ�ǰλ��
            Vector2 position = PlayerController.Instance.transform.position;

            // ������ƫ������λ��
            Debug.Log("��ǰ���λ�ã�" + position.x + "," + position.y + ")\n");
            position.x += distance * PlayerController.Instance.lookDirection.x;
            //position.y -= 1.0f;

            // ��ƫ��λ��ʵ������Ƥ��Ȧ����
            Instantiate(itemPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.Log("itemPrefab == null\n");
        }
    }

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        coll = this.GetComponent<Collider2D>();
        anchor = PlayerController.Instance.GetComponent<Rigidbody2D>();
        //trailRenderer = this.GetComponent<TrailRenderer>();
        rb.isKinematic = true;                          // ʹ��Ƥ��Ȧ�˶�״̬��Ϊ��������
        coll.isTrigger = true;                          // ʹ��Ƥ��Ȧ��ײ�����ã�����Ϊ������
        Cursor.visible = true;                          // ʹ������
        isPressed = false;
        isCatapult = false;
    }
    private void Update()
    {
        if(rb!=null)
        {
            // ÿ֡����ê��
            anchor = PlayerController.Instance.GetComponent<Rigidbody2D>();
            if (Input.GetMouseButtonDown(0)) // 0 ����������
            {
                // ��������������ʱ��ִ��һЩ����
                Debug.Log("�������Ϸ�ڵ��");
                isPressed = true;
                if (rb != null)
                {
                    rb.isKinematic = true;  //��������Ϊ���˶�ѧ�����䲻���������ã���ֹ����϶��Ĺ����е���
                }
            }
            if (Input.GetMouseButtonUp(0)) // 0 ����������
            {
                // �����������ͷ�ʱ��ִ��һЩ����
                Debug.Log("����������Ϸ�ڱ��ͷ�");
                isPressed = false;
                if (rb != null)
                {
                    rb.isKinematic = false;
                    //trailRenderer.enabled = true;
                    isCatapult = true;
                    Catapult(rb);
                    Cursor.visible = false;
                }
            }

            if (!isPressed && !isCatapult)
            {
                // ��ʼ��׼ǰ��ƤȦ�������
                Debug.Log(gameObject.name+"δ��ʼ��׼\n");
                rb.position = anchor.position;
            }
            else if (isPressed && !isCatapult)
            {
                Debug.Log(gameObject.name + "��ʼ��׼\n");
                // ��׼ʱʼ����ê�㷶Χ��
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(mousePos, anchor.position) > maxDragDistance)
                {
                    rb.position = (mousePos - anchor.position).normalized * maxDragDistance + anchor.position;
                }
                else
                {
                    rb.position = mousePos;
                }
                drawParabola();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(rb!=null && isCatapult)
        {
            // �����������������봥����ʱ����������ᱻ����
            if (Vector2.Distance(other.gameObject.transform.position,anchor.position)>maxDragDistance)
            {
                Debug.Log("����������������:" + other.gameObject.name + "\n");
                PropEffect();
                Destroy(this.gameObject);
            }
        }
    }


    // ����������������������Ԥ��������
    private void drawParabola()
    {
        
    }

    // ����rb
    private void Catapult(Rigidbody2D rb)
    {
        rb.isKinematic = false; // ȷ�����岻���˶�ѧ��
        rb.gravityScale = 8;    // ȷ������Ӱ�쿪��
        // ������׼��������ʩ�ӵ���
        Vector2 force = Mathf.Min(Vector2.Distance(mousePos, anchor.position), maxDragDistance) * coefficient * (anchor.position - mousePos).normalized;
        rb.AddForce(force,ForceMode2D.Impulse);
    }

    // ������Ч���Ч��ʵ��
    private void PropEffect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, layerMask);
        foreach (Collider2D collider in colliders)
        {
            GameObject detectedObject = collider.gameObject;
            // �����ﴦ���⵽�Ķ��󣬴˴�ӦΪ�ı���˵�״̬��ʹ����������Ҿۼ�
            // ...
        }
    }
}
