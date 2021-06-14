using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Stone : MonoBehaviour
{
    public Route currentRoute;
    public StoneCollision collision;
    public Image backImg;
    public Text congrats;
    int routePosition;
    public bool isMoving;
    int nrEnemy;
    string numeFata = "None";


    // functie mutare inainte
    IEnumerator Move(int pasi)
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (pasi > 0)
        {
            Vector3 nextPos = currentRoute.childNodeList[routePosition + 1].position;
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            pasi--;
            routePosition++;
        }

        isMoving = false;
        // S-a oprit din miscare si testam daca mai e vreun pion pe casuta pe care am ajuns
        //Debug.Log("S-a oprit din miscare");
        nrEnemy = collision.TestCollision(routePosition);
        //Debug.Log("Pe casuta nr. " + routePosition + " sunt " + nrEnemy + " pioni");
        if (nrEnemy > 1)
        {
            StartCoroutine(MoveBack(1));
        }
        else
        {
            if (numeFata == "FaceGreen")
            {
                StartCoroutine(Move(3));
            }
            if (numeFata == "FaceYellow")
            {
                StartCoroutine(Move(1));
            }
            if (numeFata == "FaceBlue")
            {
                StartCoroutine(MoveBack(1));
            }
        }

        // Afiseaza mesajul de finish game atunci cand ajungem pe ultima casuta
        if (routePosition == currentRoute.childNodeList.Count - 1)
        {
            SceneManager.LoadScene(4);
            StartCoroutine(IncrementGameFinished());
        }

    }

    // functie pentru mutare inapoi
    IEnumerator MoveBack(int pasi)
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (pasi > 0)
        {
            Vector3 nextPos = currentRoute.childNodeList[routePosition - 1].position;
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            pasi--;
            routePosition--;
        }

        isMoving = false;
        // S-a oprit din miscare si testam daca mai e vreun pion pe casuta pe care am ajuns
        nrEnemy = collision.TestCollision(routePosition);
        if (nrEnemy > 1)
        {
            StartCoroutine(MoveBack(1));
        }
        else
        {
            if (numeFata == "FaceGreen")
            {
                StartCoroutine(Move(3));
            }
            if (numeFata == "FaceYellow")
            {
                StartCoroutine(Move(1));
            }
            if (numeFata == "FaceBlue")
            {
                StartCoroutine(MoveBack(1));
            }
        }

        // Afiseaza mesajul de finish game atunci cand ajungem pe ultima casuta
        if (routePosition == currentRoute.childNodeList.Count - 1)
        {
            backImg.gameObject.SetActive(true);
            congrats.gameObject.SetActive(true);
        }
    }

    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 4f * Time.deltaTime));
    }

    // salveaza in variabila steps numarul rezultat la aruncarea zarului
    public void SetSteps(int pasi)
    {
        StartCoroutine(Move(pasi));
    }

    public void StartMoveBack(int pasi)
    {
        StartCoroutine(MoveBack(pasi));
    }

    // returneaza numarul nodului curent
    public int GetNodeNumber()
    {
        return routePosition;
    }

    public void OnCollisionStay(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag != "Cube")
            numeFata = collisionInfo.collider.tag;
    }

    public void OnCollisionExit(Collision collisionInfo)
    {
        numeFata = "None";
    }

    IEnumerator WaitCollision()
    {
        yield return new WaitForSeconds(3);
    }

    void Update()
    {

    }

    IEnumerator IncrementGameFinished()
    {
        WWWForm form = new WWWForm();
        form.AddField("userId", DBManager.userId.ToString());
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/mds_db/gameFinish.php", form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text[0] == '0')
        {
            DBManager.gamesFinished = int.Parse(www.downloadHandler.text.Split('\t')[1]);
            Debug.Log("Game Finished Succesfully");
        }
        else
        {
            Debug.Log("Game Finish Failed. Error #" + www.downloadHandler.text);
        }
    }
}
