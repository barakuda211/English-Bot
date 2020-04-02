using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace AudioWindowsServiceCs
{
    [RunInstaller(true)]
    public partial class WindowsServiceInstaller : Installer
    {
        public WindowsServiceInstaller()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            ServiceProcessInstaller serviceProcessInstaller =
                               new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            //# Service Account Information

            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            //# Service Information

            serviceInstaller.DisplayName = "Audio Windows Service";
            serviceInstaller.Description = "Audio C# Windows Service";
            serviceInstaller.StartType = ServiceStartMode.Manual;

            //# This must be identical to the WindowsService.ServiceBase name

            //# set in the constructor of AudioService.cs

            serviceInstaller.ServiceName = "Audio Windows Service";

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}