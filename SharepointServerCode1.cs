using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Collections;

//C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\12\ISAPI\Microsoft.SharePoint.dll
//C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\12\ISAPI\Microsoft.SharePoint.Security.dll

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SPSite oSite = new SPSite("http://win-eepj26dmqgv:23252"))
            {
                using (SPWeb oWeb = oSite.AllWebs["/"])
                {
                    SPList oList = oWeb.Lists["TestDima"];
                    foreach (SPListItem oItem in oList.Items)
                    {
                        Console.WriteLine("Item:");
                        Console.WriteLine("ID: " + oItem["ID"] + " __ Comment: " + oItem["Comment"]);
                        Console.WriteLine("Versions:");
                        var deleteList = new ArrayList();
                        foreach (SPListItemVersion ver in oItem.Versions)
                        {
                            Console.WriteLine(ver.VersionLabel + " " + ver.CreatedBy.User.Name);
                            if (ver.CreatedBy.User.Name.Contains("dima")) {
                                deleteList.Add(ver);
                            }
                            //Console.ReadLine();
                        }
                        foreach (SPListItemVersion ver in deleteList)
                        {
                            Console.WriteLine("DELETED: " + ver.VersionLabel);
                            try
                            {
                                ver.Delete();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                    }
                }
            }
            Console.ReadLine();
            Console.WriteLine("hello");
            
        }
    }
}
