Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.ServiceProcess

Public Class WindowsServiceInstaller

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        Init()
    End Sub

    Private Sub Init()
        Dim serviceProcessInstaller As ServiceProcessInstaller = New ServiceProcessInstaller()
        Dim serviceInstaller As ServiceInstaller = New ServiceInstaller()

        '# Service Account Information

        serviceProcessInstaller.Account = ServiceAccount.LocalSystem
        serviceProcessInstaller.Username = Nothing
        serviceProcessInstaller.Password = Nothing

        '# Service Information

        serviceInstaller.DisplayName = "Audio Windows Service"
        serviceInstaller.Description = "Audio VB.Net Windows Service"
        serviceInstaller.StartType = ServiceStartMode.Manual

        '# This must be identical to the WindowsService.ServiceBase name

        '# set in the constructor of AudioService.vb

        serviceInstaller.ServiceName = "Audio Windows Service"

        Me.Installers.Add(serviceProcessInstaller)
        Me.Installers.Add(serviceInstaller)
    End Sub

End Class
