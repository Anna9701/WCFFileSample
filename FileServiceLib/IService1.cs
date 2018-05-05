using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FileServiceLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IFileService
    {
        [OperationContract]
        [FaultContract(typeof(FileNotOpenedFault))]
        bool CloseFile(String token);

        [OperationContract]
        [FaultContract(typeof(FileAreadyInUseFault))]
        String OpenFile();

        [OperationContract]
        [FaultContract(typeof(FileNotOpenedFault))]
        void WriteToFile(String text);

        [OperationContract]
        [FaultContract(typeof(FileNotOpenedFault))]
        String ReadFromFile(int length);

        [OperationContract]
        [FaultContract(typeof(FileNotOpenedFault))]
        String ReadAllFromFile();

        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "FileServiceLib.ContractType".
    [DataContract]
    internal class File
    {
        [DataMember]
        internal bool IsOpened { get; set; }
        [DataMember]
        internal String Content { get; set; }
    }

    [DataContract]
    public class FileAreadyInUseFault
    {
        private string operation;
        private string message;

        [DataMember]
        public string Operation
        {
            get { return operation; }
            set { operation = value; }
        }

        [DataMember]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }

    [DataContract]
    public class FileNotOpenedFault
    {
        private string operation;
        private string message;

        [DataMember]
        public string Operation
        {
            get { return operation; }
            set { operation = value; }
        }

        [DataMember]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
