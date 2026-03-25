using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPItems.Items
{
    [CustomItem(ItemType.Medkit)]
    public class Defibrillator : CustomItem
    {
        public override uint Id { get; set; } = 54;
        public override string Name { get; set; } = "Defibrillator";
        public override string Description { get; set; } = "Use while pointing at a corpse to revive them, if possible!";
        public override SpawnProperties SpawnProperties { get; set; }



    }
}
