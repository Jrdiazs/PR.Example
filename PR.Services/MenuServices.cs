using AutoMapper;
using PR.Data;
using PR.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PR.Services
{
    /// <summary>
    /// Clase de servicio para el manejo del componente IMenusData
    /// </summary>
    public class MenuServices : BaseServices, IDisposable, IMenuServices
    {
        /// <summary>
        /// Repositorio para la tabla Menus
        /// </summary>
        private readonly IMenusData _data;

        /// <summary>
        /// Instancia una nuevo servicio para el manejo del componente IMenusData
        /// </summary>
        /// <param name="data">IMenusData</param>
        /// <param name="mapper">mapeo de datos</param>
        public MenuServices(IMenusData data, IMapper mapper) : base(mapper)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        /// <summary>
        /// Consulta un listado de menus con sus menus hijos
        /// </summary>
        /// <returns></returns>
        public List<MenuItems> GetMenus()
        {
            try
            {
                var result = new List<MenuItems>();

                //Consulta todos los menus
                var menusDb = _data.GetMenusAll();

                //Menus de primer nivel
                var menuParents = menusDb.Where(x => !x.MenuParentId.HasValue).OrderBy(y => y.MenuOrder).ToList();

                foreach (var menuParent in menuParents)
                {
                    MenuItems item = new MenuItems()
                    {
                        Menu = menuParent
                    };
                    var childrens = SearchChildrens(item, menusDb);
                    item.MenuChildrens.AddRange(childrens);
                    result.Add(item);
                }

                return result ?? new List<MenuItems>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Busca los menus hijos
        /// </summary>
        /// <param name="menu">menu padre</param>
        /// <param name="menus">listado de menus</param>
        private List<MenuItems> SearchChildrens(MenuItems menu, List<Menus> menus)
        {
            try
            {
                var menusChildrens = new List<MenuItems>();

                if (menus.Any(x => x.MenuParentId == menu.Menu.MenuId))
                {
                    var menusSearch = menus.Where(x => x.MenuParentId == menu.Menu.MenuId).OrderBy(x => x.MenuOrder).ToList();
                    foreach (var menuParent in menusSearch)
                    {
                        MenuItems item = new MenuItems()
                        {
                            Menu = menuParent
                        };

                        if (menus.Any(x => x.MenuParentId == menuParent.MenuId))
                        {
                            var childrens = SearchChildrens(item, menus);
                            if (childrens.Any())
                                item.MenuChildrens.AddRange(childrens);
                        }

                        menusChildrens.Add(item);
                    }
                }
                return menusChildrens ?? new List<MenuItems>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Clase de servicio para el manejo del componente IMenusData
    /// </summary>
    public interface IMenuServices : IBaseServices, IDisposable
    {
        /// <summary>
        /// Consulta un listado de menus con sus menus hijos
        /// </summary>
        /// <returns></returns>
        List<MenuItems> GetMenus();
    }
}