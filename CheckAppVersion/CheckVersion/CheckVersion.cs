using System;
using System.Net;
using System.Xml.XPath;

namespace CheckVersion
{
    public class CheckVersion
    {
        private Version newVersion = null;
        private string errorMessage;

        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
            }
        }


        //current version of application 
        private Version currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

        //url assigned to current applications versions
        private string url = @"http://domain/versions.xml";

        //application name
        private string applicationName = "ApplicationTwo";

        // return number of new version
        private Version checkVersion()
        {
            if (checkXmlUrl())
            {
                try
                {
                    XPathDocument oXPathDocument = new XPathDocument(url);
                    XPathNavigator oXPathNavigator = oXPathDocument.CreateNavigator();
                    XPathNodeIterator oApplicationNodesIterator = oXPathNavigator.Select("/Applications/Application");

                    foreach (XPathNavigator oCurrentApplication in oApplicationNodesIterator)
                    {
                        if (oCurrentApplication.SelectSingleNode("Name").Value == applicationName)
                        {
                            newVersion = new Version(oCurrentApplication.SelectSingleNode("Version").Value);
                        }
                    }
                }
                catch (Exception oException)
                {
                    errorMessage = oException.Message;
                }
            }
            return newVersion;
        }


        //check if url with new version exixsts
        private bool checkXmlUrl()
        {
            try
            {
                var request = WebRequest.Create(url);
                var response = request.GetResponse();
                return true;
            }
            catch (WebException we)
            {
                errorMessage = "URL not exixts";
            }
            return false;
        }


        //compare versions
        public bool NewVersionCheck()
        {
            checkVersion();
            if (currentVersion.CompareTo(newVersion) < 0)
            {
                return true;
            }

            return false;
        }
    }
}
