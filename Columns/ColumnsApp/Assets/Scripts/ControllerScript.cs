using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class ControllerScript : MonoBehaviour
    {
       
        bool firstRow = false;
        bool secondRow = false;
        float scoreFirst = 0;
        float scoreSecond = 0;
        float score = 0;
        public GameObject UI;
        public Text scoreText;

        public Button rerun;
        public Button back;
        System.Random rnd;

        public GameObject UI2;
        public Text countdown;
        public bool countDB = true;
        int countD = 5;
        int mmm = 50;
 

        // Use this for initialization
        void Start()
        {
            rnd = new System.Random();
            
        }

        // Update is called once per frame
        void Update()
        {

            if (countDB)
            {
                countdown.text = countD.ToString();

                if (mmm == 0 && countD == 0)
                {
                    countDB = false;
                    UI2.SetActive(false);
                }


                if (mmm == 0)
                {
                    countD -= 1;
                    mmm = 50;
                }

                else
                    mmm -= 1;


            }

            if (firstRow && secondRow)
            {
                //Debug.Log("xxxxxxx");
                score = scoreFirst + scoreSecond;
                scoreText.text = "Zdobyłeś " + score + " punktów!";
                UI.SetActive(true);
            }
        }

        public void FirstRow(float score)
        {
            firstRow = true;
            scoreFirst = score;
        }

        public void SecondRow(float score)
        {
            secondRow = true;
            scoreSecond = score;
        }

        public void Reset()
        {
            firstRow = false;
            secondRow = false;
            scoreFirst = 0;
            scoreSecond = 0;
            score = 0;
            UI.SetActive(false);
            UI2.SetActive(true);
            countD = 5;
            countDB = true;

        
    }

        public void onBackButton()
        {
            SceneManager.LoadScene("MenuScene");

        }

        public void onRerunButton()
        {
            Reset();
            GetComponentInChildren<ColumnBehaviour2>().onRerunButton();
            GetComponentInChildren<ColumnBehaviour3>().onRerunButton();

        }

        public int getRandom()
        {
            int rand = rnd.Next(0, 2);
            return rand;
        }

        public void stopCountdown()
        {
            UI2.SetActive(false);
        }

    }
}