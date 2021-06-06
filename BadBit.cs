using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwfulPrimitives
{
    public class BadBit
    {
        private string file;

        public BadBit()
        {
            file = Guid.NewGuid().ToString();
            File.Create(file).Close();
            AppDomain.CurrentDomain.ProcessExit += CleanUp;
        }

        private void CleanUp(object sender, EventArgs e)
        {
            File.Delete(file);
        }

        ~BadBit() => CleanUp(null, null);

        public bool State
        {
            get => File.ReadAllBytes(file)[0] == 1;
            set => File.WriteAllBytes(file, new byte[] { (byte)(value ? 1 : 0)});
        }

        public static implicit operator bool(BadBit bit) => bit.State;
    }
}
