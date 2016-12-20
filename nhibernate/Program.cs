using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HibernatingRhinos.Profiler.Appender;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate.Cfg;
using NHibernate.Loader.Custom;
using NHibernate.Spatial.Mapping;
using NHibernate.Spatial.Metadata;

namespace nhibernate
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize profiler integration
            NHibernateProfiler.Initialize();
            try
            {

                var cfg = new Configuration()
                    .Configure("nhibernate.cfg.xml");
                // the session factory is the entry point to NHibernate
                cfg.AddAuxiliaryDatabaseObject(new SpatialAuxiliaryDatabaseObject(cfg));
                Metadata.AddMapping(cfg, MetadataClass.GeometryColumn);
                Metadata.AddMapping(cfg, MetadataClass.SpatialReferenceSystem);
                var sessionFactory = cfg.BuildSessionFactory();



                // the session is what we use to actually 
                using (var session = sessionFactory.OpenSession())
                {
                    
                    using (var tx = session.BeginTransaction())
                    {
                        tx.Commit();
                    }

                }


            }
            finally
            {
                // we need this so we wouldn't exit the process
                // before all the work was sent to the profiler
                ProfilerInfrastructure.FlushAllMessages();
            }
        }
    }
}
