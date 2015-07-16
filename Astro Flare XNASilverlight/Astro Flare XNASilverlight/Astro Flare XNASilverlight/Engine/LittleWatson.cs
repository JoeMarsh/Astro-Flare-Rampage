using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Phone.Tasks;

namespace AstroFlare
{
    public class LittleWatson
    {

        const string filename = "LittleWatson.txt";
        static string contents = null;


        internal static void ReportException(Exception ex, string extra)
        {
          try
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    SafeDeleteFile(store);

                    using (TextWriter output = new StreamWriter(store.CreateFile(filename)))
                    {
                        output.WriteLine(extra);

                        output.WriteLine(ex.Message);

                        output.WriteLine(ex.StackTrace);
                    }
                }
            }

            catch (Exception)
            {

            }
        }


        internal static void CheckForPreviousException()
        {
            try
            {

                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (store.FileExists(filename))
                    {

                        using (TextReader reader = new StreamReader(store.OpenFile(filename, FileMode.Open, FileAccess.Read, FileShare.None)))
                        {
                            contents = reader.ReadToEnd();
                        }
                        SafeDeleteFile(store);
                    }
                }

                if (contents != null)
                {
                    List<string> MBOPTIONS = new List<string>();
                    MBOPTIONS.Add("YES");
                    MBOPTIONS.Add("NO");
                    string msg = "A problem occurred the last time you ran this application. Would you like to send an email of the generated error report?";
                    Guide.BeginShowMessageBox(
                            "Problem Report", msg, MBOPTIONS, 0,
                            MessageBoxIcon.Alert, GetMBResult, null);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                SafeDeleteFile(IsolatedStorageFile.GetUserStoreForApplication());
            }
        }

        private static void SafeDeleteFile(IsolatedStorageFile store)
        {
            try
            {
                store.DeleteFile(filename);
            }
         catch (Exception ex)
            {

            }
        }

        static void GetMBResult(IAsyncResult r)
        {
            int? b = Guide.EndShowMessageBox(r);

            if (b == 0)
            {
                EmailComposeTask email = new EmailComposeTask();

                email.To = "digitalmarsh@gmail.com";

                email.Subject = "Astro Flare auto-generated error report";

                email.Body = contents;

                SafeDeleteFile(IsolatedStorageFile.GetUserStoreForApplication()); // line added 1/15/2011
                email.Show();
            }
        }

    }
}
