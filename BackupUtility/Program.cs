/* User data backup and restore utility
 * Purpose: Backup and restore predetermined user folders/files to network share. 
 *          Also dump to text files: installed apps, printers, mapped network drives
 *          backup folder naming convention: CurrentDate_UserName
 * Initially, this app will only do backups for XP PCs.
 * 
 * utilizing Map Network Drive API - By aejw
 * http://www.aejw.com/default.aspx?page=dev/code/cnetworkdrive
 * 
 * 
 * TODO: restore to XP
 * TODO: backup Win7
 * TODO: restore to Win7
 * TODO: determine if XP/7 and backup accordingly
 * TODO: <maybe> progress bar
*/

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BackupUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            aejw.cNetworkDrive netDrive = new aejw.cNetworkDrive();
            try
            {
                netDrive.LocalDrive = "z:";
                netDrive.ShareName = "\\\\Main\\test";
                netDrive.MapDrive();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Drive has been mapped");
            
            
            // Getting list of user profile folders
            try
            {
                string dirPath = @"\Users";

                List<string> dirs = new List<string>(Directory.EnumerateDirectories(dirPath));

                foreach (var dir in dirs)
                {
                    Console.WriteLine("{0}", dir.Substring(dir.LastIndexOf("\\") + 1));
                }
                Console.WriteLine("{0} directories found.", dirs.Count);
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }

            //// Unmapping a network drive
            //try
            //{
            //    netDrive.LocalDrive = "m:";
            //    netDrive.UnMapDrive();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //Console.WriteLine("Drive has been unmapped.");
            //netDrive = null;
            Console.ReadLine();
        }
    }
}
