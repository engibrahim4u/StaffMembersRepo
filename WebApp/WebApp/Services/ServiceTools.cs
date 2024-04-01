using WebApp.Models;
using WebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Services
{
    public static class ServiceTools
    {
        public static string ReplacementPlaceholder(string msg,Dictionary<string,string> keyValuePairs)
        {
            foreach(var key in keyValuePairs.Keys)
            {
                msg = msg.Replace("$" + key + "$", keyValuePairs[key]);
            }
            return msg;
        }
        public static Dictionary<string,string> NewRegisterKeyValuePairs(string degreeName,RegisterVM register)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("UsersDegree", degreeName);
            keyValues.Add("UsersUserName", register.Email);
            //keyValues.Add("UsersName", register.FirstName.Trim() +" "+register.FamilyName.Trim());

            return keyValues;
        }

        public static string RoleNameAr(string roleName)
        {
            switch (roleName)
            {
                case "author":
                    return "مؤلف";
                case "auditor":
                    return "محكم";
                case "admin":
                    return "مدير للموقع";
                case "user":
                    return "متدرب";
                case "instructor":
                    return "محاضر";
                case "editor":
                    return "سكرتير المجلة";
                case "editorial":
                    return "رئيس التحرير";
                case "supervisor":
                    return "المشرف العام للمجلة";
                default:
                    return roleName;
            }
        }

        public static string HeadPostionAr(string position)
        {
            switch (position)
            {
                case "Head":
                    return "رئيس هيئة تحرير المجلة";
                case "Member":
                    return "عضو";
                case "Editor":
                    return "محرر";
                case "Secretary":
                    return "سكرتير المجلة";
                case "Supervisor":
                    return "مشرف المجلة";
                default:
                    return "";
            }
        }

        public static string HeadPostionEn(string position)
        {
            switch (position)
            {
                case "Head":
                    return "Journal Editor Chief";
                case "Member":
                    return "Member";
                case "Editor":
                    return "Editor";
                case "Secretary":
                    return "Journal Secretary";
                case "Supervisor":
                    return "Journal Supervisor";
                default:
                    return "";
            }
        }
      

      

      
     
        public static string RemoveFileName(string orgFilesName,string removedFileName)
        {
            orgFilesName = orgFilesName.Replace(removedFileName, "");
            orgFilesName = orgFilesName.Replace("||", "|");
            orgFilesName = orgFilesName.Trim('|');
            return orgFilesName;
        }

        public static string MergeFileNames(string orgFilesName, string added)
        {
           string newFilesName = orgFilesName+"|"+ added;
            newFilesName = newFilesName.Replace("||", "|");
            newFilesName = newFilesName.Trim('|');
            return newFilesName;
        }
    
        public static string AssignStatus(bool? status)
        {
            if (status != null && status == true)
                return "قبول التحكيم";
            if (status != null && status == false)
                return "رفض التحكيم";
            return "معلق";
        }

      

        public static string DateFormateEn(DateTime dateTime)
        {
            return dateTime.ToString("dd-MMM-yyyy", new CultureInfo("en-US"));
        }

        public static string DateFormateAr(DateTime dateTime)
        {
            return dateTime.ToString("dd-MMM-yyyy", new CultureInfo("ar-EG"));
        }
    }
}
