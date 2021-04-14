using FlightSimulatorInspection.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace FlightSimulatorInspection.ViewModels
{
    public class VideoVM : BaseViewModel
    {
        #region DLL import for FG window process and other consts
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWnd, IntPtr hWndParent);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
        public const int SWP_NOZORDER = 0x0004;
        public const int SWP_NOACTIVATE = 0x0010;
        public const int GWL_STYLE = -16;
        public const int WS_CAPTION = 0x00C00000;
        public const int WS_THICKFRAME = 0x00040000;
        #endregion
        private DataBase dbModel;
        private BaseModel model;
        private System.Windows.Forms.Panel panel;
        private Process processEXE;

        public VideoVM(DataBase db)
        {
            panel = new System.Windows.Forms.Panel();
            this.model = FlightStats.Instance;
            model.PropertyChanged += (object sender, PropertyChangedEventArgs e) => NotifyPropertyChanged("VM_" + e.PropertyName);

            this.dbModel = db;
            dbModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(dbModel.FGPath))
                {
                    runEXE(dbModel.FGPath);
                }
            };
        }

        #region Properties
        public Panel Panel
        {
            get
            {
                return this.panel;
            }
        }
        
        public string VM_Heading
        {
            get
            {
                return (model as FlightStats)[FlightStats.Stats.Heading.ToString()].ToString();
            }
        }
        public string VM_FGPath
        {
            get
            {
                return dbModel.FGPath;
            }
        }
        public string VM_SideSlip
        {
            get
            {
                return (model as FlightStats)[FlightStats.Stats.SideSlip.ToString()].ToString();
            }
        }
        public string VM_Airspeed
        {
            get
            {
                return (model as FlightStats)[FlightStats.Stats.Airspeed.ToString()].ToString();
            }
        }
        public string VM_Roll
        {
            get
            {
                return (model as FlightStats)[FlightStats.Stats.Roll.ToString()].ToString();
            }
        }
        public string VM_Pitch
        {
            get
            {
                return (model as FlightStats)[FlightStats.Stats.Pitch.ToString()].ToString();
            }
        }
        public string VM_Altimeter_indicated_altitude_ft
        {
            get
            {
                return (model as FlightStats)[FlightStats.Stats.Altimeter_indicated_altitude_ft.ToString()].ToString();
            }
        }
        #endregion

        public void runEXE(string exePath)
        {
            string newLine = Environment.NewLine;
            string commandLineArgument = " --generic=socket,in,10,127.0.0.1,5400,tcp,playback_small --fdm=null --httpd=8080";

            ProcessStartInfo psi = new ProcessStartInfo(exePath, commandLineArgument);
            string pathOnly = System.IO.Path.GetDirectoryName(exePath);
            string fileNameOnly = System.IO.Path.GetFileName(exePath);


            processEXE = Process.Start(psi);
            processEXE.WaitForInputIdle();
            Thread.Sleep(2000);
            SetParent(processEXE.MainWindowHandle, panel.Handle);
            // remove control box
            int style = GetWindowLong(processEXE.MainWindowHandle, GWL_STYLE);
            style = style & ~WS_CAPTION & ~WS_THICKFRAME;
            SetWindowLong(processEXE.MainWindowHandle, GWL_STYLE, style);

            // resize embedded application & refresh
            ResizeEmbeddedApp();
        }
        public void OnClosing()
        {
            if (processEXE != null)
            {
                processEXE.Refresh();
                processEXE.Close();
            }
            dbModel.OnClosing();
        }
        public void ResizeEmbeddedApp()
        {
            if (processEXE == null)
                return;
            SetWindowPos(processEXE.MainWindowHandle, IntPtr.Zero, 0, 0, (int)panel.ClientSize.Width, (int)panel.ClientSize.Height, SWP_NOZORDER | SWP_NOACTIVATE);

        }

    }
}
