using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject LoginScreen;
    public GameObject Registration;
    private AccountCharacteristics account;
    public GameObject Kushandeck;
    public GameObject Taiidandeck;
    public InputField Registration_player_name;
    public InputField Login_player_name;

    // Start is called before the first frame update
    void Start()
    {
        account = GameObject.Find("ActiveAccount").GetComponent<AccountCharacteristics>();
        if(account.GetPlayerName().CompareTo("") == 0)
        {
            MainMenu.SetActive(false);
            Registration.SetActive(false);
            LoginScreen.SetActive(true);
        }
        else
        {
            MainMenu.SetActive(true);
            Registration.SetActive(false);
            LoginScreen.SetActive(false);
        }
    }

    public void ActivateReg()
    {
        if (LoginScreen.activeSelf)
            LoginScreen.SetActive(false);

        Registration.SetActive(true);
    }
    private void ActivateMain()
    {
        if (Registration.activeSelf)
            Registration.SetActive(false);
        
        if(LoginScreen.activeSelf)
            LoginScreen.SetActive(false);
        
        MainMenu.SetActive(true);
    }

    public void LogOut()
    {
        if (MainMenu.activeSelf)
            MainMenu.SetActive(false);

        if (Registration.activeSelf)
            Registration.SetActive(false);

        LoginScreen.SetActive(true);
    }

    public void Kushan()
    {
        if (Registration_player_name.text != "")
        {
            Debug.Log("Player Name: " + Registration_player_name.text);
            account.NewPlayer(Registration_player_name.text,CQBCard.FactionType.KUSHAN);
            ActivateMain();
        }
        else
            Debug.LogError("No Input detected.");
    }

    public void Taiidan()
    {
        if (Registration_player_name.text != "")
        {
            Debug.Log("Player Name: " + Registration_player_name.text);
            account.NewPlayer(Registration_player_name.text, CQBCard.FactionType.TAIIDAN);
            ActivateMain();
        }
        else
            Debug.LogError("No Input detected.");
    }

    public void ExistingPlayer()
    {
        if (Login_player_name.text != "")
        {
            Debug.Log("Player Name: " + Login_player_name.text);
            account.LoadExistingPlayer(Login_player_name.text);
            ActivateMain();
        }
        else
            Debug.LogError("No Input detected.");
    }

    public void LoadGameScreen()
    {
        SceneManager.LoadScene("CQBPrototype2");
    }

    public void LoadManageScreen()
    {
        SceneManager.LoadScene("Management");
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

}
