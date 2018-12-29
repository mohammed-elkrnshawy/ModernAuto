using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Auto
{
    class More
    {
        public static void Backup(String path)
        {
            Ezzat.ExecutedNoneQuery("Backup_db", new System.Data.SqlClient.SqlParameter("@path", path));
        }
        public static void Restore(String path)
        {
            string query = string.Format("alter database  [Modern_Auto] set offline with ROLLBACK IMMEDIATE;RESTORE DATABASE[Modern_Auto] FROM  DISK = '{0}';", path);
            Ezzat.ExecutedNoneQuery(query);
        }
    }
}
