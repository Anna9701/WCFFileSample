using FileServiceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GettingStartedClient
{
    class Program
    {
        static void Main(string[] args)
        {
            FileServiceClient client = new FileServiceClient();
            String text = "hello";
            try {
                String token = client.OpenFile();
                client.WriteToFile(text);
                client.WriteToFile(text);
                Console.WriteLine("Result {0}", client.ReadFromFile(text.Length));
                Console.ReadLine();
                client.CloseFile(token);
                client.Close();
            } catch (TimeoutException timeProblem)
            {
                Console.WriteLine("The service operation timed out. " + timeProblem.Message);
                Console.ReadLine();
                client.Abort();
            }
            catch (FaultException<FileAreadyInUseFault> alreadyOpenFault)
            {
                Console.WriteLine("The file is already in use. " + alreadyOpenFault.Message);
                Console.ReadLine();
                client.Abort();
            }
            catch (FaultException<FileNotOpenedFault> notOpenedFault)
            {
                Console.WriteLine("The file was not opened. " + notOpenedFault.Message);
                Console.ReadLine();
                client.Abort();
            }
            catch (FaultException unknownFault)
            {
                Console.WriteLine("An unknown exception was received. " + unknownFault.Message);
                Console.ReadLine();
                client.Abort();
            }
            catch (CommunicationException commProblem)
            {
                Console.WriteLine("There was a communication problem. " + commProblem.Message + commProblem.StackTrace);
                Console.ReadLine();
                client.Abort();
            }
            Console.ReadLine();
        }
    }
}