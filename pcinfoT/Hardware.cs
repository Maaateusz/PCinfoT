using System;
using System.Management;

namespace pcinfoT
{
    class Hardware : IGetHardware
    {
        private string from;
        private static string select = "select * from ";
        private string inf;
        ManagementObjectSearcher management;

        public string Inf
        {
            get
            {
                return inf;
            }
            set
            {
                inf = value;
            }
        }

        public Hardware() { }

        public Hardware(string from)
        {
            this.from = from;
            management = new ManagementObjectSearcher(select + from);
        }

        public string GetElement(string element)
        {
            try
            {
                foreach (ManagementObject obj in management.Get())
                {
                    this.inf = Convert.ToString(obj[element]);
                }
                return inf;
            }
            catch
            {
                return null;
            }
        }
    }
}
