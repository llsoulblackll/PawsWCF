using PawsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsBussinessLogic.BussinessLogicObject
{
    public class PetBlo : IEntityBlo<Pet>
    {
        public int Insert(Pet toInsert)
        {
            throw new NotImplementedException();
        }

        public bool Update(Pet toUpdate)
        {
            throw new NotImplementedException();
        }
        public bool Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Pet Find(object id)
        {
            throw new NotImplementedException();
        }

        public List<Pet> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
