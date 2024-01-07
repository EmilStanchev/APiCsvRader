using Data.Helpers;
using Data.Implementation;
using Data.Interfaces;
using Ninject.Modules;
using Services.Controllers;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReader.NinjectConfiguration
{
    public class ReaderModule: NinjectModule
    {
        public override void Load()
        {

            Bind<IPrinter>().To<Printer>().InSingletonScope();
            Bind<IReaderConfiguration>().To<ReaderConfiguration>().InSingletonScope();
            Bind<IDatabaseService>().To<DatabaseService>().InSingletonScope();
            Bind<IDataInserter>().To<DataInserter>().InSingletonScope();
            Bind<ICsvReaderService>().To<CsvReaderService>().InSingletonScope();
            Bind<CsvReaderController>().To<CsvReaderController>().InSingletonScope();


        }
    }
}
