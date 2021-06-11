using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Dice : MonoBehaviour
{
    public Route route;
    public Stone pion1;
    public Stone pion2;
    public Stone pion3;
    public Stone pion4;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;
    public GameObject cam4;
    public Image defaultImg;
    public Manager manager;
    int indexPion = 1;
    public Sprite[] diceSides;
    int currentPos;
    bool isRolling;

    public SpriteRenderer rend;
    int diceVal;
    int nrPioni;

	void Start () {

        
        rend = GetComponent<SpriteRenderer>();

        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
	}
	
    public void OnMouseDown()
    {
        StartCoroutine("RollTheDice");
    }

    private IEnumerator RollTheDice()
    {
        if (isRolling || pion1.isMoving || pion2.isMoving || pion3.isMoving || pion4.isMoving)
            yield break;
        isRolling = true;
        int randomDiceSide = 0;

        int finalSide = 0;

        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);

            rend.sprite = diceSides[randomDiceSide];
            defaultImg.sprite = rend.sprite;
            yield return new WaitForSeconds(0.05f);
        }

        finalSide = randomDiceSide + 1;

        switch (indexPion)
        {
            case 1:
                nrPioni = manager.GetNrStones();
                cam1.SetActive(true);
                cam2.SetActive(false);
                cam3.SetActive(false);
                cam4.SetActive(false);
                if (pion1.GetNodeNumber() + finalSide < route.childNodeList.Count)
                {
                    pion1.SetSteps(finalSide);
                }
                indexPion++;
                break;

            case 2:
                nrPioni = manager.GetNrStones();
                cam1.SetActive(false);
                cam2.SetActive(true);
                cam3.SetActive(false);
                cam4.SetActive(false);
                if (pion2.GetNodeNumber() + finalSide < route.childNodeList.Count)
                {
                    pion2.SetSteps(finalSide);
                }
                if (nrPioni > 2)
                    indexPion++;
                else indexPion = 1;
                break;

            case 3:
                nrPioni = manager.GetNrStones();
                cam1.SetActive(false);
                cam2.SetActive(false);
                cam3.SetActive(true);
                cam4.SetActive(false);
                if (pion3.GetNodeNumber() + finalSide < route.childNodeList.Count)
                {
                    pion3.SetSteps(finalSide);
                }
                if (nrPioni > 3)
                    indexPion++;
                else indexPion = 1;
                break;

            case 4:
                nrPioni = manager.GetNrStones();
                cam1.SetActive(false);
                cam2.SetActive(false);
                cam3.SetActive(false);
                cam4.SetActive(true);
                if (pion4.GetNodeNumber() + finalSide < route.childNodeList.Count)
                {
                    pion4.SetSteps(finalSide);
                }
                indexPion = 1;
                break;

            default:
                break;
                
        }
        isRolling = false;
        
    }
}
