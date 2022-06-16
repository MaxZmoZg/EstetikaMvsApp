using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Estetika.Models.Entities
{
    public partial class Zapis : IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Data))
                {
                    if (!Data.HasValue)
                    {
                        return "Укажите дату";
                    }
                    else if (Data.Value.Date < System.DateTime.Now.Date)
                    {
                        return "Дата должна быть позднее текущей даты";
                    }
                }

                if (columnName == nameof(Vremya))
                {
                    if (!Vremya.HasValue)
                    {
                        return "Укажите время";
                    }
                    else if (Vremya.HasValue)
                    {
                        if (!Data.HasValue)
                        {
                            return "Укажите дату перед указанием времени";
                        }
                        else
                        {
                            using (SalonEntities entities = new SalonEntities())
                            {
                                bool isRequestForGivenDateTimeExists = entities.Zapis
                                    .ToList()
                                    .Any(z => z.Data.Value.Date == Data && z.Vremya.Value == Vremya);
                                if (isRequestForGivenDateTimeExists)
                                {
                                    return "На указанные время и дату уже есть запись, выберите другие время и дату";
                                }
                            }
                        }
                    }
                }

                if (columnName == nameof(ID_Master))
                {
                    if (ID_Master == 0)
                    {
                        return "Выберите мастера";
                    }
                }

                return null;
            }
        }

        public string Error
        {
            get
            {
                IEnumerable<string> propertyNames = GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Select(p => p.Name);
                StringBuilder errors = new StringBuilder();

                foreach (var propertyName in propertyNames)
                {
                    if (this[propertyName] != null)
                    {
                        errors.AppendLine(propertyName + ": " + this[propertyName]);
                    }
                }
                return errors.ToString();
            }
        }
    }
}