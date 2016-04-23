using AsyncPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.DAL.DAO
{
    [TableName("Games")]
    [PrimaryKey("Id")]
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
