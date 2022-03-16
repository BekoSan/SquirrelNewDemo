using Squirrel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_Application
{
    public partial class MainWindow : Form
    {
        public     MainWindow() 
        {
            InitializeComponent();
            worker.DoWork += Worker_DoWork;
        }

        private async  void  Worker_DoWork(object sender, DoWorkEventArgs e)
        {
           await  CheckForUpdates();
        }

        UpdateManager manager;
        BackgroundWorker worker;

        private void MainWindow_Load(object sender, EventArgs e)
        {
            AddVerisonNumber();
        }

        private void AddVerisonNumber()
        {

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            lbl_version.Text = versionInfo.FileVersion;
        }

        private async Task CheckForUpdates() 
        {
             
            using (manager = await   UpdateManager.GitHubUpdateManager(@"https://github.com/BekoSan/SquirrelNewDemo"))
            {
                await manager.UpdateApp(); 
            }
            
        }

        private void RunRelaseify(string nugetFilePath) 
        {
            //Process.Start(@"D:\Windows Desktop Applications\Demo\SquirrelDemo\packages\squirrel.windows.2.0.1\tools\Squirrel.exe", $"--releasify { nugetFilePath  }" );
            Process.Start(@"Squirrel", $" --releasify {nugetFilePath}"); 
           // RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();

           // Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration);
           // runspace.Open();

           // Pipeline pipeline = runspace.CreatePipeline();

           // //Here's how you add a new script with arguments
           // Command myCommand = new Command($"Squirrel");
           // CommandParameter testParam = new CommandParameter("filepath", $"--releasify");
           // myCommand.Parameters.Add(testParam);
           

           // pipeline.Commands.Add(myCommand);

           // // Execute PowerShell script
           //var results = pipeline.Invoke();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //RunRelaseify(@"D:\Windows Desktop Applications\Demo\SquirrelDemo\DemoApplication.1.0.2.nupkg");
        }
    }
}
