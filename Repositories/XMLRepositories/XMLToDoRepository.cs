using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Repositories.IRepositories;
using Repositories.Models;

namespace Repositories.XMLRepositories
{
    public class XMLToDoRepository : IToDoRepository
    {
        private string _filePath;

        public XMLToDoRepository(string filePath)
        {
            _filePath = filePath;
        }

        private int GenerateId() => (int)(DateTime.Now.Ticks % 1000000000);

        public async Task<ToDo> Create(ToDo toDo)
        {
            var doc = new XDocument();
            try
            {
                doc = XDocument.Load(_filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load XML from {_filePath}.", ex);
            }

            XElement toDoList = doc.Element("Data")?.Element("ToDoList");
            if (toDoList == null)
            {
                throw new Exception($"The 'ToDoList' element is not found in {_filePath}.");
            }

            int id = GenerateId();
            bool idExists = toDoList.Elements("ToDo").Any(x => (int)x.Attribute("Id") == id);
            while (idExists)
            {
                id = GenerateId();
                idExists = toDoList.Elements("ToDo").Any(x => (int)x.Attribute("Id") == id);
            }

            toDo.Id = id;
            toDoList.Add(new XElement("ToDo",
                    new XAttribute("Id", toDo.Id),
                    new XElement("Title", toDo.Title),
                    new XElement("Description", toDo.Description),
                    new XElement("DueDate", toDo.DueDate.ToString()),
                    new XElement("Status", toDo.Status),
                    new XElement("CategoryId", toDo.CategoryId)
                ));
            try
            {
                doc.Save(_filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save XML to {_filePath}.", ex);
            }

            return toDo;
        }


        public async Task<bool> Delete(int id)
        {
            var doc = new XDocument();
            try
            {
                doc = XDocument.Load(_filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load XML from {_filePath}.", ex);
            }

            XElement toDoList = doc.Element("Data")?.Element("ToDoList");
            if (toDoList == null)
            {
                throw new Exception($"The 'ToDoList' element is not found in {_filePath}.");
            }

            var toDo = toDoList.Elements("ToDo").FirstOrDefault(t => t.Attribute("Id").Value == id.ToString());
            if (toDo == null)
            {
                return false;
            }

            toDo.Remove();
            try
            {
                doc.Save(_filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save XML to {_filePath}.", ex);
            }

            return true;
        }

        public async Task<ToDo> Get(int id)
        {
            var doc = new XDocument();
            try
            {
                doc = XDocument.Load(_filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load XML from {_filePath}.", ex);
            }

            XElement toDoList = doc.Element("Data")?.Element("ToDoList");
            if (toDoList == null)
            {
                throw new Exception($"The 'ToDoList' element is not found in {_filePath}.");
            }

            var toDo = toDoList.Elements("ToDo").FirstOrDefault(t => t.Attribute("Id").Value == id.ToString());
            if (toDo == null)
            {
                return null;
            }

            XElement categoryList = doc.Element("Data")?.Element("CategoryList");
            if (toDoList == null)
            {
                throw new Exception($"The 'CategoryList' element is not found in {_filePath}.");
            }
            var categories = categoryList.Elements("Category").Select(c => new Category
            {
                Id = int.Parse(c.Attribute("Id").Value),
                Name = c.Element("Name").Value
            }).ToList();

            return new ToDo
            {
                Id = int.Parse(toDo.Attribute("Id").Value),
                Title = toDo.Element("Title").Value,
                Description = toDo.Element("Description").Value,
                DueDate = DateTime.TryParse(toDo.Element("DueDate").Value, out DateTime parsedDT) ? parsedDT : null,
                Status = toDo.Element("Status").Value,
                CategoryId = int.TryParse(toDo.Element("CategoryId").Value, out int parsedId) ? parsedId : null,
                Category = categories.FirstOrDefault(c => c.Id == parsedId)
            };
        }

        public async Task<IEnumerable<ToDo>> GetList()
        {
            var doc = new XDocument();
            try
            {
                doc = XDocument.Load(_filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load XML from {_filePath}.", ex);
            }

            XElement toDoList = doc.Element("Data")?.Element("ToDoList");
            if (toDoList == null)
            {
                throw new Exception($"The 'ToDoList' element is not found in {_filePath}.");
            }

            XElement categoryList = doc.Element("Data")?.Element("CategoryList");
            if (categoryList == null)
            {
                throw new Exception($"The 'CategoryList' element is not found in {_filePath}.");
            }

            var categories = categoryList.Elements("Category").Select(c => new Category
            {
                Id = int.Parse(c.Attribute("Id").Value),
                Name = c.Element("Name").Value
            }).ToList();

            try
            {
                var a = toDoList.Elements("ToDo").Select(toDo => new ToDo
                    {
                        Id = int.Parse(toDo.Attribute("Id").Value),
                        Title = toDo.Element("Title").Value,
                        Description = toDo.Element("Description").Value,
                        DueDate = DateTime.TryParse(toDo.Element("DueDate").Value, out DateTime parsedDT) ? parsedDT : null,
                        Status = toDo.Element("Status").Value,
                        CategoryId = int.TryParse(toDo.Element("CategoryId").Value, out int parsedId) ? parsedId : null,
                        Category = categories.FirstOrDefault(c => c.Id == parsedId)
                    }).ToList();
                return a;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<ToDo> Update(ToDo toDo)
        {

            var doc = new XDocument();
            try
            {
                doc = XDocument.Load(_filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load XML from {_filePath}.", ex);
            }

            XElement toDoList = doc.Element("Data")?.Element("ToDoList");
            if (toDoList == null)
            {
                throw new Exception($"The 'ToDoList' element is not found in {_filePath}.");
            }

            XElement toDoElement = toDoList.Elements("ToDo").FirstOrDefault(t => t.Attribute("Id").Value == toDo.Id.ToString());
            if (toDoElement == null)
            {
                throw new Exception($"The 'ToDo' element with Id={toDo.Id} is not found in {_filePath}.");
            }

            toDoElement.Element("Title").Value = toDo.Title;
            toDoElement.Element("Description").Value = toDo.Description;
            toDoElement.Element("DueDate").Value = toDo.DueDate.ToString();
            toDoElement.Element("Status").Value = toDo.Status.ToString();
            toDoElement.Element("CategoryId").Value = toDo.CategoryId.ToString();
            doc.Save(_filePath);

            return toDo;
        }
    }
}
