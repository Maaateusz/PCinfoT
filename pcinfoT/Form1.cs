using System;
using System.Drawing;
using System.Windows.Forms;

namespace pcinfoT
{
    public partial class Form1 : Form
    {
        #region Hardware
        Hardware processor = new Hardware("Win32_Processor");
        Hardware system = new Hardware("Win32_OperatingSystem");
        Hardware video = new Hardware("Win32_VideoController");
        Hardware keyboard = new Hardware("Win32_Keyboard");
        Hardware mouse = new Hardware("Win32_PointingDevice");
        Hardware disk = new Hardware("Win32_DiskDrive");
        Hardware motherboard = new Hardware("Win32_BaseBoard");
        Hardware bios = new Hardware("Win32_BIOS");
        Hardware physicalMemory = new Hardware("Win32_PhysicalMemory");
        Hardware sound = new Hardware("Win32_SoundDevice");
        Hardware monitor = new Hardware("Win32_DesktopMonitor");
        Hardware computerSystem = new Hardware("Win32_ComputerSystem");
        #endregion

        #region Performance
        PowerStatus power = SystemInformation.PowerStatus;
        Performance processorTime = new Performance("Processor Information", "% Processor Time", "_Total");
        Performance processorInterrupts = new Performance("Processor Information", "Interrupts/sec", "_Total");
        Performance processorDPCt = new Performance("Processor Information", "% DPC Time", "_Total");
        Performance processorDPCq = new Performance("Processor Information", "DPCs Queued/sec", "_Total");
        Performance processorFrequency = new Performance("Processor Information", "Processor Frequency","_Total");
        Performance uptime = new Performance("System", "System Up Time");
        Performance diskTime = new Performance("PhysicalDisk", "% Disk Time", "_Total");
        Performance diskReadTime = new Performance("PhysicalDisk", "% Disk Read Time", "_Total");
        Performance diskWriteTime = new Performance("PhysicalDisk", "% Disk Write Time", "_Total");
        Performance diskRead = new Performance("PhysicalDisk", "Disk Reads/sec", "_Total");
        Performance diskWrite = new Performance("PhysicalDisk", "Disk Writes/sec", "_Total");
        Performance diskIdleTime = new Performance("PhysicalDisk", "% Idle Time", "_Total");
        Performance diskFreeSpace = new Performance("LogicalDisk", "Free Megabytes", "_Total");
        Performance ram = new Performance("Memory", "Available MBytes");
        Performance ramCache = new Performance("Memory", "Cache Bytes");
        Performance ramCommitLimit = new Performance("Memory", "Commit Limit");
        Performance ramCommited = new Performance("Memory", "Committed Bytes");
        #endregion

        private double processorTimeValue;
        private double diskTimeValue;
        private double diskWriteTimeValue;
        private double diskReadTimeValue;

        public Form1()
        {
            InitializeComponent();

            #region SetStaticLabels
            /*---General-------------------------------------------*/
            label46.Text = processor.GetElement("Name");
            label38.Text = Environment.MachineName;
            label40.Text = Environment.OSVersion.ToString();
            label39.Text = Environment.UserName.ToString();
            label41.Text = system.GetElement("Caption");
            label44.Text = video.GetElement("Name");
            /*---General-------------------------------------------*/

            /*---CPU-------------------------------------------*/
            label6.Text = processor.GetElement("Name");
            label51.Text = processor.GetElement("Caption");
            label45.Text = processor.GetElement("L2CacheSize") + " KB";
            label58.Text = processor.GetElement("L3CacheSize") + " KB";
            label48.Text = processor.GetElement("MaxClockSpeed") + " MHz";
            label50.Text = processor.GetElement("NumberOfCores");
            label49.Text = processor.GetElement("NumberOfLogicalProcessors");
            label60.Text = processor.GetElement("ProcessorId");
            label98.Text = processor.GetElement("AddressWidth");
            /*---CPU-------------------------------------------*/

            /*---Disc-------------------------------------------*/
            label70.Text = disk.GetElement("Model");
            label9.Text = (Convert.ToInt64(disk.GetElement("Size")) /(1024*1024)).ToString() + " MB";
            /*---Disc-------------------------------------------*/

            /*---Motherboard & RAM-------------------------------------------*/
            label13.Text = motherboard.GetElement("Description");
            label84.Text = motherboard.GetElement("Manufacturer");
            label74.Text = (Convert.ToInt64( computerSystem.GetElement("TotalPhysicalMemory") ) / (1024 * 1024)).ToString() + " MB";
            label71.Text = motherboard.GetElement("Version");
            label72.Text = bios.GetElement("Description"); 
            label94.Text = physicalMemory.GetElement("Speed") + " MHz";
            label96.Text = physicalMemory.GetElement("Manufacturer");
            /*---Motherboard & RAM-------------------------------------------*/

            /*---Other-------------------------------------------*/
            label60.Text = keyboard.GetElement("Name");
            label62.Text = mouse.GetElement("Name");
            label100.Text = sound.GetElement("Name");
            label102.Text = monitor.GetElement("Name");
            /*---Other-------------------------------------------*/
            #endregion
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region SetDynamicLabels
            /*---General-------------------------------------------*/  
            label43.Text = DateTime.Now.ToString();
            label42.Text = string.Format("{0:0} h {1:0} min", (uptime.GetElement()/3600 ), (uptime.GetElement() / 60) - (((int)uptime.GetElement() / 3600) * 60));
            /*---General-------------------------------------------*/

            /*---CPU-------------------------------------------*/
            processorTimeValue = processorTime.GetElement();
            chart1.Series["SeriesCPU"].Points.AddY(processorTimeValue);
            if (processorTimeValue > 95) label54.BackColor = Color.Red;
            else label54.BackColor = Color.Transparent;
            label54.Text = string.Format("{0:0.00} %", processorTimeValue);
            label56.Text = string.Format("{0:0.00} %", processorDPCt.GetElement());
            label53.Text = string.Format("{0:0}", processorDPCq.GetElement());
            label55.Text = string.Format("{0:0}", processorInterrupts.GetElement());
            label47.Text = processorFrequency.GetElement().ToString() + " MHz";
            /*---CPU-------------------------------------------*/

            /*---Disc-------------------------------------------*/
            diskReadTimeValue = diskReadTime.GetElement();
            diskWriteTimeValue = diskWriteTime.GetElement();
            diskTimeValue = diskTime.GetElement();
            chart2.Series["DiskTime"].Points.AddY(diskTimeValue);
            chart2.Series["WriteTime"].Points.AddY(diskWriteTimeValue);
            chart2.Series["ReadTime"].Points.AddY(diskReadTimeValue);
            label64.Text = string.Format("{0:0.00} %", diskTimeValue);
            label65.Text = string.Format("{0:0.00} %", diskReadTimeValue);
            label66.Text = string.Format("{0:0.00} %", diskWriteTimeValue);
            label14.Text = string.Format("{0:0.000} KB", diskWrite.GetElement() / 10*1024);
            label67.Text = string.Format("{0:0.000} KB", diskRead.GetElement() / 10*1024);
            label68.Text = string.Format("{0:0.00} %", diskIdleTime.GetElement());
            label10.Text = diskFreeSpace.GetElement().ToString() + " MB";
            /*---Disc-------------------------------------------*/

            /*---Motherboard & RAM-------------------------------------------*/
            label73.Text = ram.GetElement().ToString() + " MB";
            label75.Text = string.Format("{0:0.00} MB", ramCache.GetElement() / (1024 * 1024));
            label76.Text = string.Format("{0:0} MB", ramCommitLimit.GetElement() / (1024 * 1024));
            label77.Text = string.Format("{0:0} MB", ramCommited.GetElement() / (1024 * 1024));
            /*---Motherboard & RAM-------------------------------------------*/

            /*---Other-------------------------------------------*/
            label78.Text = power.BatteryChargeStatus.ToString();
            label79.Text = string.Format("{0:0} %", power.BatteryLifePercent * 100);
            label81.Text = string.Format("{0:0} min", power.BatteryLifeRemaining / 60);
            label82.Text = power.PowerLineStatus.ToString();
            /*---Other-------------------------------------------*/
            #endregion
        }

        #region menuStrip
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            timer1.Interval = 250;
            GraphClear();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            timer1.Interval = 500;
            GraphClear();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            timer1.Interval = 750;
            GraphClear();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            GraphClear();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            timer1.Interval = 2000;
            GraphClear();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            timer1.Interval = 5000;
            GraphClear();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            timer1.Interval = 10000;
            GraphClear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void resetGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphClear();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoBox();
        }
        #endregion

        private void GraphClear()
        {
            chart1.Series["SeriesCPU"].Points.Clear();
            chart2.Series["DiskTime"].Points.Clear();
            chart2.Series["WriteTime"].Points.Clear();
            chart2.Series["ReadTime"].Points.Clear();
        }

        private void chart1_DoubleClick(object sender, EventArgs e)
        {
            GraphClear();
        }

        private void chart2_DoubleClick(object sender, EventArgs e)
        {
            GraphClear();
        }

        static private void InfoBox()
        {
            string text = "Created by Mateusz Sołoducha" + Environment.NewLine + "Working on Windows 10, Windows 8.1 and Windows 7.";
            MessageBox.Show(text, "PCInfoT Version 1.0");
        }

        private void label43_MouseHover(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }

        private void label43_MouseLeave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InfoBox();
        }
    }
}
