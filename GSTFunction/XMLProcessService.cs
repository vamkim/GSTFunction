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
            var returnValue = new expense();
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(expense));
                StringReader stringReader = new StringReader(xmlStringData);
                returnValue = (Model.expense)serializer.Deserialize(stringReader);
                double totalamount;
                if (String.IsNullOrEmpty(returnValue.total) || !double.TryParse(returnValue.total, out totalamount))
                { throw new InvalidOperationException("Invalid Data"); }
                returnValue.totalGSTexclusionamount = (totalamount * gstpersentage);
                returnValue.totalGSTamount = totalamount - returnValue.totalGSTexclusionamount;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

            return returnValue;
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
                }
                else
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