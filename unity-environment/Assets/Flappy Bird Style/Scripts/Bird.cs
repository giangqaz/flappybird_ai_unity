using UnityEngine;
using System.Collections;

public class Bird : Singleton<Bird> {  

	public float upForce;					//Upward force of the "flap".
	public bool isDead = false;		//Has the player collided with a wall?
    public bool getPoint = false;
    public float top;
    public float bottom;
    public float range { get; set; }

    private Animator anim;					//Reference to the Animator component.
	private Rigidbody2D rb2d;				//Holds a reference to the Rigidbody2D component of the bird.
    private Vector2 startPos;
    private Transform mTransform;
    
	void Start()
	{
        mTransform = transform;
		//Get reference to the Animator component attached to this GameObject.
		anim = GetComponent<Animator> ();
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        range = top - bottom;
	}
    //private void OnSceneGUI()
    //{
    //    Debug.Log("abcdd");
    //    GUI.Label(new Rect(new Vector2(mTransform.position.x + 3, mTransform.position.y), new Vector2(6, 10)),"abc");
    //}
    void Update()
	{
        if (mTransform.position.y > 4.77 || mTransform.position.y < -1.96)
        {
            isDead = true;
        }
		//Don't allow control if the bird has died.
		//if (isDead == false) 
		//{
		//	//Look for input to trigger a "flap".
		//	if (Input.GetMouseButtonDown(0)) 
		//	{
  //              AddForce();
		//	}
		//}
	}
    public void ResetBird() {
        anim.SetTrigger("Flap");
        transform.position = startPos;
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0;
        isDead = false;
        getPoint = false;
    }
    public void AddForce()
    {
        //...tell the animator about it and then...
        anim.SetTrigger("Flap");
        //...zero out the birds current y velocity before...
        rb2d.velocity = Vector2.zero;
        //	new Vector2(rb2d.velocity.x, 0);
        //..giving the bird some upward force.
        rb2d.AddForce(new Vector2(0, upForce));
    }

	void OnCollisionEnter2D(Collision2D other)
	{
		// Zero out the bird's velocity
		rb2d.velocity = Vector2.zero;
		// If the bird collides with something set it to dead...
		isDead = true;
		//...tell the Animator about it...
		anim.SetTrigger ("Die");
		//...and tell the game control about it.
	}
}
