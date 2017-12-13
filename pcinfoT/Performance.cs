using System.Diagnostics;

namespace pcinfoT
{
    class Performance : IGetPerformance
    {
        private string x;
        private string y;
        private string z;
        private double value;
        PerformanceCounter tmp;

        public Performance() { }

        public Performance(string x, string y)
        {
            this.x = x;
            this.y = y;
            tmp = new PerformanceCounter(x, y);
        }

        public Performance(string x, string y, string z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            tmp = new PerformanceCounter(x, y, z);
        }

        public double GetElement()
        {
            try
            {
                value = tmp.NextValue();
                return value;
            }
            catch
            {
                return -1;
            }
        }
    }
}
