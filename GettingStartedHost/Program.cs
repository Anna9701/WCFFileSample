using FileServiceLib;
using GettingStartedLib2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace GettingStartedHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8000/GettingStarted/");
            ServiceHost selfHost = new ServiceHost(typeof(FileService), baseAddress);
            try
            {
                
               // selfHost.AddServiceEndpoint(typeof(ICalculator), new WSHttpBinding(), "Calculator");
                selfHost.AddServiceEndpoint(typeof(IFileService), new WSHttpBinding(), "FileService");
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true
                };
                selfHost.Description.Behaviors.Add(smb);

                selfHost.Open();
                Console.WriteLine("The service is ready");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                selfHost.Close();
            } catch (CommunicationException ex)
            {
                Console.WriteLine("An exception occurred: {0}", ex.Message);
                selfHost.Abort();
            }
        }
    }
}
