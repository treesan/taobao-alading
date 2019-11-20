using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class PictureService
    {

        public static ReturnType AddPicture(Picture picture)
        {
            return DataProviderClass.Instance().AddPicture(picture);
        }

        public static ReturnType AddPicture(List<Picture> pictureList)
        {
            return DataProviderClass.Instance().AddPicture(pictureList);
        }
    
        public static ReturnType RemoveAllPicture()
        {
            return DataProviderClass.Instance().RemoveAllPicture();
        }
    
        public static ReturnType RemovePicture(Func<Picture, bool> func)
        {
            return DataProviderClass.Instance().RemovePicture(func);
        }

        public static ReturnType RemovePicture(string OuterID)
        {
            return DataProviderClass.Instance().RemovePicture(OuterID);
        }       
        
        /*
        public static ReturnType RemovePicture(int pictureID)
        {
            return DataProviderClass.Instance().RemovePicture(pictureID);
        }
        */

        public static ReturnType RemovePicture(List<string> OuterIDList)
        {
            return DataProviderClass.Instance().RemovePicture(OuterIDList);
        }        
        
        /*
        public static ReturnType RemovePicture(List<int> pictureIDList)
        {
            return DataProviderClass.Instance().RemovePicture(pictureIDList);
        }
        */
    
        public static ReturnType UpdatePicture(Picture picture)
        {
            return DataProviderClass.Instance().UpdatePicture(picture);
        }

        public static ReturnType UpdatePicture(string OuterID, Picture picture)
        {
            return DataProviderClass.Instance().UpdatePicture(OuterID, picture);
        }
        
        /*
        public static ReturnType UpdatePicture(int pictureID, Picture picture)
        {
            return DataProviderClass.Instance().UpdatePicture(pictureID, picture);
        }
        */
    
        public static List<Picture> GetAllPicture()
        {
            return DataProviderClass.Instance().GetAllPicture();
        }
    
        public static List<Picture> GetPicture(Func<Picture, bool> func)
        {
            return DataProviderClass.Instance().GetPicture(func);
        }

        public static Picture GetPicture(string OuterID)
        {
            return DataProviderClass.Instance().GetPicture(OuterID);
        }
        
        /*
        public static Picture GetPicture(int pictureID)
        {
            return DataProviderClass.Instance().GetPicture(pictureID);
        }
        */

        public static List<Picture> GetPicture(List<string> OuterIDList)
        {
            return DataProviderClass.Instance().GetPicture(OuterIDList);
        }
        
        /*
        public static List<Picture> GetPicture(List<int> pictureIDList)
        {
            return DataProviderClass.Instance().GetPicture(pictureIDList);
        }
        */
    
        public static List<Picture> GetPicture(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetPicture(pageIndex, pageSize, out rowCount);
        }
        
        public static List<Picture> GetPicture(Func<Picture, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetPicture(func, pageIndex, pageSize, out rowCount);
        }
    }
}
