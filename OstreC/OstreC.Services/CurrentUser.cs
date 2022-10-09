﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OstreC.Services
{

    public class CurrentUser : User
    {

        public bool LoggedIn { get; set; }

        public CurrentUser(int id, string username, string password, bool loggedIn)
       : base(username, password, id)
        {
            LoggedIn = loggedIn;
        }

        public bool Login(string userName, string password)
        {

            var x = JsonFile.DeserializeUsersList("Users");

            foreach (var user in x.Results)
            {
                if (user.UserName == userName && user.Password == password)
                {
                    LoggedIn = true;

                    return true;
                }


            }
            return false;
        }

        public bool createUser(CurrentUser currentUser, out string feedback)
        {

            var usersList = JsonFile.DeserializeUsersList("Users");
            var usersArray = usersList.Results.ToArray();
            bool userExists = false;



            foreach (var user in usersArray)
            {
                if (user.UserName == currentUser.UserName)
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
            if (currentUser.UserName.Length != 0)
            {
                currentUser.Id = usersList.Results.Count() + 1;
                usersList.Results.Add(currentUser);

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

        public bool sendEmail(int emailType, CurrentUser currentUser, out string feedback)
        {

            //Checks if user exists
            var usersList = JsonFile.DeserializeUsersList("Users");
            var usersArray = usersList.Results.ToArray();
            bool userExists = false;

            foreach (var user in usersArray)
            {
                if (user.UserName == currentUser.UserName)
                {
                    userExists = true;
                    currentUser.Email = user.Email;
                    currentUser.Id = user.Id;
                    currentUser.Password = user.Password;
                    break;
                }


            }


            switch (userExists)
            {

                case true:

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("ostreCGame@gmail.com", "jgkeyglxajjymsft"),
                        EnableSsl = true,
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("ostreCGame@gmail.com"),
                        Subject = "",
                        Body = "",
                        IsBodyHtml = true,
                    };

                    //Forgot Password
                    if (emailType == 1)
                    {
                        mailMessage.Subject = "Ostre C Game password recovery email";
                        mailMessage.Body = $"<h1>Hello,</h1> <br> <h2> dear {currentUser.UserName}</h2><br> You forgot your password. <br> For now the best I can do is send you your password. Here it is : <br>" +
                            $"Your username: {currentUser.UserName} <br> Your password: {currentUser.Password} <br> Please don't forget your password going forward. <br> <b>Regards</b>,<br><b> Ostre C team</b>";

                        mailMessage.To.Add(currentUser.Email);

                        feedback = "Email sent on the email adress assigned to your existing account :" + currentUser.Email;
                        smtpClient.Send(mailMessage);
                        return true;
                    }
                    else
                    {
                        feedback = "An non existing email template was chosen";
                        throw new Exception(feedback);
                        return false;

                    }



                case false:


                    feedback = "Provided username doesn't exist.";
                    return false;



                default:

                    throw new Exception("This method should exit either with user existing ( so a password recovery email is sent) or non existing.");
                    break;


            }
        }
    }


}