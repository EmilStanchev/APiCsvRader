// See https://aka.ms/new-console-template for more information
using CsvReader.NinjectConfiguration;
using Data;
using Data.Implementation;
using Ninject;
using Services.Controllers;
using Services.Implementations;

var kernel = new StandardKernel(new ReaderModule());
var readerController = kernel.Get<ReaderController>();
readerController.Start();