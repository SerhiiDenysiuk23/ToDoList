using Repositories.IRepositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Repositories.XMLRepositories
{
    public class XMLCategoryRepository : ICategoryRepository
    {
        private string _filePath;

        public XMLCategoryRepository(string filePath)
        {
            _filePath = filePath;
        }

        private int GenerateId() => (int)(DateTime.Now.Ticks % 1000000000);

        public async Task<Category> Create(Category category)
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

            XElement categoryList = doc.Element("Data")?.Element("CategoryList");
            if (categoryList == null)
            {
                throw new Exception($"The 'CategoryList' element is not found in {_filePath}.");
            }

            int id = GenerateId();
            bool idExists = categoryList.Elements("Category").Any(x => (int)x.Attribute("Id") == id);
            while (idExists)
            {
                id = GenerateId();
                idExists = categoryList.Elements("Category").Any(x => (int)x.Attribute("Id") == id);
            }

            category.Id = id;
            categoryList.Add(new XElement("Category",
                    new XAttribute("Id", category.Id),
                    new XElement("Name", category.Name)
                ));
            try
            {
                doc.Save(_filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save XML to {_filePath}.", ex);
            }

            return category;
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

            XElement categoryList = doc.Element("Data")?.Element("CategoryList");
            if (categoryList == null)
            {
                throw new Exception($"The 'CategoryList' element is not found in {_filePath}.");
            }

            var Category = categoryList.Elements("Category").FirstOrDefault(t => t.Attribute("Id").Value == id.ToString());
            if (Category == null)
            {
                return false;
            }

            Category.Remove();
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

        public async Task<Category> Get(int id)
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

            XElement categoryList = doc.Element("Data")?.Element("CategoryList");
            if (categoryList == null)
            {
                throw new Exception($"The 'CategoryList' element is not found in {_filePath}.");
            }

            var category = categoryList.Elements("Category").FirstOrDefault(t => t.Attribute("Id").Value == id.ToString());
            if (category == null)
            {
                return null;
            }
            return new Category
            {
                Id = int.Parse(category.Attribute("Id").Value),
                Name = category.Element("Name").Value
            };
        }

        public async Task<IEnumerable<Category>> GetList()
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

            XElement categoryList = doc.Element("Data")?.Element("CategoryList");
            if (categoryList == null)
            {
                throw new Exception($"The 'CategoryList' element is not found in {_filePath}.");
            }

            return categoryList.Elements("Category").Select(category => new Category
            {
                Id = int.Parse(category.Attribute("Id").Value),
                Name = category.Element("Name").Value
            }).ToList();
        }
    }
}
