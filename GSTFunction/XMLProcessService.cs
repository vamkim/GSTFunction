using GSTFunction.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace GSTFunction
{
    public class XMLProcessService : IXmlProcessService
    {
        public expense convertXMLtoData(string xmlStringData, double gstpersentage)
        {
            throw new NotImplementedException();
        }

        public string extractXMLinText(string requestBody = "")
        {
            string returnValue = "";
            try
            {
                int startposition = requestBody.IndexOf("<expense>");
                int endposition = requestBody.IndexOf("</expense>");
                if (startposition > 0 && endposition > 0)
                {
                    returnValue = requestBody.Substring(startposition, endposition - startposition + 10);
                } else
                {
                    throw new InvalidOperationException("Invalid Data");
                }
                  
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            return returnValue;
           
        }
    }
}
