using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    // Start is called before the first frame update

    //скорость падение и переменная для определенния что объект схвачен
    public float speed = 0.1f;
    public bool isTaked = false;

    //список "полок" с которыми этот объект соприкасается
    //изначально просто использовалась bool переменная, однако был обнаружен баг с соприкосновением двух колизий
    //переставая взаймодействовать с одним из двух колизий, объект считал что может свободно падать
    //поэтому теперь объект фиксирует вообще все колизий и падает только когда список пуст
    public List<GameObject> canFallObject = new List<GameObject>();


    //добавление колизий в список
	private void OnTriggerEnter2D(Collider2D collision)
	{
		canFallObject.Add(collision.gameObject);
	}
    //удаление колизий из списка
	private void OnTriggerExit2D(Collider2D collision)
	{
		canFallObject.Remove(collision.gameObject);
	}
	// Update is called once per frame
    //тут просто падение объекта
	void Update()
    {
        if (canFallObject.Count <= 0 && !isTaked)
            transform.position = transform.position + new Vector3(0, -1 * speed, 0);
    }
}
