using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 0.1f;//скорость движения камеры в право-влево
    public bool taked = false;//схвачен ли объект
	GameObject takedObject = null;//схваченный объект



    // Update is called once per frame
    //этот скрипт лежит у камеры
    void Update()
    {
		if (Input.touchCount > 0)
        {
			//запись ворлдовских координат куда нажал игрок
			Touch touch = Input.GetTouch(0);
			Vector2 touchPos = touch.position;
			Vector3 touchPosinWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, Camera.main.nearClipPlane));

			//отправка луча с целью проверить, нажал ли игрок на объект
			if (touch.phase == TouchPhase.Began)
			{
				RaycastHit2D hit = Physics2D.Raycast(touchPosinWorldSpace, -Vector2.up);
				if (hit.collider != null)
				{
					if (hit.collider.gameObject.tag == "Object")
					{
						taked = true;
						takedObject = hit.collider.gameObject;
						takedObject.GetComponent<ObjectScript>().isTaked = true;
					}
				}
			}

			//забыание объекта после убирания пальца с экрана
			if (touch.phase == TouchPhase.Ended)
			{
				if(taked)
				{
					takedObject.GetComponent<ObjectScript>().isTaked = false;
					takedObject = null;
					taked = false;
				}
			}

			//движение объекта или движение камеры вправо-влево
            if (touch.phase == TouchPhase.Moved)
            {
                if (!taked)
				{
					if (touch.deltaPosition.x > 0)
					{
						if (transform.position.x > -15)
							transform.position = transform.position + new Vector3(touch.deltaPosition.x * -speed, 0, 0);
					}
					else
					{
						if (transform.position.x < 15)
							transform.position = transform.position + new Vector3(touch.deltaPosition.x * -speed, 0, 0);
					}
				}
				else
				{
					takedObject.transform.position = touchPosinWorldSpace;
				}
			}
		}
	}
}
