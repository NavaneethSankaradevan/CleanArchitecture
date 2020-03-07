using Core.Data;

namespace MSF.Domain
{
    public class UOM: BaseEntity<int>
    {
        public string UnitOfMesurement { get; set; }

        public string UOMAbbr { get; set; }
    }
}
