using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IPicture
    {       
        ReturnType AddPicture(Picture picture);
       
        ReturnType AddPicture(List<Picture> pictureList);
        
        ReturnType RemoveAllPicture();
       
        ReturnType RemovePicture(Func<Picture, bool> func);

        ReturnType RemovePicture(string OuterID);

        ReturnType RemovePicture(List<string> OuterIDList);
       
        ReturnType UpdatePicture(Picture picture);

        ReturnType UpdatePicture(string OuterID, Picture picture);
       
        List<Picture> GetAllPicture();
      
        List<Picture> GetPicture(Func<Picture, bool> func);

        List<Picture> GetPicture(List<string> OuterIDList);
       
        List<Picture> GetPicture(int pageIndex, int pageSize, out int rowCount);
        
        List<Picture> GetPicture(Func<Picture, bool> func, int pageIndex, int pageSize, out int rowCount);

        Picture GetPicture(string OuterID);
         
        /*        
        ReturnType RemovePicture(int pictureID);
        
        ReturnType RemovePicture(List<int> pictureIDList);
        
        ReturnType UpdatePicture(int pictureID,Picture picture);
        
        List<Picture> GetPicture(List<int> pictureIDList);
        */
    }
}
