using ApiDatabaseServices.ViewModels;
using ApiServices.Interfaces;
using ApiServices.ViewModels;
using CsvReader.API.Helpers;
using CsvReaderAPI.Services.Interfaces;
using CsvReaderAPI.Services.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Fonts;
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
            GlobalFontSettings.FontResolver = new CustomFontResolver();
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

                var organization = _dbService.GetEntityById<Organization>(id, "Organizations", "Organization_Id");

                string formattedInformation = $"Organization Information:\n" +
                                              $"Index: {organization.Index}\n" +
                                              $"Organization ID: {organization.Organization_Id}\n" +
                                              $"Name: {organization.Name}\n" +
                                              $"Website: {organization.Website}\n" +
                                              $"Founded: {organization.Founded}\n" +
                                              $"Description: {organization.Description}\n" +
                                              $"Industry: {organization.Industry}\n" +
                                              $"Number Of Employees: {organization.NumberOfEmployees}\n" +
                                              $"Country id: {organization.CountryId}";

                XRect layoutRectangle = new XRect(10, 10, page.Width, page.Height);
                XTextFormatter textFormatter = new XTextFormatter(gfx);
                textFormatter.DrawString(formattedInformation, font, XBrushes.Black, layoutRectangle, XStringFormats.TopLeft);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    document.Save(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
        public int CreateOrganization(OrganizationViewModel model)
        {
            try
            {
                Organization organization = new Organization()
                {
                    CountryId = model.CountryId,
                    Index = model.Index,
                    Name = model.Name,
                    Organization_Id = model.Organization_Id,
                    Website = model.Website,
                    Founded = model.Founded,
                    Description = model.Description,
                    Industry = model.Industry,
                    NumberOfEmployees = model.NumberOfEmployees,
                };
                _dbService.InsertData(organization);
                return StatusCodes.Status201Created;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCodes.Status400BadRequest;
            }
        }
        public int DeleteOrganization(string organizationId,string accountId)
        {
            var account = _dbService.GetEntityById<Account>(accountId, "Accounts", "Id");
            if (account.UserType == "Admin")
            {
                try
                {

                    _dbService.SoftDeleteOrganization(organizationId);
                    return StatusCodes.Status200OK;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCodes.Status400BadRequest;
                }
            }
            return StatusCodes.Status401Unauthorized;
        }
    }
}
