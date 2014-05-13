using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generic.Entity.Repository;

namespace generic_entity_usage
{
    public class Sample
    {
        //This way you can initialize this repository for any kind of entity you have.
        private IRepository<SampleEntity> _sampleRep;

        public Sample(IRepository<SampleEntity> sampleRep)
        {
            //Structure map will initialize object of this rep for you.
            _sampleRep = sampleRep;

            //OR old tradiional way to initialize.
            //_sampleRep = new Repository<SampleEntity>(new DataBaseSettings());
        }

        public void TestRepositoryMethods()
        {
            //Get by id by passing condition as expression.
            SampleEntity sampleEntity = _sampleRep.Get(x => x.ID == 1);

            //Get all records from DB and return by converting them in List<SampleEntity>.
            IList<SampleEntity> sampleEntities = _sampleRep.FetchAll();

            //Get all records from DB and return by converting them in IQueryable<SampleEntity>.
            IQueryable<SampleEntity> sampleEntitiesQuery = _sampleRep.GetAll(x => x.SampleName == "ameo");

            //Insert a record in DB
            _sampleRep.Insert(new SampleEntity()
            {
                SampleName = "Ameotech"
            });

            //Update a record in DB
            _sampleRep.Update(new SampleEntity()
            {
                ID = 1, //for update we should have id
                SampleName = "Ameotech"
            });
        }
    }
}
