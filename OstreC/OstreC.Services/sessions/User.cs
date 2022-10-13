﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;
using System.Net;
using System.Collections.Specialized;




namespace OstreC.Services
{
    
        public class User
        {

            public int Id { get; set; }

            public string UserName { get; set; }
            public string Password { get; set; }

            public string Email { get; set; }

            

            //Init with -1 until defined.
            public User(string username, string password, int id = -1)
            {
                Id = id;
                UserName = username;
                Password = password;

            }

        public string presentUser()
        {
            return  $"Username: {this.UserName}  User email: {this.Email} User password: {this.Password}"; 
        }
        public string showEmail()
        {
            return $" {this.Email} ";
        }

        public string presentUserBreakLine(bool xs)
        {
            return $"Username: {this.UserName} \n User email: {this.Email}\n User password: {this.Password}";
        }

        public bool updateUser(CurrentUser CurrentUser, string newData, int param)
        {

            var usersList = JsonFile.DeserializeUsersList("Users");
            bool updated = false;
            bool userExists = false;

            foreach (var user in usersList.Results)
            {
                if (user.UserName == CurrentUser.UserName)
                {
                    userExists = true;
                }
            }

            foreach (var user in usersList.Results)
            {
                if (user.Id == CurrentUser.Id)
                {
                    switch (param)
                    {
                        case 1:
                            user.UserName = newData;
                            CurrentUser.UserName = newData;
                            break;
                        case 2:
                            user.Password = newData;
                            CurrentUser.Password = newData;
                            break;
                        case 3:
                            if (newData.Contains("@") && newData.Contains("."))
                            {
                                user.Email = newData;
                                CurrentUser.Email = newData;
                                break;
                            }
                            else
                            {
                                return false;
                            }
                    }
                    updated = true;
                }
            }
            if (updated)
            {

                var x = JsonFile.SerializeUsersList(usersList);
                JsonFile.serializedToJson(x, "Users");

                return true;
            }
            else
            {
                return false;
            }


        }


        public bool createUser(string userName, string password, string email, CurrentUser currentUser, out string feedback)
        {

            var usersList = JsonFile.DeserializeUsersList("Users");
            bool userExists = false;

            foreach (var user in usersList.Results)
            {
                if (user.UserName == userName)
                {


                    userExists = true;
                    break;
                }
            }

            if (userExists)
            {
                feedback = "User with provided userName already exists";
                return false;

            }
            else if (userName.Length != 0)
            {
                currentUser.Id = usersList.Results.Count() + 1;
                currentUser.Email = email;
                currentUser.UserName = userName;
                currentUser.Password = password;
                usersList.Results.Add((User)currentUser);

                var x = JsonFile.SerializeUsersList(usersList);
                JsonFile.serializedToJson(x, "Users");

                feedback = "User created";

                return true;
            }
            else
            {
                feedback = "User name provided is not valid. You can't provide an empty string for a username.";
                return false;
            }
        }


        public void deleteUser(CurrentUser currentUser)
        {
            UsersList usersList = JsonFile.DeserializeUsersList("Users");

            bool found = false;
            //This variable is most likely avoidable. But I can't remove an objet from an array while iterating. I can't do X = I  as I will be equal to last index of usersList.
            //From here the name of the variable is self explanatory.
            int[] IhadNoChoice = new int[1];
            for (int i = 0; i < usersList.Results.Count(); i++)
            {
                if (usersList.Results[i].Id == currentUser.Id)
                {
                    IhadNoChoice[0] = i;
                    found = true;
                    break;
                }

            }

            if (found)
            {
                usersList.Results.Remove(usersList.Results[IhadNoChoice[0]]);
                //Sets values to empty values since log off is the next step. 
            

                var z = JsonFile.SerializeUsersList(usersList);
                JsonFile.serializedToJson(z, "Users");


            }
            else
            {
                throw new Exception("Couldn't find ID of connected user.");

            }
        }

    }
        
}
 