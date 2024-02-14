using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class CUserContext
{
    public bool IsIPRole
    {
        get 
        {
                try
                {
                    string usert_type = Convert.ToString(HttpContext.Current.Session["user_type"]);
                    if (usert_type == "IP") return true;
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            
        }
    }
    //public bool IsIPGBVRole
    //{
    //    get
    //    {
    //        try
    //        {
    //            string usert_type = Convert.ToString(HttpContext.Current.Session["user_type"]);
    //            string gbvIP_type = Convert.ToString(HttpContext.Current.Session["GBVUser"]);
    //            if (usert_type == "IP" && gbvIP_type == "1") return true;
    //            return false;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }

    //    }
    //}
    public bool IsTSTRole
    {
        get
        {
            try
            {
                string usert_type = Convert.ToString(HttpContext.Current.Session["user_type"]);
                if (usert_type == "TST") return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
    public bool IsManagerRole
    {
        get
        {
            try
            {
                string usert_type = Convert.ToString(HttpContext.Current.Session["user_type"]);
                if (usert_type == "Manager") return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
    public bool IsManagerGRMRole
    {
        get
        {
            try
            {
                string usert_type = Convert.ToString(HttpContext.Current.Session["user_type"]);
                if (usert_type == "ManagerGRM") return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
    public bool IsEntryGRMRole
    {
        get
        {
            try
            {
                string usert_type = Convert.ToString(HttpContext.Current.Session["user_type"]);
                if (usert_type == "EntryGRM") return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
    public bool IsCoordinatorRole
    {
        get
        {
            try
            {
                string usert_type = Convert.ToString(HttpContext.Current.Session["user_type"]);
                if (usert_type == "Cordinator") return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
    public bool IsSpecialistRole
    {
        get
        {
            try
            {
                string usert_type = Convert.ToString(HttpContext.Current.Session["user_type"]);
                if (usert_type == "Specialist") return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
    public bool IsAdminRole
    {
        get
        {
            try
            {
                string usert_type = Convert.ToString(HttpContext.Current.Session["user_type"]);
                if (usert_type == "Admin") return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }



}

