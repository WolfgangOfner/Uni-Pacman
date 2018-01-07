namespace PacmanClientWPF.Util
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Text.RegularExpressions;
   using System.Threading.Tasks;

   public static class Util
   {
      /// <summary>
      /// method to validate an IP address
      /// using regular expressions. The pattern
      /// being used will validate an ip address
      /// with the range of 1.0.0.0 to 255.255.255.255
      /// </summary>
      /// <param name="addr">Address to validate</param>
      /// <returns></returns>
      public static bool IsValidIP(string addr)
      {
         //create our match pattern
         string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
         //create our Regular Expression object
         Regex check = new Regex(pattern);
         //boolean variable to hold the status
         bool valid = false;
         //check to make sure an ip address was provided
         if (addr == String.Empty)
         {
            //no address provided so return false
            valid = false;
         }
         else
         {
            //address provided so use the IsMatch Method
            //of the Regular Expression object
            valid = check.IsMatch(addr, 0);
         }
         return valid;
      }

      /// <summary>
      /// method to validate an IP port
      /// using regular expressions. The pattern
      /// being used will validate a port with 5 numbers
      /// with the range of 10000 to 99999
      /// </summary>
      /// <param name="addr">Address to validate</param>
      /// <returns></returns>
      public static bool IsValidPort(string port)
      {
         //create our match pattern
         string pattern = @"\d{5}";
         //create our Regular Expression object
         bool valid = false;
         //check to make sure an ip portwas provided
         if (port == String.Empty)
         {
            //no port provided so return false
            valid = false;
         }
         else
         {
            //port provided so use the IsMatch Method
            //of the Regular Expression object
            valid = Regex.IsMatch(port, pattern);

            if (valid)
            {
               int iport = Convert.ToInt32(port);
               if (iport < 10000 || iport > 99999)
               {
                  valid = false;
               }
            }
         }
         return valid;
      }
   }
}
