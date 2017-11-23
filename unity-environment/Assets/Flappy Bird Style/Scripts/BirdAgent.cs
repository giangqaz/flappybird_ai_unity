using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAgent : Agent {
    public Academy academy;
    private Transform mTransform;
    private Bird bird;
    private Collider2D col1, col2;
    private int enemyLayerMask;
    private Rigidbody2D mRid;
    void Awake()
    {
        mTransform = transform;
        bird = GetComponent<Bird>();
        enemyLayerMask = LayerMask.GetMask("Enemy");
        mRid = GetComponent<Rigidbody2D>();
    }
    public override List<float> CollectState()
    {
        //Debug.Log("abc");
        List<float> state = new List<float>();
        state.Add((mTransform.position.y - bird.bottom)/bird.range);
        state.Add(mRid.velocity.y/8);

        if (col1 == null || col2 == null)// column not found
        {
            state.Add(1f);
            state.Add(.4f);
            state.Add(-.5f);
            state.Add(.4f);
        }
        else
        {
            var t1 = col1.transform;
            var t2 = col2.transform;
            state.Add(t1.position.y/bird.range);
            state.Add(t1.position.x/4);
            state.Add(t2.position.y/bird.range);
            state.Add(t2.position.x/4);
        }
        return state;
    }
    
    public override void AgentStep(float[] action)
    {
        //Debug.Log((mTransform.position.y - bird.bottom) / bird.range + " " + (mRid.velocity.y / 6));
        Collider2D[] cols = Physics2D.OverlapBoxAll(new Vector2(mTransform.position.x + 1.5f, 0), new Vector2(4.5f, 20), 0, enemyLayerMask);
        
        if (cols.Length >= 2)
        {
            if (cols[0].bounds.center.y > cols[1].bounds.center.y)
            {
                col1 = cols[0];
                col2 = cols[1];
            }
            else
            {
                col1 = cols[1];
                col2 = cols[0];
            }
            var t1 = col1.transform;
            var t2 = col2.transform;
            Debug.Log(t1.parent.name + " " + t2.parent.name);
            if (t1.parent.name != t2.parent.name)
                Debug.Break();
            Debug.Log(string.Format("{0} {1} {2} {3} {4},{5}", t1.position.y / bird.range, t1.position.x / 4, col1.name, t2.position.y / bird.range, t2.position.x / 4, col2.name));
        }
        else
        {
            col1 = null;
            col2 = null;
        }



        if (action[0] == 1) {
            bird.AddForce();
        }

        if (done == false)
        {
            reward = 0.1f;
        }
        if (bird.getPoint)
        {
            reward += 1;
            bird.getPoint = false;
        }
            
        if (bird.isDead)
        {
            done = true;
            reward = -1;
        }
    }
    public override void AgentReset()
    {
        
        academy.AcademyReset();
    }
}
