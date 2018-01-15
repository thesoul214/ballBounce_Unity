using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSc : MonoBehaviour {
	public GameObject Sphere_red;
	public GameObject leftWall;
	public GameObject northWall;
	public GameObject rightWall;
	public GameObject southWall;

    private Vector3 velocity;
    private Vector3 direction;

    private Object[] materials;

    private float dirX;
    private float dirZ;

    void Start () {
        dirX = Random.Range(-100.0f, 100.0f);
        dirZ = Random.Range(-100.0f, 100.0f);
        direction = new Vector3(dirX, 0.0f, dirZ);
        velocity = direction * 0.6f;

        Sphere_red = GameObject.Find("Sphere_red");
        leftWall = GameObject.Find("leftWall");
        northWall = GameObject.Find("northWall");
        rightWall = GameObject.Find("rightWall");
        southWall = GameObject.Find("southWall");

        // Assets/Resources/materials 안에 있는 material 파일을 모두 가져오는 연습코드
        materials = Resources.LoadAll("Materials", typeof(Material));
    	Debug.Log(materials.Length);

    	
    	// Trail Component의 색을 지정해주기 위한 코드
    	Material trail = Sphere_red.GetComponent<TrailRenderer>().material;
    	Color nColor = Sphere_red.GetComponent<Renderer>().material.color;
    	trail.SetColor("_Color", nColor);

    }
    
    void OnCollisionEnter(Collision collisionInfo){
        if(collisionInfo.collider.name != "Ground"){
            velocity = -velocity;
            // velocity.y = 0.0f;
            velocity = Vector3.Reflect(velocity, collisionInfo.contacts[0].normal);
        }
    }

    void FixedUpdate(){
    	if(transform.position.z < leftWall.transform.position.z){
    		Destroy(this.gameObject);
    	}else if(transform.position.z > rightWall.transform.position.z){
    		Destroy(this.gameObject);
    	}else if(transform.position.x > southWall.transform.position.x){
    		Destroy(this.gameObject);
    	}else if(transform.position.x < northWall.transform.position.x){
    		Destroy(this.gameObject);
    	}

    	// position.y값이 너무 커지면 Y 방향을 낮춰준다.
    	if(this.gameObject.transform.position.y > 10.0f){
    		Debug.Log( string.Format("########### higher than 10.0f #######") );
    		velocity.y = velocity.y - 2.5f;
    	}
    	this.gameObject.transform.Translate(velocity * Time.deltaTime);
    }

	void OnMouseDown()
	{
		 
	    Debug.Log( string.Format("########### Quaternion.identity :  {0} " , Quaternion.identity) );
	    GameObject cloneObj = (GameObject) Instantiate(this.gameObject);
	    Renderer rend = cloneObj.GetComponent<Renderer>();
	    // public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax);
	    // hue : 색상, saturation : 채도
	    rend.material.color = Random.ColorHSV(0f, 1f, 0f, 1f, 0.5f, 1f);


	    // 복제된 object의 trail component 
	    Material trail = cloneObj.GetComponent<TrailRenderer>().material;
	    // 볼과 Trail을 같은 색으로 지정해준다.
    	trail.SetColor("_Color", rend.material.color);
	}

    public override string ToString(){
        return "aaa";
    }
}
