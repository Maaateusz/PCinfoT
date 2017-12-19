using System.Diagnostics;
using System;

namespace pcinfoT
{
    class Performance : IGetPerformance
    {
        private string x;
        private string y;
        private string z;
        private double value;
        private string error;
        PerformanceCounter tmp;

        public Performance() { }

        public Performance(string x, string y)
        {
            this.x = x;
            this.y = y;
            try
            {
                tmp = new PerformanceCounter(x, y);
            }
            catch (Exception e)
            {
                error = e.ToString();
            }
        }

        public Performance(string x, string y, string z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            try
            {
                tmp = new PerformanceCounter(x, y, z);
            }
            catch (Exception e)
            {
                error = e.ToString();
            }
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
