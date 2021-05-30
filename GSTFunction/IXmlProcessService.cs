using System;
using System.Collections.Generic;
using System.Text;
using Model = GSTFunction.Model;

namespace GSTFunction
{
    public interface IXmlProcessService
    {
        string extractXMLinText(string requestBody = "");

        Model.expense convertXMLtoData(string xmlStringData, double gstpersentage);
    }
}
