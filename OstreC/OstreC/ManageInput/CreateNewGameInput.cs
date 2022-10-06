﻿using OstreC.Services;
using OstreC;
using OstreC.Interface;
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;


namespace OstreC.ManageInput
{
    public class CreateNewGameInput : IuiInput
    {

       
        public PageType Type => PageType.Create_NewGame;
        public void checkUserInput(UI UI)
        {
            

            var input = Console.ReadLine();

            if (Helpers.isCommand(input, UI))
            {
                Helpers.HandleCommand(input, UI);
            }

            //Your code goes here

        }

    }
}