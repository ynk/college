namespace BusinessLayer
{
    using BusinessLayer.Entities;
    using BusinessLayer.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="ImportExportManager" />.
    /// </summary>
    public class ImportExportManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExportManager"/> class.
        /// </summary>
        /// <param name="ouw">The ouw<see cref="IUnitOfWork"/>.</param>
        public ImportExportManager(IUnitOfWork ouw)
        {
            _unitOfWork = ouw;
        }

        /// <summary>
        /// Defines the _unitOfWork.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// The ToDataTable.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="data">The data<see cref="List{T}"/>.</param>
        /// <returns>The <see cref="DataTable"/>.</returns>
        public DataTable ToDataTable<T>(List<T> data)
        {
            var type = typeof(T);
            //Om type te weten van T typeof(T), dan vragen we de properties (bv id en naam) van dit type (type kan author/publisher/serie etc zijn
            var props = TypeDescriptor.GetProperties(type);


            var table = new DataTable();
            table.TableName = type.Name;
            for (var i = 0; i < props.Count; i++)
            {
                var prop = props[i];
                //check of dat prop nullable is
                if (prop.PropertyType.IsGenericType &&
                    prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    //als het nullable is, haal de onderliggende type bv int?
                    var col = table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                    col.AllowDBNull = true;
                }
                else
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
            }

            var values = new object[props.Count];
            foreach (var item in data)
            {
                for (var i = 0; i < values.Length; i++) values[i] = props[i].GetValue(item);
                table.Rows.Add(values);
            }

            return table;
        }

        /// <summary>
        /// Leest Json uit van bestand en schrijft json naar bestand.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        public static class JSONSerializer<T> where T : class
        {
            /// <summary>
            /// Serializes an object to JSON.
            /// </summary>
            /// <param name="instance">The instance<see cref="T"/>.</param>
            /// <param name="filePath">The filePath<see cref="string"/>.</param>
            public static void Serialize(T instance, string filePath)
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                using (var stream = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    serializer.WriteObject(stream.BaseStream, instance);
                }
            }

            /// <summary>
            /// DeSerializes an object from JSON.
            /// </summary>
            /// <param name="filePath">The filePath<see cref="string"/>.</param>
            /// <returns>The <see cref="T"/>.</returns>
            public static T DeSerialize(string filePath)
            {
                using (var stream = new StreamReader(filePath))
                {
                    var serializer = new DataContractJsonSerializer(typeof(T));
                    return serializer.ReadObject(stream.BaseStream) as T;
                }
            }
        }

        /// <summary>
        /// The SaveAllToDB.
        /// </summary>
        /// <param name="comics">The comics<see cref="List{Comic}"/>.</param>
        /// <param name="comicAuthors">The comicAuthors<see cref="List{ComicAuthor}"/>.</param>
        public void SaveAllToDB(List<Comic> comics, List<ComicAuthor> comicAuthors)
        {
            var comicIdList = comics.Select(x => x.Id).ToList();
            _unitOfWork.ComicAuthorRepo.RemoveAllComicAuthorsForComicIds(comicIdList);

            //En dan voegen we alle onze records toe
            //Omdat de klasse comic niet 1op1 mapt voor onze db, maken we met de hand een datatable aan ipv generic. 
            _unitOfWork.ImportExportRepo.InsertDataIntoSQLServerUsingSQLBulkCopy(ToDataTable(comicAuthors));
        }
    }
}
