using System.Collections.Generic;
using System.Data;
using BusinessLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IImportExportRepository
    {
        void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable stripDatatable);
    }
}