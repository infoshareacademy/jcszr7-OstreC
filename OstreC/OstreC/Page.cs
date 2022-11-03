namespace OstreC
{//Used to save " active page" data.
    public class Page
    {
        public PageType CurrentType { get; set; }
        public string BreakLine { get; } = "\n=======================================================================================================\n";
        public string PageInfo { get; internal set; } = "";
        public string Error { get; set; } = "";
        public string Instructions { get; set; } = "";
        
        public Page(PageType x)
        {
            //Default value
            CurrentType = x;
        }

        //Text to show when  UI.Page.CurrentType  page changes to a differnt one. 
        public void switchPage(PageType x, UI UI)
        {
            switch (x)
            {
                case PageType.Main_Menu:
                    CurrentType = PageType.Main_Menu;
                    PageInfo = "You are in the main menu!";
                    Instructions = " Press 1 to Start a New Game! \n Press 2 to Load a game! \n Press 3 to Create a new character! \n Press 4 to enter the Story Builder \n" +
                        " Press 5 to view the D&D Library \n Press 6 to logout \n Press 7 to exit the program \n Press 8 to manage your account";
                    UI.DrawUI(UI, true);
                    break;

                case PageType.Create_NewGame:
                    CurrentType = PageType.Create_NewGame;
                    PageInfo = "Welcome to game creation screen!";
                    Instructions = " Press 1 to create new game\n pres 2 to go back to main menu";
 
                    UI.DrawUI(UI, true);
                    break;

                case PageType.Create_Character:
                    CurrentType = PageType.Create_Character;
                    PageInfo = "Welcome to character creation page!";                    
                    Instructions = " 1. Create Default Player \n 2. Create customer Player \n 3.DeletePlayer \n 4.DisplayStatistics \n 5.Save Player \n 6.Exit to Main Menu";
                    UI.DrawUI(UI, true);
                    break;

                case PageType.Load_Game:
                    CurrentType = PageType.Load_Game;
                    PageInfo = "Welcome to game loading screen!";
                    Instructions = "User input is not handled yet.";
                    UI.DrawUI(UI, true);
                    break;

                case PageType.Paragraph:
                    CurrentType = PageType.Paragraph;
                    PageInfo = "Welcome to Paragraphs!";
                    Instructions = " Press ENTER to start the story!";
                    UI.DrawUI(UI, true);
                    break;

                case PageType.Story_Bulider:
                    CurrentType = PageType.Story_Bulider;
                    PageInfo = "Welcome to the Story Builder!";
                    Instructions = " Type 0 to go back to the main menu! \n Type 1 to create a new story!\n Type 2 to load an existing story and edit it!";
                    UI.DrawUI(UI, true);
                    break;

                case PageType.Dictionary:
                    CurrentType = PageType.Dictionary;
                    PageInfo = "Welcome to Dictionary!";
                    Instructions = " 1. View all existing enemies! \n 2. To compare 2 enemies witch each other!  ";
                    UI.DrawUI(UI, true);
                    break;

                case PageType.Login:
                    CurrentType = PageType.Login;
                    PageInfo = "Welcome to the login page! ";
                    Instructions = " Type 1 to login \n Type 2 to register \n Type 3 if you forgot your password";
                    UI.DrawUI(UI, true);
                    break;

                case PageType.ManageAccount:
                    CurrentType = PageType.ManageAccount;
                    PageInfo = $"Welcome to account management page dear : {UI.CurrentUser.UserName} \n Your account data is visible below: \n {UI.CurrentUser.PresentUserBreakLine(true)} ";
                       
                    Instructions = " Type 1 edit your data \n Type 2 to delete your account";
                    UI.DrawUI(UI, true);
                    break;

                default:
                    throw new Exception("SwitchPage() was ivoked at Page.Cs with an unhandled page type.Default message won't be assigned.");
                    break;
            }
        }
    }
}
