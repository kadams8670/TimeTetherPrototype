using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assembler : MonoBehaviour
{
    public bool isGenerating = false;

	public ItemDetector[] itemDetector;

    public GameObject player;
    public Transform itemToCreate;
    public float distanceToPlayer;
	public Vector2 CreatedItem;

    // Use this for initialization    
void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        /*if (linkedButton == null && itemDetector == null && flashTrigger == null)
			timeDelay = 0;*/
    }


    // Update is called once per frame
    void Update()
    {
		CheckDetectors ();
		AssemblerControl ();

    }

    void AssemblerControl()
    {		
		if (itemDetector != null && CheckDetectors() == true)
        {
            //check detector
			if (!isGenerating)
			{
				isGenerating = true;
				Instantiate(itemToCreate, CreatedItem, Quaternion.identity);
            }
        }
        else
        {
			if (Vector3.Distance(player.transform.position, transform.position) <= 0.75f && Input.GetKeyDown(KeyCode.E) && !isGenerating)
            {
				isGenerating = true;
            }
        }
    }
    public void ActivateAssembler()
    {
		isGenerating = true;
       
    }

	bool CheckDetectors()
	{
		for (int i = 0; i < itemDetector.Length; i++) {

			if (!itemDetector [i].isActive)
				return false;
		}
		return true;
	}
}
