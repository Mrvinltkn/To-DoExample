using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoExample.Models;
using System.Web.Security;

namespace ToDoExample.Functions
{
    public class FilterFunction
    {
        public static IQueryable<TO_DO> Filter(DateFilterModel d)
        {
            ToDoEntities br = new ToDoEntities();
            var usrid = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString()); 
            var result = br.TO_DO.Where(x=>x.UserID==usrid).AsQueryable();
            if (d != null)
            {
                if (d.Deadline != 3)
                {
                    if (d.Deadline==1)
                    {
                        result = result.Where(x => x.Deadline > DateTime.Now);
                    }
                    else if (d.Deadline==0)
                    {
                        result = result.Where(x => x.Deadline < DateTime.Now);
                    }
                }
                if (!string.IsNullOrEmpty(d.Name))
                    result = result.Where(x => x.Name.Equals(d.Name));
                if (!string.IsNullOrEmpty(d.Status))
                {
                    byte n = Convert.ToByte(d.Status);
                    result = result.Where(x => x.Status.Equals(n));
                }
                     
            }
            return result;
        }
        public static IQueryable<TO_DO> FilterForAdmin(DateFilterModel d)
        {
            ToDoEntities br = new ToDoEntities();
            var result = br.TO_DO.AsQueryable();
            if (d != null)
            {
                if (d.Deadline != 3)
                {
                    if (d.Deadline == 1)
                    {
                        result = result.Where(x => x.Deadline > DateTime.Now);
                    }
                    else if (d.Deadline == 0)
                    {
                        result = result.Where(x => x.Deadline < DateTime.Now);
                    }
                }
                if (!string.IsNullOrEmpty(d.UserId))
                {
                    var userid = Guid.Parse(d.UserId);
                    result = result.Where(x => x.UserID.Equals(userid));
                }
                if (!string.IsNullOrEmpty(d.Name))
                    result = result.Where(x => x.Name.Equals(d.Name));
                if (!string.IsNullOrEmpty(d.Status))
                {
                    byte n = Convert.ToByte(d.Status);
                    result = result.Where(x => x.Status.Equals(n));
                }
                    
            }
            return result;
        }
    }
}