using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [System.Serializable]
    public class Menu
    {
        public string menuName;
        public GameObject menuObject;
    }

    [SerializeField] private List<Menu> menus;
    private Menu currentOpenMenu;

    public void OpenMenu(string menuName)
    {
        // Close the currently open menu (if any)
        if (currentOpenMenu != null)
        {
            currentOpenMenu.menuObject.SetActive(false);
        }

        // Find and open the requested menu
        foreach (var menu in menus)
        {
            if (menu.menuName == menuName)
            {
                menu.menuObject.SetActive(true);
                currentOpenMenu = menu;
                return;
            }
        }
    }

    public void CloseCurrentMenu()
    {
        if (currentOpenMenu != null)
        {
            currentOpenMenu.menuObject.SetActive(false);
            currentOpenMenu = null;
        }
    }
}