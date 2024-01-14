using ApiServices.Interfaces;
using ApiServices.ViewModels;
using CsvReaderAPI.Services.Interfaces;
using Microsoft.VisualBasic;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderAPI.Services.Implementation
{
    public class OrganizationSerive : IOrganizationService
    {
        private readonly IDatabaseService _dbService;
        public OrganizationSerive(IDatabaseService dbService)
        {
            _dbService = dbService;
        }
        public Organization SelectOrganizationById(string id)
        {
            return _dbService.GetEntityById<Organization>(id, "Organizations", "Organization_Id");
        }
        public async Task<IEnumerable<Organization>> SearchOrganizationByCountry(string countryId)
        {
            return await _dbService.SearchOrganizationByCountry(countryId);
        }
        public Organization GetOrganizationWithMaxEmployees()
        {
            return _dbService.GetOrganizationWithMaxEmployees();
        }
        public byte[] ReturnPdfFileForORganization(string id)
        {
            using (PdfDocument document = new PdfDocument())
            {
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 12);
                string organization = _dbService.GetEntityById<Organization>(id, "Organizations", "Organization_Id").ToString();
                gfx.DrawString($"Information: {organization}", font, XBrushes.Black, new XRect(10, 10, page.Width, page.Height), XStringFormats.TopLeft);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    document.Save(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
