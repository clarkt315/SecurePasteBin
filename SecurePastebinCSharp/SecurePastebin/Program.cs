// This is licensed under the GNU General Public License v3.0
// See license.txt for details.
// REMINDER:
// THERE IS NO WARRANTY FOR THE PROGRAM, TO THE EXTENT PERMITTED BY
// APPLICABLE LAW.EXCEPT WHEN OTHERWISE STATED IN WRITING THE COPYRIGHT
// HOLDERS AND/OR OTHER PARTIES PROVIDE THE PROGRAM "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, BUT NOT LIMITED TO,
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
// PURPOSE.THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE PROGRAM
// IS WITH YOU.SHOULD THE PROGRAM PROVE DEFECTIVE, YOU ASSUME THE COST OF
// ALL NECESSARY SERVICING, REPAIR OR CORRECTION.

// Credit:
// MSDN AES encryption example: 
// https://msdn.microsoft.com/en-us/library/system.security.cryptography.aes(v=vs.110).aspx
// Jay Tuley's AES / HMAC implementation
// https://gist.github.com/jbtule/4336842#file-aesthenhmac-cs
// HMAC-based Extract-and-Expand Key Derivation Function (HKDF)
// H. Krawczyk, P. Eronen
// Internet Engineering Task Force
// https://tools.ietf.org/html/rfc5869

using System;
using System.Windows.Forms;

namespace SecurePastebin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // This form asks the user if they want to create a new paste or open an existing one.
            Application.Run(new MainForm());
        }
    }
}
