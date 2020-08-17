using Dapper;
using PR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PR.Data
{
    /// <summary>
    /// Repositorio de datos para la tabla Menus
    /// </summary>
    public class MenusData : RepositoryGeneric<Menus>, IMenusData, IDisposable
    {
        /// <summary>
        /// Repositorio de datos para la tabla Menus
        /// </summary>
        public MenusData() : base() { }

        /// <summary>
        /// Consulta el Menu por id
        /// </summary>
        /// <param name="menuId">id menu</param>
        /// <param name="completeInfo">determina si debe consultar los menus con la informacion completa o no</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns></returns>
        public Menus GetMenuFromById(int menuId, bool completeInfo = false, IDbTransaction transaction = null)
        {
            try
            {
                Menus menu;
                if (completeInfo)
                {
                    var query = GetMenusAll(menuId: menuId, transaction: transaction);
                    menu = query.FirstOrDefault();
                }
                else
                {
                    menu = GetFindId(menuId, transaction: transaction);
                }
                return menu;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Consulta todos los menus por menu id o por menu padre id
        /// </summary>
        /// <param name="menuId">menu id</param>
        /// <param name="menuParentId">menu padre id</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns></returns>
        public List<Menus> GetMenusAll(int? menuId = null, int? menuParentId = null, IDbTransaction transaction = null)
        {
            try
            {
                var menus = new List<Menus>();
                menus = Connection.Query<Menus, Menus, Menus>("Menus_Search_ALL",
                    map: (mc, mp) => { mc.MenuParent = mp; return mc; },
                    param: new
                    {
                        MenuId = menuId,
                        MenuParentId = menuParentId
                    },
                    splitOn: "split",
                    commandType: CommandType.StoredProcedure).ToList();

                return menus ?? new List<Menus>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Repositorio de datos para la tabla Menus
    /// </summary>
    public interface IMenusData : IRepositoryGeneric<Menus>, IDisposable
    {
        /// <summary>
        /// Consulta todos los menus por menu id o por menu padre id
        /// </summary>
        /// <param name="menuId">menu id</param>
        /// <param name="menuParentId">menu padre id</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns></returns>
        List<Menus> GetMenusAll(int? menuId = null, int? menuParentId = null, IDbTransaction transaction = null);

        /// <summary>
        /// Consulta el Menu por id
        /// </summary>
        /// <param name="menuId">id menu</param>
        /// <param name="completeInfo">determina si debe consultar los menus con la informacion completa o no</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns></returns>
        Menus GetMenuFromById(int menuId, bool completeInfo = false, IDbTransaction transaction = null);
    }
}