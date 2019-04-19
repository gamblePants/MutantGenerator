using System;
using System.Windows.Forms;

namespace MutantGenerator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Program, GameSystem and Story ideas by Koan Stevenson
        /// koan.stevenson@gmail.com
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Display());
        }
    }
}
