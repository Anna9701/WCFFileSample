using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FileServiceLib
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class FileService : IFileService
    {
        private static File file;
        private static String tokenToCloseFile;

        public FileService()
        {
            file = new File
            {
                Content = ""
            };
        }

        public bool CloseFile(String token)
        {
            if (file.IsOpened && tokenToCloseFile.Equals(token))
            {
                file.IsOpened = false;
                return true;
            }
            return false;
        }

        public String OpenFile()
        {
            Console.WriteLine("Open file requested. File is opened: {0}", file.IsOpened);
            
            if (!file.IsOpened)
            {
                Console.WriteLine("Opening file");
                file.IsOpened = true;
                Console.WriteLine("File opened: {0}", file.IsOpened);
                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                tokenToCloseFile = Convert.ToBase64String(time.Concat(key).ToArray());
                return tokenToCloseFile;
            }
            throw new FaultException<FileAreadyInUseFault>(new FileAreadyInUseFault(), "Cannot open already opened file!");
        }

        public string ReadFromFile(int length)
        {
            Console.WriteLine("Read From File. File is open: {0}", file.IsOpened);
            Console.WriteLine("Current file content {0}", file.Content);
            Console.WriteLine("Return {0}", file.Content.Substring(0, length));
            if (file.IsOpened)
                return file.Content.Substring(0, length);
            throw new FaultException<FileNotOpenedFault>(new FileNotOpenedFault(), "Cannot read from not opened file!");
        }

        public string ReadAllFromFile()
        {
            Console.WriteLine("Read From File. File is open: {0}", file.IsOpened);
            Console.WriteLine("Current file content {0}", file.Content);
            if (file.IsOpened)
                return file.Content;
            throw new FaultException<FileNotOpenedFault>(new FileNotOpenedFault(), "Cannot read from not opened file!");
        }

        public void WriteToFile(string text)
        {
            Console.WriteLine("Write to file. File is open: {0}", file.IsOpened);
            Console.WriteLine("Write {0} to file requested", text);
            if (file.IsOpened)
            {
                file.Content += text;
            } else
            {
                throw new FaultException<FileNotOpenedFault>(new FileNotOpenedFault(), "Cannot write to not opened file!");
            }

        }
    }
}
