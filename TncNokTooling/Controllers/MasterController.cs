using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using TncNokTooling.Models;

namespace TncNokTooling.Controllers
{
    public class MasterController : Controller
    {
        private TncNokToolingEntities DBTNT = new TncNokToolingEntities();

        [Chk_Authen]
        public ActionResult PRData()
        {
            return View();
        }

        [Chk_Authen]
        public ActionResult LeadTime()
        {
            ViewBag.PlantList = DBTNT.tm_nok_plant;
            ViewBag.DescList = DBTNT.tm_process;
            ViewBag.ProductList = DBTNT.tm_product;
            ViewBag.TypeList = DBTNT.tm_type;
            return View();
        }

        //public ActionResult Relation()
        //{
        //    return View();
        //}

        //--------------------------------------//

        [HttpPost]
        public JsonResult PlantList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = DBTNT.tm_nok_plant;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.id,
                        s.name
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreatePlant()
        {
            try
            {
                //var id = byte.Parse(Request.Form["id"]);
                //var dbData = DBTNT.tm_nok_plant.Find(id);
                //if (dbData == null)
                //{
                    tm_nok_plant data = new tm_nok_plant();
                    //data.id = id;
                    data.name = Request.Form["name"];

                    DBTNT.tm_nok_plant.Add(data);
                //}

                DBTNT.SaveChanges();

                return Json(new { Result = "OK", Record = DBTNT.tm_nok_plant.OrderByDescending(o => o.id).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdatePlant()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var data = DBTNT.tm_nok_plant.Find(id);
                data.name = Request.Form["name"];
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeletePlant()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var data = DBTNT.tm_nok_plant.Find(id);
                DBTNT.tm_nok_plant.Remove(data);
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //--------------------------------------//

        [HttpPost]
        public JsonResult DescriptionList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = DBTNT.tm_process;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.id,
                        s.name
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateDescription()
        {
            try
            {
                //var id = byte.Parse(Request.Form["id"]);
                //var dbData = DBTNT.tm_description.Find(id);
                //if (dbData == null)
                //{
                tm_process data = new tm_process();
                    //data.id = id;
                    data.name = Request.Form["name"];

                    DBTNT.tm_process.Add(data);
                //}

                DBTNT.SaveChanges();

                return Json(new { Result = "OK", Record = DBTNT.tm_process.OrderByDescending(o => o.id).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateDescription()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var data = DBTNT.tm_process.Find(id);
                data.name = Request.Form["name"];
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteDescription()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var data = DBTNT.tm_process.Find(id);
                DBTNT.tm_process.Remove(data);
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //--------------------------------------//

        [HttpPost]
        public JsonResult ProductList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = DBTNT.tm_product;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.id,
                        s.name
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateProduct()
        {
            try
            {
                //var id = byte.Parse(Request.Form["id"]);
                //var dbData = DBTNT.tm_product.Find(id);
                //if (dbData == null)
                //{
                    tm_product data = new tm_product();
                    //data.id = id;
                    data.name = Request.Form["name"];

                    DBTNT.tm_product.Add(data);
                //}

                DBTNT.SaveChanges();

                return Json(new { Result = "OK", Record = DBTNT.tm_product.OrderByDescending(o => o.id).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateProduct()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var data = DBTNT.tm_product.Find(id);
                data.name = Request.Form["name"];
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteProduct()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var data = DBTNT.tm_product.Find(id);
                DBTNT.tm_product.Remove(data);
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //--------------------------------------//

        [HttpPost]
        public JsonResult TypeList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = DBTNT.tm_type;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.id,
                        s.name
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateType()
        {
            try
            {
                //var id = byte.Parse(Request.Form["id"]);
                //var dbData = DBTNT.tm_type.Find(id);
                //if (dbData == null)
                //{
                    tm_type data = new tm_type();
                    //data.id = id;
                    data.name = Request.Form["name"];

                    DBTNT.tm_type.Add(data);
                //}

                DBTNT.SaveChanges();

                return Json(new { Result = "OK", Record = DBTNT.tm_type.OrderByDescending(o => o.id).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateType()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var data = DBTNT.tm_type.Find(id);
                data.name = Request.Form["name"];
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteType()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var data = DBTNT.tm_type.Find(id);
                DBTNT.tm_type.Remove(data);
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //--------------------------------------//

        [HttpPost]
        public JsonResult LeadTimeList(byte? plant, byte? process, byte? product, byte? type, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = from a in DBTNT.tm_leadtime select a;

                if (plant != null)
                {
                    query = query.Where(w => w.plant == plant);
                }
                if (process != null)
                {
                    query = query.Where(w => w.process == process);
                }
                if (product != null)
                {
                    query = query.Where(w => w.product == product);
                }
                if (type != null)
                {
                    query = query.Where(w => w.type == type);
                }

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.plant,
                        s.process,
                        s.product,
                        s.type,
                        s.etd,
                        s.inspection,
                        s.making,
                        s.trial,
                        s.shipping,
                        s.eta
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetPlantList()
        {
            try
            {
                var result = DBTNT.tm_nok_plant
                    .Select(r => new { DisplayText = r.name, Value = r.id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetDescList()
        {
            try
            {
                var result = DBTNT.tm_process
                    .Select(r => new { DisplayText = r.name, Value = r.id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetProductList()
        {
            try
            {
                var result = DBTNT.tm_product
                    .Select(r => new { DisplayText = r.name, Value = r.id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetTypeList()
        {
            try
            {
                var result = DBTNT.tm_type
                    .Select(r => new { DisplayText = r.name, Value = r.id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //--------------------------------------//

        //[HttpPost]
        //public JsonResult PlantDescList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        //{
        //    try
        //    {
        //        var query = DBTNT.tr_plant_process;

        //        //Get data from database
        //        int TotalRecord = query.Count();

        //        // Paging
        //        var output = query
        //            .Select(s => new
        //            {
        //                s.plant,
        //                s.process
        //            }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

        //        //Return result to jTable
        //        return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult CreatePlantDesc()
        //{
        //    try
        //    {
        //        var plant = byte.Parse(Request.Form["plant"]);
        //        var process = byte.Parse(Request.Form["process"]);
        //        var dbData = DBTNT.tr_plant_process.Find(plant, process);
        //        if (dbData == null)
        //        {
        //            tr_plant_process data = new tr_plant_process();
        //            data.plant = plant;
        //            data.process = process;

        //            DBTNT.tr_plant_process.Add(data);
        //            DBTNT.SaveChanges();
        //        }

        //        return Json(new { Result = "OK", Record = DBTNT.tr_plant_process.OrderByDescending(o => o.plant).FirstOrDefault() });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult DeletePlantDesc()
        //{
        //    try
        //    {
        //        var plant = byte.Parse(Request.Form["plant"]);
        //        var process = byte.Parse(Request.Form["process"]);
        //        var dbData = DBTNT.tr_plant_process.Find(plant, process);
        //        if (dbData != null)
        //        {
        //            DBTNT.tr_plant_process.Remove(dbData);
        //            DBTNT.SaveChanges();
        //        }

        //        return Json(new { Result = "OK" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        ////--------------------------------------//

        //[HttpPost]
        //public JsonResult DescProductList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        //{
        //    try
        //    {
        //        var query = DBTNT.tr_process_product;

        //        //Get data from database
        //        int TotalRecord = query.Count();

        //        // Paging
        //        var output = query
        //            .Select(s => new
        //            {
        //                s.process,
        //                s.product
        //            }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

        //        //Return result to jTable
        //        return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult CreateDescProduct()
        //{
        //    try
        //    {
        //        var process = byte.Parse(Request.Form["process"]);
        //        var product = byte.Parse(Request.Form["product"]);
        //        var dbData = DBTNT.tr_process_product.Find(process, product);
        //        if (dbData == null)
        //        {
        //            tr_process_product data = new tr_process_product();
        //            data.process = process;
        //            data.product = product;

        //            DBTNT.tr_process_product.Add(data);
        //            DBTNT.SaveChanges();
        //        }

        //        return Json(new { Result = "OK", Record = DBTNT.tr_process_product.OrderByDescending(o => o.process).FirstOrDefault() });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult DeleteDescProduct()
        //{
        //    try
        //    {
        //        var process = byte.Parse(Request.Form["process"]);
        //        var product = byte.Parse(Request.Form["product"]);
        //        var dbData = DBTNT.tr_process_product.Find(process, product);
        //        if (dbData != null)
        //        {
        //            DBTNT.tr_process_product.Remove(dbData);
        //            DBTNT.SaveChanges();
        //        }

        //        return Json(new { Result = "OK" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        ////--------------------------------------//

        //[HttpPost]
        //public JsonResult ProductTypeList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        //{
        //    try
        //    {
        //        var query = DBTNT.tr_product_type;

        //        //Get data from database
        //        int TotalRecord = query.Count();

        //        // Paging
        //        var output = query
        //            .Select(s => new
        //            {
        //                s.product,
        //                s.type
        //            }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

        //        //Return result to jTable
        //        return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult CreateProductType()
        //{
        //    try
        //    {
        //        var product = byte.Parse(Request.Form["product"]);
        //        var type = byte.Parse(Request.Form["type"]);
        //        var dbData = DBTNT.tr_product_type.Find(product, type);
        //        if (dbData == null)
        //        {
        //            tr_product_type data = new tr_product_type();
        //            data.product = product;
        //            data.type = type;

        //            DBTNT.tr_product_type.Add(data);
        //            DBTNT.SaveChanges();
        //        }

        //        return Json(new { Result = "OK", Record = DBTNT.tr_product_type.OrderByDescending(o => o.product).FirstOrDefault() });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult DeleteProductType()
        //{
        //    try
        //    {
        //        var product = byte.Parse(Request.Form["product"]);
        //        var type = byte.Parse(Request.Form["type"]);
        //        var dbData = DBTNT.tr_product_type.Find(product, type);
        //        if (dbData != null)
        //        {
        //            DBTNT.tr_product_type.Remove(dbData);
        //            DBTNT.SaveChanges();
        //        }

        //        return Json(new { Result = "OK" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //--------------------------------------//
  
    }
}
