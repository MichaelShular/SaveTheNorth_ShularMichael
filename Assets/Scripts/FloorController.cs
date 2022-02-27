using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public float decreaseAmount;
    private int decreaseAmountCount;
    public int amountOFCollectionsPossible;

    // Start is called before the first frame update
    void Start()
    {
        decreaseAmountCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseFloorSize()
    {
        transform.localScale *= decreaseAmount;
        decreaseAmountCount++;
        if(decreaseAmountCount > amountOFCollectionsPossible)
        {
            Destroy(this.gameObject);
        }
    }
}
