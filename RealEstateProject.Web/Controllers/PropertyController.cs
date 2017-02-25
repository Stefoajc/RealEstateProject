using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstateProject.Data;
using RealEstateProject.Model;
using System.Windows.Controls;
using PagedList;
using RealEstateProject.Web.ViewModels;
using System.Linq.Expressions;
using LinqKit;
using System.Web.Caching;

namespace RealEstateProject.Web.Controllers
{
    public class PropertyController : Controller
    {
        private dbContext db = new dbContext();

        //Property/Index
        //[OutputCache(Duration = 1000)]
        public ActionResult Index(string sortOrder, string searchString, int? page, int pageSize = 3)
        {
            ViewBag.SortOrder = sortOrder;
            ViewBag.SearchString = searchString;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            return View();
        }

        // GET: Property/PartialIndex
        [OutputCache(Duration = 1000)]
        public ActionResult PartialIndex(string sortOrder, string searchString, int? page, int pageSize = 3)
        {

            //if (searchString != null)
            //{
            //    page = 1;
            //}
            //IQueryable<PROPERTIES> pROPERTIES;
            //pROPERTIES = db.PROPERTIES.Include(p => p.IMAGES).Include(p => p.REGIONS).Include(p => p.PROPERTYTYPES).Include(p => p.REGIONS.CITIES).Include(p => p.PARAMS);

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    var id = int.Parse(searchString);
            //    pROPERTIES = pROPERTIES.Where(p => p.PROPERTYID == id);
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(type) && type != "-- Всички --")
            //    {
            //        pROPERTIES = pROPERTIES.Where(p => p.PROPERTYTYPES.TYPE == type);
            //    }

            //}

            //switch (sortOrder)
            //{
            //    case "rent_asc":
            //        {
            //            pROPERTIES = pROPERTIES.OrderBy(p => p.RENT);
            //            break;
            //        }
            //    case "rent_desc":
            //        {
            //            pROPERTIES = pROPERTIES.OrderByDescending(p => p.RENT);
            //            break;
            //        }
            //    case "sell_asc":
            //        {
            //            pROPERTIES = pROPERTIES.OrderBy(p => p.SELL);
            //            break;
            //        }
            //    case "sell_desc":
            //        {
            //            pROPERTIES = pROPERTIES.OrderByDescending(p => p.SELL);
            //            break;
            //        }
            //    case "size_asc":
            //        {
            //            pROPERTIES = pROPERTIES.OrderBy(p => p.SIZE);
            //            break;
            //        }
            //    case "size_desc":
            //        {
            //            pROPERTIES = pROPERTIES.OrderByDescending(p => p.SIZE);
            //            break;
            //        }
            //    default:
            //        {
            //            pROPERTIES = pROPERTIES.OrderBy(p => p.RENT);
            //            break;
            //        }
            //}
            ////if (sortOrder == null)
            ////{
            ////    
            ////}

            //int pageNumber = (page ?? 1);
            //return PartialView(pROPERTIES.ToPagedList(pageNumber, pageSize));
            var pROPERTIES = db.PROPERTIES.Include(p => p.IMAGES).Include(p => p.REGIONS).Include(p => p.PROPERTYTYPES).Include(p => p.REGIONS.CITIES).Include(p => p.PARAMS);

            return PartialView(pageConstructor(pROPERTIES, sortOrder, searchString, page, pageSize));
        }

        // GET: Property/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = db.CATEGORY;
            var pROPERTIES = db.PROPERTIES.Include(p => p.IMAGES).Include(p => p.PARAMS)
                .Where(p => p.PROPERTYID == id)
                .FirstOrDefault();

            if (pROPERTIES == null)
            {
                return HttpNotFound();
            }
            return View(pROPERTIES);
        }

        // GET: Property/Create
        public ActionResult Create()
        {
            ViewBag.OWNERID = new SelectList(db.OWNERS, "OWNERID", "FNAME");
            ViewBag.PROPERTYTYPEID = new SelectList(db.PROPERTYTYPES, "ID", "TYPE");
            ViewBag.REGIONID = new SelectList(db.REGIONS, "REGIONID", "REGIONNAME");
            return View();
        }

        // POST: Property/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PROPERTYID,SIZE,RENT,SELL,DESCRIPTION,ADDRESS,IsActive,PROPERTYTYPEID,REGIONID,OWNERID")] PROPERTIES pROPERTIES)
        {
            if (ModelState.IsValid)
            {
                db.PROPERTIES.Add(pROPERTIES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OWNERID = new SelectList(db.OWNERS, "OWNERID", "FNAME", pROPERTIES.OWNERID);
            ViewBag.PROPERTYTYPEID = new SelectList(db.PROPERTYTYPES, "ID", "TYPE", pROPERTIES.PROPERTYTYPEID);
            ViewBag.REGIONID = new SelectList(db.REGIONS, "REGIONID", "REGIONNAME", pROPERTIES.REGIONID);
            return View(pROPERTIES);
        }

        // GET: Property/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROPERTIES pROPERTIES = db.PROPERTIES.Find(id);
            if (pROPERTIES == null)
            {
                return HttpNotFound();
            }
            ViewBag.OWNERID = new SelectList(db.OWNERS, "OWNERID", "FNAME", pROPERTIES.OWNERID);
            ViewBag.PROPERTYTYPEID = new SelectList(db.PROPERTYTYPES, "ID", "TYPE", pROPERTIES.PROPERTYTYPEID);
            ViewBag.REGIONID = new SelectList(db.REGIONS, "REGIONID", "REGIONNAME", pROPERTIES.REGIONID);
            return View(pROPERTIES);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PROPERTYID,SIZE,RENT,SELL,DESCRIPTION,ADDRESS,IsActive,PROPERTYTYPEID,REGIONID,OWNERID")] PROPERTIES pROPERTIES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROPERTIES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OWNERID = new SelectList(db.OWNERS, "OWNERID", "FNAME", pROPERTIES.OWNERID);
            ViewBag.PROPERTYTYPEID = new SelectList(db.PROPERTYTYPES, "ID", "TYPE", pROPERTIES.PROPERTYTYPEID);
            ViewBag.REGIONID = new SelectList(db.REGIONS, "REGIONID", "REGIONNAME", pROPERTIES.REGIONID);
            return View(pROPERTIES);
        }

        // GET: Properties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROPERTIES pROPERTIES = db.PROPERTIES.Find(id);
            if (pROPERTIES == null)
            {
                return HttpNotFound();
            }
            return View(pROPERTIES);
        }


        // POST: Property/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROPERTIES pROPERTIES = db.PROPERTIES.Find(id);
            db.PROPERTIES.Remove(pROPERTIES);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------//
        /*HelperFunctions and Actions*/
        //--------------------------------------------------------------------------------------------------------------------------------------------//
        public ActionResult RegionSearch(int cityID)
        {
            ViewBag.Regions = db.REGIONS.Where(r => r.CITYID == cityID).ToList();
            return View();
        }


        [HttpGet]
        public ActionResult SearchCriteria(string filters)
        {
            ViewBag.PropTypes = db.PROPERTYTYPES.ToList();
            ViewBag.Features = db.CATEGORY.Include(c => c.CATEGORYTYPES);
            ViewBag.Cities = db.CITIES;


            return View();
        }

        [HttpPost]
        public ActionResult SearchCriteriaResult(PropertiesSearchViewModel PSVM, string sortOrder, string searchString, int? page, int pageSize = 3)
        {

            var props = SearchProperties(PSVM);

            //---------------------TESTS

            string test = PSVM.ToString();
            Response.Redirect("Property/SearchCriteriaResult?" + test);

            //---------------------

            return View(pageConstructor(props, sortOrder, searchString, page, pageSize));
        }

        [HttpGet]
        public ActionResult SearchCriteriaResult(string propType, string Features, string sortOrder, string searchString, int? page, int pageSize = 3
            ,int RentFrom = 0, int RentTo = 0, int SellFrom = 0, int SellTo = 0, int SizeFrom = 0, int SizeTo = 0,int City= 0, int Region = 0)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentPageSize = pageSize;
            ViewBag.propType = propType;
            ViewBag.Features = Features;
            ViewBag.RentFrom = RentFrom;
            ViewBag.RentTo = RentTo;
            ViewBag.SellFrom = SellFrom;
            ViewBag.SellTo = SellTo;
            ViewBag.SizeFrom = SizeFrom;
            ViewBag.SizeTo = SizeTo;
            ViewBag.City = City;
            ViewBag.Region = Region;

            PropertiesSearchViewModel PSVM = new PropertiesSearchViewModel();
            PSVM = PSVM.StringToPSVM(propType, Features, RentFrom, RentTo, SellFrom, SellTo, SizeFrom, SizeTo, City, Region);
            var props = SearchProperties(PSVM);
            return View(pageConstructor(props, sortOrder, searchString, page, pageSize));
        }

        IQueryable<PROPERTIES> SearchProperties(PropertiesSearchViewModel PSVM)
        {
            object[] parameters = new object[100];
            var q =
                "Select DISTINCT(p.PROPERTYID) from PROPERTIES p" +
                //" Join PROPERTYTYPES pt on p.PROPERTYTYPEID = pt.ID" +
                //" join PARAMS prms on p.PROPERTYID = prms.PROPERTYID" +
                " join REGIONS r on r.REGIONID = p.REGIONID" +
                " join CITIES c on c.CITYID = r.CITYID";

            bool queryWhereCheck = false;

            string whereClausePropType = " where ";
            int i = 0;

            if (PSVM.RentTo != 0)
            {
                queryWhereCheck = true;
                whereClausePropType += "p.RENT <= @p" + i;
                whereClausePropType += " and ";
                parameters[i++] = PSVM.RentTo;
            }

            if (PSVM.SellTo != 0)
            {
                queryWhereCheck = true;
                whereClausePropType += "p.SELL <= @p" + i;
                whereClausePropType += " and ";
                parameters[i++] = PSVM.SellTo;
            }

            if (PSVM.SizeTo != 0)
            {
                queryWhereCheck = true;
                whereClausePropType += "p.SIZE <= @p" + i;
                whereClausePropType += " and ";
                parameters[i++] = PSVM.SizeTo;
            }

            if (PSVM.City != 0)
            {
                queryWhereCheck = true;
                whereClausePropType += "c.CITYID = @p" + i;
                whereClausePropType += " and ";
                parameters[i++] = PSVM.City;
            }

            if (PSVM.Region != 0)
            {
                queryWhereCheck = true;
                whereClausePropType += "r.REGIONID = @p" + i;
                whereClausePropType += " and ";
                parameters[i++] = PSVM.Region;
            }

            if (PSVM.PropType != null && PSVM.PropType.Count != 0)
            {
                q += " Join PROPERTYTYPES pt on p.PROPERTYTYPEID = pt.ID";
                queryWhereCheck = true;
                whereClausePropType += "(";

                foreach (var pt in PSVM.PropType)
                {
                    whereClausePropType += "pt.ID = @p" + i;
                    whereClausePropType += " or ";
                    parameters[i++] = pt;
                }
                whereClausePropType += " 1=0) and ";
            }
            

            if (PSVM.Features != null && PSVM.Features.Count != 0)
            {
                q += " join PARAMS prms on p.PROPERTYID = prms.PROPERTYID";
                queryWhereCheck = true;
                whereClausePropType += "(";
                foreach (var ft in PSVM.Features)
                {
                    whereClausePropType += "prms.CATEGORYVALUES = @p" + i;
                    whereClausePropType += " or ";
                    parameters[i++] = ft;
                }
                whereClausePropType += " 1=0) and ";
            }
            whereClausePropType += " 1=1;";
            q += whereClausePropType;



            IQueryable<PROPERTIES> props;
            if (queryWhereCheck == true)
            {
                var query = db.Database.SqlQuery<int>(q, parameters).ToList();
                props = db.PROPERTIES.Where(p => query.Contains(p.PROPERTYID));
            }
            else
            {
                props = db.PROPERTIES;
            }
            props = props.OrderBy(p => p.PROPERTYID);

            return props;
        }

        public IPagedList<PROPERTIES> pageConstructor(IQueryable<PROPERTIES> pROPERTIES, string sortOrder, string searchString, int? page, int pageSize = 3)
        {
            if (searchString != null)
            {
                page = 1;
            }


            if (!String.IsNullOrEmpty(searchString))
            {
                var id = int.Parse(searchString);
                pROPERTIES = pROPERTIES.Where(p => p.PROPERTYID == id);
            }


            switch (sortOrder)
            {
                case "rent_asc":
                    {
                        pROPERTIES = pROPERTIES.OrderBy(p => p.RENT);
                        break;
                    }
                case "rent_desc":
                    {
                        pROPERTIES = pROPERTIES.OrderByDescending(p => p.RENT);
                        break;
                    }
                case "sell_asc":
                    {
                        pROPERTIES = pROPERTIES.OrderBy(p => p.SELL);
                        break;
                    }
                case "sell_desc":
                    {
                        pROPERTIES = pROPERTIES.OrderByDescending(p => p.SELL);
                        break;
                    }
                case "size_asc":
                    {
                        pROPERTIES = pROPERTIES.OrderBy(p => p.SIZE);
                        break;
                    }
                case "size_desc":
                    {
                        pROPERTIES = pROPERTIES.OrderByDescending(p => p.SIZE);
                        break;
                    }
                default:
                    {
                        pROPERTIES = pROPERTIES.OrderBy(p => p.RENT);
                        break;
                    }
            }
            //if (sortOrder == null)
            //{
            //    
            //}

            int pageNumber = (page ?? 1);
            return (pROPERTIES.ToPagedList(pageNumber, pageSize));
        }

    }

}


