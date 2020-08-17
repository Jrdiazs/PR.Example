using System.Collections.Generic;

namespace PR.Models
{
    /// <summary>
    /// Modelo que establece un menu con todos sus menus hijos
    /// </summary>
    public class MenuItems
    {
        /// <summary>
        /// Manu Actual
        /// </summary>
        public Menus Menu { get; set; }

        /// <summary>
        /// Listado de todos los menus hijos
        /// </summary>
        public List<MenuItems> MenuChildrens { get; set; } = new List<MenuItems>();
    }
}